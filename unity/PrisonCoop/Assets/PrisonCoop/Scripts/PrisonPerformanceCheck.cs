using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;
namespace PrisonCoop
{
    public static class PrisonPerformanceCheck
    {
        public static IEnumerator Run(Camera camera,string output)
        {
            Application.targetFrameRate=-1;QualitySettings.vSyncCount=0;
            // Same resolution and render path for every view, no image readback in the timed interval.
            camera.enabled=false;
            var target=new RenderTexture(1440,900,24){antiAliasing=4};camera.targetTexture=target;
            var points=new[]{new Vector3(-10,1.65f,4),new Vector3(-17,2,1),new Vector3(24,1.65f,-3),new Vector3(24,1.65f,18)};
            var looks=new[]{new Vector3(10,4,4),new Vector3(-18,3,5),new Vector3(30,1.7f,-13),new Vector3(33,2,34)};
            string report="Standalone 1440x900, MSAA4; uncapped manual render + frame intervals. Not a GPU profiler or release benchmark.\n";
            for(int i=0;i<points.Length;i++)
            {
                camera.transform.position=points[i];camera.transform.LookAt(looks[i]);
                float warm=Time.realtimeSinceStartup+1;while(Time.realtimeSinceStartup<warm){camera.Render();yield return null;}
                var values=new List<float>();float until=Time.realtimeSinceStartup+3;
                while(Time.realtimeSinceStartup<until){camera.Render();yield return null;values.Add(Time.unscaledDeltaTime*1000);}
                values.Sort();report+="View "+i+": median "+values[values.Count/2].ToString("F2")+" ms, p95 "+values[Mathf.Min(values.Count-1,(int)(values.Count*0.95f))].ToString("F2")+" ms; samples="+values.Count+"\n";
            }
            report+="Allocated Unity memory: "+(Profiler.GetTotalAllocatedMemoryLong()/1048576)+" MiB\n";
            report+="Mesh renderers in scene: "+Object.FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None).Length+" (not visible draw calls).\n";
            File.WriteAllText(Path.Combine(output,"performance.txt"),report);camera.targetTexture=null;target.Release();Object.Destroy(target);Application.Quit(0);
        }
    }
}
