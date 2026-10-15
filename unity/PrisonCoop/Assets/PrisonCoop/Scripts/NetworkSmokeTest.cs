using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PrisonCoop
{
    public sealed class NetworkSmokeTest : MonoBehaviour
    {
        private string role;
        private string output;
        private readonly List<string> results = new List<string>();
        private int failed;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Install()
        {
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i + 2 < args.Length; i++)
                if (args[i] == "--net-smoke")
                {
                    var test = new GameObject("Network integration test").AddComponent<NetworkSmokeTest>();
                    test.role = args[i + 1];
                    test.output = args[i + 2];
                    return;
                }
        }

        private IEnumerator Start()
        {
            Directory.CreateDirectory(output);
            Application.logMessageReceived += OnLog;
            yield return null;
            var network = PrototypeNetwork.Instance;
            var player = FindFirstObjectByType<PrototypePlayer>();
            player.enabled = false;
            network.Begin(role == "host", 7799);
            float deadline = Time.realtimeSinceStartup + 20;
            while ((!network.Connected || network.PeerCount != 2) && Time.realtimeSinceStartup < deadline) yield return null;
            Check(network.Connected && network.PeerCount == 2, "Two independent processes connected and share two poses");
            if (!network.Connected || network.PeerCount != 2) { Finish(); yield break; }
            deadline = Time.realtimeSinceStartup + 2;
            while (network.RemoteAvatarCount != 1 && Time.realtimeSinceStartup < deadline) yield return null;
            Check(network.RemoteAvatarCount == 1, "Remote player has a visible placeholder body");

            if (role == "client")
            {
                network.ToggleTool();
                yield return new WaitForSecondsRealtime(0.3f);
                Check(network.ToolOwner == PrototypeNetwork.Nobody, "Host rejects picking up through the cell divider");
                var controller = player.GetComponent<CharacterController>();
                controller.enabled = false;
                player.transform.position = new Vector3(-1.2f, 0.02f, 0.8f);
                controller.enabled = true;
                yield return new WaitForSecondsRealtime(0.4f);
                network.ToggleTool();
                deadline = Time.realtimeSinceStartup + 5;
                while (network.ToolOwner != network.LocalId && Time.realtimeSinceStartup < deadline) yield return null;
                Check(network.ToolOwner == network.LocalId && network.Tool.IsHeld, "Client sees authoritative tool pickup");
                yield return new WaitForSecondsRealtime(1.5f);
                Check(network.ToolOwner == network.LocalId, "Ownership remains stable while the host also requests the tool");
                Finish();
            }
            else
            {
                deadline = Time.realtimeSinceStartup + 7;
                while (network.ToolOwner == PrototypeNetwork.Nobody && Time.realtimeSinceStartup < deadline) yield return null;
                Check(network.ToolOwner != PrototypeNetwork.Nobody && network.ToolOwner != network.LocalId, "Host sees the client's tool ownership");
                ulong owner = network.ToolOwner;
                network.ToggleTool();
                Check(network.ToolOwner == owner, "Second player cannot take an already owned tool");
                deadline = Time.realtimeSinceStartup + 7;
                while (network.PeerCount == 2 && Time.realtimeSinceStartup < deadline) yield return null;
                yield return null;
                Check(network.PeerCount == 1 && network.ToolOwner == PrototypeNetwork.Nobody && !network.Tool.IsHeld,
                    "Disconnect returns held tool to the table and removes the remote player");
                Finish();
            }
        }

        private void Check(bool passed, string description)
        {
            results.Add((passed ? "PASS " : "FAIL ") + description);
            if (!passed) failed++;
        }

        private void OnLog(string message, string trace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
                Check(false, "Runtime: " + message);
        }

        private void Finish()
        {
            Application.logMessageReceived -= OnLog;
            results.Add("Loopback only; no second computer or human co-op test.");
            File.WriteAllLines(Path.Combine(output, "network-" + role + ".txt"), results);
            Application.Quit(failed == 0 ? 0 : 1);
        }
    }
}
