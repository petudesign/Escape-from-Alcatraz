using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace PrisonCoop
{
    public sealed class PrisonPreview : MonoBehaviour
    {
        private string output;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Install()
        {
            var args=Environment.GetCommandLineArgs();
            for(int i=0;i+1<args.Length;i++)if(args[i]=="--visual-preview")
                new GameObject("Visual preview capture").AddComponent<PrisonPreview>().output=args[i+1];
        }
        private IEnumerator Start()
        {
            Directory.CreateDirectory(output);yield return null;yield return null;
            Time.timeScale=1;

            var player=FindFirstObjectByType<PrototypePlayer>();player.enabled=false;
            var camera=player.GetComponentInChildren<Camera>();
            var population=FindFirstObjectByType<PrisonPopulation>();
            yield return null;
            var ownBody=player.GetComponent<PlayerBody>();
            if(ownBody!=null)
            {
                Capture(camera,player.transform.position+Vector3.up*1.65f,player.transform.position+new Vector3(0,1.65f,3),"first-person.png");
                Capture(camera,player.transform.position+Vector3.up*1.65f,player.transform.position+new Vector3(0,0.15f,0.6f),"first-person-down.png");
                ownBody.SetModelVisible(false);
            }
            if(population!=null&&population.Actors.Length>0)
            {
                yield return new WaitForSecondsRealtime(1);
                var actor=population.Actors[0];
                Capture(camera,actor.transform.position+actor.transform.forward*1.9f+Vector3.up*1.3f,actor.transform.position+Vector3.up*1.05f,"character.png");
            }
            if(population!=null)
            {
                for(int i=0;i<Mathf.Min(4,population.Actors.Length);i++)
                {
                    var subject=population.Actors[i];
                    var center=subject.transform.position;
                    Capture(camera,center+subject.transform.forward*1.25f+subject.transform.right*0.30f+Vector3.up*1.56f,center+Vector3.up*1.48f,"portrait-"+i+".png");
                }
            }
            Capture(camera,new Vector3(-1.35f,1.65f,-0.7f),new Vector3(-1.65f,1.2f,-3.4f),"cell.png");
            Capture(camera,new Vector3(-1.1f,1.1f,-0.85f),new Vector3(-0.095f,0.65f,-2.3f),"wear-detail.png");
            Capture(camera,new Vector3(-12.6f,1.7f,4),new Vector3(10,4.8f,4.1f),"cellblock.png");
            Capture(camera,new Vector3(-13,8.35f,0.8f),new Vector3(10,5.2f,5),"gallery.png");
            Capture(camera,new Vector3(24,1.65f,0),new Vector3(30,1.5f,-14),"dining.png");
            Capture(camera,new Vector3(24,1.65f,17),new Vector3(33,2.4f,35),"yard.png");
            Capture(camera,new Vector3(-12.5f,1.8f,3.5f),new Vector3(-18.5f,3.6f,4.5f),"stairs.png");
            Capture(camera,new Vector3(18.8f,1.65f,3.6f),new Vector3(25,1.6f,6.3f),"control-hall.png");
            if(population.Guards!=null&&population.Guards.Length>0)
            {
                var guard=population.Guards[0];
                Capture(camera,guard.transform.position+guard.transform.forward*1.3f+Vector3.up*1.5f,guard.transform.position+Vector3.up*1.1f,"guard.png");
            }
            if(Array.IndexOf(Environment.GetCommandLineArgs(),"--animation-check")>=0)
            {
                foreach(var prisoner in population.Actors)prisoner.gameObject.SetActive(false);
                var actor=population.Actors[0];actor.gameObject.SetActive(true);
                actor.Motor.enabled=false;actor.enabled=false;
                actor.transform.SetPositionAndRotation(new Vector3(-5,0.02f,4),Quaternion.identity);
                var visual=actor.GetComponent<PrisonerVisual>();
                float elapsed=0;int frame=0;float minKnee=180,maxKnee=-180;
                while(elapsed<1.3f)
                {
                    yield return null;elapsed+=Time.deltaTime;
                    visual.Animate(1.2f,1.2f);
                    foreach(var joint in actor.GetComponentsInChildren<Transform>())if(joint.name=="Knee")
                    {float angle=Mathf.DeltaAngle(0,joint.localEulerAngles.x);minKnee=Mathf.Min(minKnee,angle);maxKnee=Mathf.Max(maxKnee,angle);}
                    if(elapsed>=frame*0.16f&&frame<8)
                    {Capture(camera,new Vector3(-2.8f,1.35f,6.4f),actor.transform.position+Vector3.up*0.95f,"walk-"+frame+".png");frame++;}
                }
                ownBody.SetModelVisible(true);
                bool torso=false,pelvis=false;
                foreach(var renderer in ownBody.GetComponentsInChildren<MeshRenderer>())
                {
                    if(renderer.name=="Chest")torso=renderer.shadowCastingMode!=UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    if(renderer.name=="Body")pelvis=renderer.shadowCastingMode!=UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
                Capture(camera,player.transform.position+Vector3.up*1.65f,player.transform.position+new Vector3(0,0.1f,0.14f),"first-person-steep-down.png");
                bool passed=torso&&pelvis&&maxKnee-minKnee>25&&frame==8;
                File.WriteAllText(Path.Combine(output,"animation.txt"),"Visible torso: "+torso+"\nVisible pelvis: "+pelvis+"\nKnee range: "+minKnee+" .. "+maxKnee+"\nCaptured poses: "+frame+"\nPassed: "+passed+"\nVisual samples, not a foot-contact or frame-time benchmark.\n");
                Application.Quit(passed?0:2);yield break;
            }
            if(Array.IndexOf(Environment.GetCommandLineArgs(),"--performance-check")>=0)
            {yield return PrisonPerformanceCheck.Run(camera,output);yield break;}
            if(Array.IndexOf(Environment.GetCommandLineArgs(),"--circulation-check")>=0)
            {
                yield return PrisonCirculationCheck.Run(player,population,output);
                yield break;
            }
            if(Array.IndexOf(Environment.GetCommandLineArgs(),"--visual-only")>=0)
            {
                File.WriteAllText(Path.Combine(output,"capture.txt"),"Visual-only capture from standalone build; no gameplay checks requested.\n");
                Application.Quit(0);yield break;
            }
            if(population!=null)
            {
                var positions=new Vector3[population.Actors.Length];
                for(int i=0;i<positions.Length;i++)positions[i]=population.Actors[i].transform.position;
                yield return new WaitForSecondsRealtime(3);
                var motion="Actor displacement over 3 seconds (metres):\n";
                bool passed=true;
                for(int i=0;i<positions.Length;i++)
                {
                    float distance=Vector3.Distance(positions[i],population.Actors[i].transform.position);
                    motion+=(i+1)+": "+distance.ToString("F3",System.Globalization.CultureInfo.InvariantCulture)+"\n";
                    passed&=distance>0.1f;
                }
                File.WriteAllText(Path.Combine(output,"motion.txt"),motion+"All actors moved: "+passed+"\n");
                Capture(camera,new Vector3(-12.6f,1.7f,4),new Vector3(10,4.8f,4.1f),"cellblock-later.png");
            }
            if(population!=null)
            {
                Time.timeScale=3;
                yield return new WaitForSeconds(45);
                string routes="Normal routes over 45 additional simulated seconds:\n";bool healthy=true;
                foreach(var actor in population.Actors)
                {
                    routes+=actor.name+": waypoints="+actor.Motor.WaypointsReached+", longest block="+actor.Motor.LongestBlock+"\n";
                    healthy&=actor.Motor.WaypointsReached>0&&actor.Motor.LongestBlock<3.5f;
                }
                File.WriteAllText(Path.Combine(output,"routes.txt"),routes+"Routes healthy: "+healthy+"\n");
                Time.timeScale=1;
                // Force the reported failure: two people approaching on precisely the same line.
                foreach(var actor in population.Actors)actor.gameObject.SetActive(false);
                var a=population.Actors[0];var b=population.Actors[1];
                a.transform.position=new Vector3(-3,0.02f,4);b.transform.position=new Vector3(3,0.02f,4);
                a.gameObject.SetActive(true);b.gameObject.SetActive(true);
                a.Motor.SetRoute(new[]{a.transform.position,new Vector3(4,0.02f,4)});
                b.Motor.SetRoute(new[]{b.transform.position,new Vector3(-4,0.02f,4)});
                float minDistance=10;float elapsed=0;bool passed=false;
                while(elapsed<13)
                {
                    yield return null;elapsed+=Time.deltaTime;
                    minDistance=Mathf.Min(minDistance,Vector3.Distance(a.transform.position,b.transform.position));
                    passed|=a.transform.position.x>b.transform.position.x;
                }
                string check="Head-on pass: "+passed+"\nMinimum separation: "+minDistance+"\nLongest block: "+Mathf.Max(a.Motor.LongestBlock,b.Motor.LongestBlock)+"\n";
                File.WriteAllText(Path.Combine(output,"encounter.txt"),check);
                Capture(camera,new Vector3(-1,2.2f,7),new Vector3(0,1,4),"encounter.png");
                if(!passed){Debug.LogError("CROWD_CHECK_FAILED "+check);Application.Quit(2);yield break;}
                Debug.Log("CROWD_CHECK_PASSED "+check);
            }
            File.WriteAllText(Path.Combine(output,"capture.txt"),"Rendered from the standalone visual build. No network or microphone test.\n");
            Application.Quit(0);
        }
        private void Capture(Camera camera,Vector3 position,Vector3 target,string name)
        {
            camera.transform.position=position;camera.transform.LookAt(target);
            SaveImage(camera,Path.Combine(output,name));
        }
        public static void SaveImage(Camera camera,string path)
        {
            var rt=new RenderTexture(1600,1000,24){antiAliasing=4};var previous=RenderTexture.active;camera.targetTexture=rt;camera.Render();RenderTexture.active=rt;
            var texture=new Texture2D(1600,1000,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,1600,1000),0,0);texture.Apply();
            File.WriteAllBytes(path,texture.EncodeToPNG());camera.targetTexture=null;RenderTexture.active=previous;rt.Release();Destroy(rt);Destroy(texture);
        }
    }
}
