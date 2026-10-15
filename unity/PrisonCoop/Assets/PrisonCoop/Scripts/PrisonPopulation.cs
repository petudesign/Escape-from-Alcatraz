using UnityEngine;

namespace PrisonCoop
{
    public sealed class PrisonPopulation : MonoBehaviour
    {
        public Shader PaintShader;
        public PrisonerAppearance[] Profiles;
        public WalkingPrisoner[] Actors { get; private set; }
        public GuardPost[] Guards { get; private set; }
        private void Start()
        {
            var material=new Material(PaintShader){name="Prisoner vertex paint"};
            Actors=new WalkingPrisoner[12];
            Vector3[][] paths={
                new[]{P(-5,2.2f),P(5,2.2f),P(5,5.6f),P(-5,5.6f)},
                new[]{P(3,5.5f),P(-6,5.5f),P(-6,3.4f),P(3,3.4f)},
                new[]{P(0,3.8f),P(6.5f,3.8f),P(6.5f,5.6f),P(0,5.6f)},
                new[]{P(-3,5.8f),P(4,5.8f),P(4,2.5f),P(-3,2.5f)},
                new[]{new Vector3(-5,3.35f,0.72f),new Vector3(6,3.35f,0.72f)},
                new[]{P(21,20),P(40,20),P(40,30),P(21,30)},
                new[]{P(37,33),P(20,33),P(20,25),P(37,25)},
                new[]{new Vector3(-10,6.7f,7.2f),new Vector3(10,6.7f,7.2f)},
                new[]{P(-12,2.5f),P(-4,2.5f),P(-4,5.2f),P(-12,5.2f)},
                new[]{new Vector3(-8,3.35f,7.2f),new Vector3(9,3.35f,7.2f)},
                new[]{P(19,22),P(19,32),P(28,32),P(28,22)},
                new[]{P(-10,5.2f),P(11,5.2f),P(11,3.5f),P(-10,3.5f)}
            };
            for(int i=0;i<Actors.Length;i++)
            {
                var root=new GameObject("Prisoner "+(i+1));
                root.transform.SetParent(transform,false);root.transform.position=paths[i][0];
                var actor=root.AddComponent<WalkingPrisoner>();
                actor.Initialize(Profiles[i%Profiles.Length],material,paths[i],0.70f+(i%3)*0.12f,i*1.9f,i);
                Actors[i]=actor;
            }
            Guards=new GuardPost[3];
            Vector3[] posts={P(-14,4.5f),new Vector3(-14.8f,3.35f,4),P(20.2f,6.65f)};
            float[] headings={-90,-90,155};
            for(int i=0;i<Guards.Length;i++)
            {
                var profile=ScriptableObject.CreateInstance<PrisonerAppearance>();profile.Face=i;profile.IsGuard=true;profile.Height=1.86f-i*0.035f;
                profile.Width=1.04f+i*0.04f;profile.FaceWidth=1.03f;profile.HairStyle=1;profile.ShirtStyle=1;
                profile.Shirt=new Color(0.22f,0.29f,0.34f);profile.Trousers=new Color(0.13f,0.17f,0.20f);
                var root=new GameObject("Guard post "+(i+1));root.transform.SetParent(transform,false);root.transform.position=posts[i];root.transform.rotation=Quaternion.Euler(0,headings[i],0);
                var guard=root.AddComponent<GuardPost>();guard.Initialize(profile,material);Guards[i]=guard;
            }
        }
        private static Vector3 P(float x,float z)=>new Vector3(x,0.02f,z);
    }
}
