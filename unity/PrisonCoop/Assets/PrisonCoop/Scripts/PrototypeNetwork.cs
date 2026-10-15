using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace PrisonCoop
{
    // Small LAN/loopback lab: NGO owns connections, host owns tool state.
    // These explicit messages are prototype data, not the final production movement model.
    public sealed class PrototypeNetwork : MonoBehaviour
    {
        public const ulong Nobody = ulong.MaxValue;
        public static PrototypeNetwork Instance { get; private set; }
        public ulong ToolOwner { get; private set; } = Nobody;
        public ulong LocalId => manager != null ? manager.LocalClientId : Nobody;
        public bool Connected => manager != null && manager.IsConnectedClient;
        public bool Hosting => manager != null && manager.IsHost;
        public bool Running => manager != null && manager.IsListening;
        public int PeerCount => poses.Count;
        public int RemoteAvatarCount => avatars.Count;
        public TestTool Tool { get; private set; }
        private NetworkManager manager;
        private PrototypePlayer player;
        private Transform hand;
        private Vector3 toolHome;
        private string address = "127.0.0.1";
        private string status = "Yksinpeli · paikallinen verkkokoe valmis käynnistettäväksi";
        private float nextSend;
        private readonly Dictionary<ulong, Pose> poses = new Dictionary<ulong, Pose>();
        private readonly Dictionary<ulong, GameObject> avatars = new Dictionary<ulong, GameObject>();

        private struct Pose { public Vector3 Position; public float Yaw,Pitch; }
        private PrisonEnvironment environment;
        public bool FullRemoteBodies
        {
            get { foreach(var avatar in avatars.Values)if(avatar.GetComponent<PlayerBody>()==null)return false;return avatars.Count>0; }
        }


        private void Start()
        {
            Instance = this;
            environment=FindFirstObjectByType<PrisonEnvironment>();
            player = FindFirstObjectByType<PrototypePlayer>();
            hand = player.GetComponentInChildren<Camera>().transform;
            Tool = FindFirstObjectByType<TestTool>();
            toolHome = Tool.transform.position;
        }

        public void Begin(bool host, ushort port = 7777)
        {
            if (manager != null) return;
            Tool.SetOwner(null);
            var root = new GameObject("Local networking");
            var transport = root.AddComponent<UnityTransport>();
            transport.SetConnectionData(address, port, "127.0.0.1");
            manager = root.AddComponent<NetworkManager>();
            manager.NetworkConfig = new NetworkConfig
            {
                NetworkTransport = transport,
                EnableSceneManagement = false,
                ConnectionApproval = true,
                ForceSamePrefabs = false
            };
            manager.ConnectionApprovalCallback = (request, response) =>
            {
                response.Approved = manager.ConnectedClients.Count < 4;
                response.CreatePlayerObject = false;
                response.Reason = response.Approved ? "" : "Testiversioon mahtuu neljä pelaajaa.";
            };
            manager.OnClientConnectedCallback += OnConnected;
            manager.OnClientDisconnectCallback += OnDisconnected;
            bool started = host ? manager.StartHost() : manager.StartClient();
            if (!started) { status = "Yhteyden käynnistys epäonnistui. Käynnistä sovellus uudelleen."; return; }
            manager.CustomMessagingManager.RegisterNamedMessageHandler("pose", ReceivePose);
            manager.CustomMessagingManager.RegisterNamedMessageHandler("state", ReceiveState);
            manager.CustomMessagingManager.RegisterNamedMessageHandler("tool", ReceiveToolRequest);
            status = host ? "Host käynnissä · toinen asiakas voi liittyä" : "Yhdistetään paikalliseen hostiin…";
        }

        private void OnConnected(ulong id)
        {
            if (manager.IsServer)
                poses[id] = new Pose { Position = SpawnPoint(id) };
            if (id == manager.LocalClientId)
            {
                var controller = player.GetComponent<CharacterController>();
                controller.enabled = false;
                player.transform.position = SpawnPoint(id);
                controller.enabled = true;
                if(environment!=null)player.GetComponent<PlayerBody>().Initialize(environment.CharacterProfiles[(int)(id%4)],environment.CharacterShader,true);
                status = manager.IsHost ? "Host · yhteys valmis" : "Asiakas · yhteys valmis";
            }
            ShowState();
        }

        private void OnDisconnected(ulong id)
        {
            poses.Remove(id);
            if (avatars.TryGetValue(id, out var avatar)) { Destroy(avatar); avatars.Remove(id); }
            if (manager.IsServer && ToolOwner == id) ToolOwner = Nobody;
            if (id == LocalId || !manager.IsServer)
            {
                foreach (var other in avatars.Values) Destroy(other);
                avatars.Clear();
                poses.Clear();
                ToolOwner = Nobody;
                Tool.SetOwner(null);
                status = "Yhteys katkesi. Käynnistä testisovellus uudelleen liittyäksesi.";
            }
        }

        private void Update()
        {
            if (!Connected || Time.unscaledTime < nextSend) return;
            nextSend = Time.unscaledTime + 0.05f;
            var pose = new Pose { Position = player.transform.position, Yaw = player.transform.eulerAngles.y, Pitch=player.ViewPitch };
            if (Hosting)
            {
                poses[LocalId] = pose;
                Broadcast();
            }
            else
            {
                using var writer = new FastBufferWriter(48, Allocator.Temp);
                writer.WriteValueSafe(pose.Position);
                writer.WriteValueSafe(pose.Yaw);
                writer.WriteValueSafe(pose.Pitch);
                manager.CustomMessagingManager.SendNamedMessage("pose", NetworkManager.ServerClientId, writer, NetworkDelivery.UnreliableSequenced);
            }
            ShowState();
        }

        private void ReceivePose(ulong sender, FastBufferReader reader)
        {
            if (!manager.IsServer || !poses.ContainsKey(sender)) return;
            reader.ReadValueSafe(out Vector3 position);
            reader.ReadValueSafe(out float yaw);
            reader.ReadValueSafe(out float lookPitch);
            if (!float.IsFinite(position.x) || !float.IsFinite(position.y) || !float.IsFinite(position.z) || !float.IsFinite(yaw)||!float.IsFinite(lookPitch)) return;
            if(environment!=null)
            {if(position.x< -22||position.x>50||position.z< -19||position.z>42||position.y< -1||position.y>10)return;}
            else if (Mathf.Abs(position.x) > 2.6f || position.z < -3.2f || position.z > 5.2f || position.y < -1 || position.y > 3) return;
            poses[sender] = new Pose { Position = position, Yaw = yaw,Pitch=Mathf.Clamp(lookPitch,-80,80) };
        }

        private void Broadcast()
        {
            using var writer = new FastBufferWriter(512, Allocator.Temp);
            writer.WriteValueSafe(poses.Count);
            foreach (var pair in poses)
            {
                writer.WriteValueSafe(pair.Key);
                writer.WriteValueSafe(pair.Value.Position);
                writer.WriteValueSafe(pair.Value.Yaw);
                writer.WriteValueSafe(pair.Value.Pitch);
            }
            writer.WriteValueSafe(ToolOwner);
            foreach (ulong id in manager.ConnectedClientsIds)
                if (id != LocalId)
                    manager.CustomMessagingManager.SendNamedMessage("state", id, writer, NetworkDelivery.ReliableSequenced);
        }

        private void ReceiveState(ulong sender, FastBufferReader reader)
        {
            if (Hosting || sender != NetworkManager.ServerClientId) return;
            reader.ReadValueSafe(out int count);
            if (count < 1 || count > 4) return;
            poses.Clear();
            for (int i = 0; i < count; i++)
            {
                reader.ReadValueSafe(out ulong id);
                reader.ReadValueSafe(out Vector3 position);
                reader.ReadValueSafe(out float yaw);
                reader.ReadValueSafe(out float lookPitch);
                poses[id] = new Pose { Position = position, Yaw = yaw,Pitch=lookPitch };
            }
            reader.ReadValueSafe(out ulong owner);
            ToolOwner = owner;
            ShowState();
        }

        public void ToggleTool()
        {
            if (!Connected) return;
            if (Hosting) ApplyToolRequest(LocalId);
            else
            {
                using var writer = new FastBufferWriter(1, Allocator.Temp);
                writer.WriteValueSafe((byte)1);
                manager.CustomMessagingManager.SendNamedMessage("tool", NetworkManager.ServerClientId, writer, NetworkDelivery.ReliableSequenced);
            }
        }

        private void ReceiveToolRequest(ulong sender, FastBufferReader reader)
        {
            if (manager.IsServer) ApplyToolRequest(sender);
        }

        private void ApplyToolRequest(ulong sender)
        {
            if (!poses.TryGetValue(sender, out var pose)) return;
            if (ToolOwner == sender) ToolOwner = Nobody;
            else if (ToolOwner == Nobody && Vector3.Distance(pose.Position + Vector3.up * 1.65f, toolHome) <= 2.5f)
            {
                // Validate range and line of sight on the host, independent of a client's request.
                Vector3 origin = pose.Position + Vector3.up * 1.65f;
                Physics.SyncTransforms();
                if (Physics.Raycast(origin, (toolHome - origin).normalized, out var hit, 2.5f) && hit.collider.GetComponent<TestTool>() == Tool)
                    ToolOwner = sender;
            }
            Broadcast();
            ShowState();
        }

        private void ShowState()
        {
            var removed = new List<ulong>();
            foreach (var pair in avatars)
                if (!poses.ContainsKey(pair.Key)) removed.Add(pair.Key);
            foreach (ulong id in removed) { Destroy(avatars[id]); avatars.Remove(id); }
            foreach (var pair in poses)
            {
                if (pair.Key == LocalId) continue;
                if (!avatars.TryGetValue(pair.Key, out var avatar))
                {
                    if(environment!=null)
                    {
                        avatar=new GameObject("Other player "+pair.Key);
                        avatar.AddComponent<PlayerBody>().Initialize(environment.CharacterProfiles[(int)(pair.Key%4)],environment.CharacterShader,false);
                    }
                    else
                    {
                        avatar=GameObject.CreatePrimitive(PrimitiveType.Capsule);avatar.GetComponent<Collider>().enabled=false;
                        avatar.transform.localScale=new Vector3(0.6f,0.9f,0.6f);
                    }
                    avatars.Add(pair.Key, avatar);
                }
                var body=avatar.GetComponent<PlayerBody>();
                if(body!=null)body.ReceivePose(pair.Value.Position,pair.Value.Yaw,pair.Value.Pitch);
                else avatar.transform.SetPositionAndRotation(pair.Value.Position+Vector3.up*0.9f,Quaternion.Euler(0,pair.Value.Yaw,0));
            }
            Transform holder = null;
            if (ToolOwner == LocalId) holder = hand;
            else if (avatars.TryGetValue(ToolOwner, out var owner)) holder = owner.transform;
            Tool.SetOwner(holder);
        }

        private void OnGUI()
        {
            if(environment!=null&&!Running)return;
            if (player == null || player.GetComponent<PrototypeSettings>().IsOpen) return;
            var prior = GUI.matrix;
            GUI.matrix = Matrix4x4.Scale(new Vector3(Screen.width / 1280f, Screen.height / 720f, 1));
            GUI.Box(new Rect(820, 20, 438, 150), "");
            GUI.Label(new Rect(840, 35, 400, 30), "PAIKALLINEN CO-OP-KOE");
            GUI.Label(new Rect(840, 70, 400, 30), status);
            if (manager == null)
            {
                GUI.Label(new Rect(840, 115, 400, 30), "H — aloita host · J — liity hostiin");
            }
            else GUI.Label(new Rect(840, 115, 400, 30), "Pelaajia " + PeerCount + "/4 · " + address + ":7777");
            GUI.matrix = prior;
        }

        private Vector3 SpawnPoint(ulong id)
        {
            if(environment!=null)return new Vector3(-3+id*1.6f,0.08f,3.5f);
            return new Vector3(id==0?-1.2f:1.2f,0.02f,0.8f);
        }
        private void OnDestroy()
        {
            if (manager != null && manager.IsListening) manager.Shutdown();
            if (Instance == this) Instance = null;
        }
    }
}
