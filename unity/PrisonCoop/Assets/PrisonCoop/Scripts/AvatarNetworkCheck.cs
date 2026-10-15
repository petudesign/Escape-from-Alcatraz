using System;
using System.Collections;
using System.IO;
using UnityEngine;
namespace PrisonCoop
{
    public sealed class AvatarNetworkCheck : MonoBehaviour
    {
        private string role,output;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Install()
        {
            var args=Environment.GetCommandLineArgs();
            for(int i=0;i+2<args.Length;i++)if(args[i]=="--avatar-check")
            {var test=new GameObject("Avatar check").AddComponent<AvatarNetworkCheck>();test.role=args[i+1];test.output=args[i+2];}
        }
        private IEnumerator Start()
        {
            yield return null;yield return null;
            var player=FindFirstObjectByType<PrototypePlayer>();player.enabled=false;
            var network=PrototypeNetwork.Instance;network.Begin(role=="host",7808);
            float until=Time.realtimeSinceStartup+25;
            while((network.PeerCount<2||!network.FullRemoteBodies)&&Time.realtimeSinceStartup<until)yield return null;
            bool passed=network.Connected&&network.PeerCount==2&&network.FullRemoteBodies&&player.GetComponent<PlayerBody>().HasFullBody;
            yield return new WaitForSecondsRealtime(2);
            Directory.CreateDirectory(output);
            File.WriteAllText(Path.Combine(output,"avatar-"+role+".txt"),"Connected: "+network.Connected+"\nPeers: "+network.PeerCount+"\nRemote full mesh bodies: "+network.FullRemoteBodies+"\nLocal body: "+player.GetComponent<PlayerBody>().HasFullBody+"\nLoopback two-process avatar check: "+passed+"\n");
            if(passed)
            {
                player.GetComponent<PlayerBody>().SetModelVisible(false);
                var camera=player.GetComponentInChildren<Camera>();
                var others=FindObjectsByType<PlayerBody>(FindObjectsSortMode.None);
                foreach(var body in others)if(body.gameObject!=player.gameObject)
                {
                    camera.transform.position=body.transform.position+Vector3.up*1.4f+Vector3.forward*2;
                    camera.transform.LookAt(body.transform.position+Vector3.up*1.0f);
                    PrisonPreview.SaveImage(camera,Path.Combine(output,"remote-"+role+".png"));break;
                }
            }
            // Keep the host alive long enough for its peer to capture; no disconnect before both checks.
            yield return new WaitForSecondsRealtime(role=="host"?5:2);
            Application.Quit(passed?0:4);
        }
    }
}
