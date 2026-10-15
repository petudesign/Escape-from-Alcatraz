using System.Collections;
using System.IO;
using UnityEngine;

namespace PrisonCoop
{
    // Standalone regression check: drive the player's real capsule through stairs and circulation.
    public static class PrisonCirculationCheck
    {
        public static IEnumerator Run(PrototypePlayer player,PrisonPopulation population,string output)
        {
            string sight="Geometry sightlines to the lower and upper stair flights:\n";
            bool visible=true;
            for(int i=0;i<2;i++)
            {
                var guard=population.Guards[i];var origin=guard.transform.position+Vector3.up*1.6f;
                var target=new Vector3(-16.65f,i*3.35f+1.7f,3.2f);var delta=target-origin;
                bool clear=true;
                foreach(var hit in Physics.RaycastAll(origin,delta.normalized,delta.magnitude))
                    if(hit.collider.name!="Stair safety panel"&&hit.collider.name!="Gallery safety barrier"&&hit.collider.GetComponent<GuardPost>()==null&&hit.collider.GetComponent<PrototypePlayer>()==null)
                    {clear=false;sight+="  occluder: "+hit.collider.name+"\n";}
                sight+="Guard "+i+": "+clear+"\n";visible&=clear;
            }
            File.WriteAllText(Path.Combine(output,"guard-sightlines.txt"),sight+"Both sightlines clear: "+visible+"\n");
            foreach(var actor in population.Actors)actor.gameObject.SetActive(false);
            foreach(var guard in population.Guards)guard.gameObject.SetActive(false);
            var controller=player.GetComponent<CharacterController>();
            var points=new[]{
                P(-1.45f,0,0.8f),P(-18,0,0.8f),P(-16.65f,0,0.8f),
                P(-16.65f,1.675f,6.1f),P(-19.05f,1.675f,6.1f),P(-19.05f,3.35f,1.1f),
                P(-16.65f,3.35f,0.8f),P(-16.65f,5.025f,6.1f),P(-19.05f,5.025f,6.1f),P(-19.05f,6.7f,1.1f),
                P(-14.8f,6.7f,0.8f),P(16.6f,6.7f,0.8f),P(16.6f,6.7f,7.2f),P(-14.8f,6.7f,7.2f),P(-14.8f,6.7f,0.8f),
                P(-19.05f,6.7f,0.8f),P(-19.05f,5.025f,6.1f),P(-16.65f,5.025f,6.1f),P(-16.65f,3.35f,1.1f),
                P(-19.05f,3.35f,0.8f),P(-19.05f,1.675f,6.1f),P(-16.65f,1.675f,6.1f),P(-16.65f,0,1.1f),
                P(-18,0,0.8f),P(-12.5f,0,0.8f),P(-12.5f,0,4),P(19.5f,0,4),P(24,0,4),P(24,0,12),P(24,0,18),P(32,0,28),
                P(24,0,18),P(24,0,4),P(24,0,-13.5f)
            };
            Time.timeScale=4;float vertical=0;string log="Real player capsule circulation check; input disabled, collisions enabled.\n";
            for(int i=0;i<points.Length;i++)
            {
                float elapsed=0;float budget=Vector3.Distance(player.transform.position,points[i])/2.5f+10;
                bool reached=false;
                while(elapsed<budget)
                {
                    Vector3 delta=points[i]-player.transform.position;Vector3 horizontal=new Vector3(delta.x,0,delta.z);
                    if(horizontal.magnitude<0.18f&&Mathf.Abs(delta.y)<0.24f){reached=true;break;}
                    if(controller.isGrounded&&vertical<0)vertical=-2;
                    vertical+=Physics.gravity.y*Time.deltaTime;
                    controller.Move((horizontal.normalized*3+Vector3.up*vertical)*Time.deltaTime);
                    elapsed+=Time.deltaTime;yield return null;
                }
                log+="Waypoint "+i+" "+points[i]+": "+reached+" actual="+player.transform.position+"\n";
                if(!reached)
                {
                    File.WriteAllText(Path.Combine(output,"circulation.txt"),log+"CIRCULATION FAILED\n");Debug.LogError(log);Time.timeScale=1;Application.Quit(3);yield break;
                }
            }
            Time.timeScale=1;File.WriteAllText(Path.Combine(output,"circulation.txt"),log+"CIRCULATION PASSED\n");Debug.Log("CIRCULATION_PASSED");Application.Quit(0);
        }
        private static Vector3 P(float x,float y,float z)=>new Vector3(x,y,z);
    }
}
