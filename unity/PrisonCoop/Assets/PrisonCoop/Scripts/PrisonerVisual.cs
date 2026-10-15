using System.Collections.Generic;
using UnityEngine;

namespace PrisonCoop
{
    // Art/motion prototype: articulated rigid pieces, authored routes, no conversation model.
    public sealed class PrisonerVisual : MonoBehaviour
    {
        private float phase,blend;
        private Transform body,head,chest;
        private readonly Transform[] feet=new Transform[2];
        private Vector3 travel;
        public void SetTravel(Vector3 localVelocity){travel=localVelocity;}
        private readonly Transform[] thighs=new Transform[2],shins=new Transform[2],arms=new Transform[2],forearms=new Transform[2];
        private Material material;
        private Color skin,hair,shirt,pants,shoes;
        private int variant;
        private float baseWidth;
        private PrisonerAppearance appearance;
        private bool playerPose;
        public Transform Head=>head;
        public void SetPlayerPose(bool local)
        {
            playerPose=local;
            if(local)
            {
                head.GetComponent<MeshRenderer>().shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

            }
        }
        public void SetLookPitch(float pitch){head.localRotation=Quaternion.Euler(pitch,0,0);}

        public void Initialize(PrisonerAppearance style,Material paint,float offset)
        {
            appearance=style;variant=style.Face;material=paint;phase=offset;
            baseWidth=style.Width;skin=style.Skin;hair=style.Hair;shirt=style.Shirt;pants=style.Trousers;shoes=style.Shoes;
            body=Joint(transform,"Body",Vector3.zero);
            body.localScale=new Vector3(style.Height/2*baseWidth,style.Height/2,style.Height/2);
            BuildBody();
            CombineRigidParts(body);
        }

        private void BuildBody()
        {
            chest=Joint(body,"Chest",new Vector3(0,1.05f,0));
            Shape(body,"Shirt torso",new Vector3(0,1.30f,0),new[]{-0.30f,-0.25f,-0.13f,0.01f,0.15f,0.23f,0.30f},new[]{0.225f,0.245f,0.237f,0.265f,0.28f,0.275f,0.125f},new[]{0.135f,0.15f,0.145f,0.16f,0.17f,0.14f,0.085f},shirt,8);
            if(appearance.ShirtStyle!=2)
            {
                Triangle(body,"Open neckline",new Vector3(-0.059f,1.585f,0.113f),new Vector3(0.059f,1.585f,0.113f),new Vector3(0,1.455f,0.17f),skin*0.93f);
            }
            if(appearance.ShirtStyle!=2)
            {
                Triangle(body,"Left collar",new Vector3(-0.065f,1.59f,0.10f),new Vector3(-0.045f,1.46f,0.166f),new Vector3(-0.135f,1.52f,0.146f),shirt*1.05f);
                Triangle(body,"Right collar",new Vector3(0.065f,1.59f,0.10f),new Vector3(0.135f,1.52f,0.146f),new Vector3(0.045f,1.46f,0.166f),shirt*0.95f);
                Box(body,"Chest pocket",new Vector3(-0.13f,1.31f,0.149f),new Vector3(0.105f,0.12f,0.008f),shirt*0.94f);
                for(int button=0;button<3;button++)Box(body,"Shirt button",new Vector3(0,1.34f-button*0.11f,0.163f),new Vector3(0.014f,0.014f,0.006f),shirt*0.82f);
            }
            if(appearance.IsGuard)
            {
                Shape(body,"Guard badge",new Vector3(0.15f,1.38f,0.175f),new[]{-0.038f,0.038f},new[]{0.025f,0.032f},new[]{0.009f,0.009f},new Color(0.65f,0.55f,0.30f),6);
                Box(body,"Duty pouch",new Vector3(0.26f,0.94f,0),new Vector3(0.12f,0.18f,0.10f),pants*0.5f);
            }
            Shape(body,"Trouser seat",new Vector3(0,0.94f,0),new[]{-0.14f,0.08f},new[]{0.24f,0.235f},new[]{0.13f,0.145f},pants,8);
            Box(body,"Belt",new Vector3(0,0.99f,0),new Vector3(0.455f,0.035f,0.275f),pants*0.65f);
            Box(body,"Buckle",new Vector3(0,0.99f,0.156f),new Vector3(0.045f,0.031f,0.009f),new Color(0.39f,0.39f,0.34f));
            Shape(body,"Neck",new Vector3(0,1.61f,0),new[]{-0.075f,0.09f},new[]{0.072f,0.075f},new[]{0.075f,0.072f},skin,6);
            head=Joint(body,"Head",new Vector3(0,1.77f,0.015f));
            head.localScale=new Vector3(1,appearance.HeadHeight,1);
            float faceWidth=appearance.FaceWidth;
            Shape(head,"Angular face",Vector3.zero,new[]{-0.21f,-0.14f,-0.015f,0.11f,0.22f},new[]{0.095f*faceWidth*appearance.JawWidth,0.145f*faceWidth*appearance.JawWidth,0.17f*faceWidth,0.163f*faceWidth,0.12f*faceWidth},new[]{0.075f,0.105f,0.115f,0.107f,0.08f},skin,8);
            SculptFace();
            foreach(float side in new[]{-1f,1f})
            {
                Shape(head,"Ear",new Vector3(side*0.176f*faceWidth,-0.01f,0.01f),new[]{-0.06f,-0.025f,0.025f,0.055f},new[]{0.022f,0.043f,0.044f,0.023f},new[]{0.023f,0.029f,0.027f,0.018f},skin*0.96f,6);
                Box(head,"Eye socket",new Vector3(side*0.072f,0.027f,0.137f),new Vector3(0.069f,0.028f,0.018f),skin*0.68f);
                Box(head,"Eye",new Vector3(side*0.071f,0.030f,0.149f),new Vector3(0.037f,0.008f,0.004f),new Color(0.75f,0.73f,0.62f));
                Box(head,"Pupil",new Vector3(side*0.069f,0.030f,0.155f),new Vector3(0.014f,0.012f,0.004f),new Color(0.10f,0.10f,0.07f));
                var brow=Box(head,"Heavy eyebrow",new Vector3(side*0.071f,0.065f,0.146f),new Vector3(0.082f,0.033f,0.016f),hair);
                brow.localRotation=Quaternion.Euler(0,0,side*(variant==3?9:4));
            }
            foreach(float side in new[]{-1f,1f})
            {
                Triangle(head,"Cheek plane",new Vector3(side*0.039f,-0.015f,0.116f),new Vector3(side*0.138f,-0.025f,0.088f),new Vector3(side*0.09f,-0.10f,0.103f),skin*0.91f);
                Box(head,"Lower eyelid",new Vector3(side*0.071f,0.014f,0.150f),new Vector3(0.054f,0.009f,0.009f),skin*0.83f);
            }
            Nose();
            Box(head,"Mouth",new Vector3(0,-0.113f,0.142f),new Vector3(0.082f,0.009f,0.005f),skin*0.53f);
            Box(head,"Lower lip",new Vector3(0,-0.129f,0.143f),new Vector3(0.075f,0.009f,0.005f),skin*0.86f);
            if(appearance.Beard>=2)
            {
                Box(head,"Moustache",new Vector3(0,-0.085f,0.15f),new Vector3(0.104f,0.028f,0.021f),hair);
                if(appearance.Beard==3)Box(head,"Grey chin",new Vector3(0,-0.164f,0.085f),new Vector3(0.12f,0.045f,0.030f),hair);
            }
            else if(appearance.Beard==1) Box(head,"Light stubble",new Vector3(0,-0.172f,0.115f),new Vector3(0.10f,0.020f,0.005f),Color.Lerp(skin,hair,0.25f));
            if(appearance.HairStyle!=4)Shape(head,"Hair cap",new Vector3(0,0.192f,-0.008f),new[]{-0.052f,0.025f,0.084f},new[]{0.158f,0.180f,0.133f},new[]{0.110f,0.132f,0.105f},hair,8);
            int chunks=appearance.HairStyle==4?0:appearance.HairStyle==1?2:appearance.HairStyle==2?1:appearance.HairStyle==3?7:5;
            for(int i=0;i<chunks;i++)
            {
                float x=-0.11f+i*0.22f/Mathf.Max(1,chunks-1);
                var strand=Shape(head,"Swept hair lock",new Vector3(x,0.205f+(i%2)*0.012f,0.045f),new[]{-0.04f,0.025f,0.06f},new[]{0.044f,0.052f,0.022f},new[]{0.09f,0.075f,0.04f},hair*(0.93f+i*0.025f),5);
                strand.localRotation=Quaternion.Euler(8+i*5,0,-14+i*7);
            }
            if(appearance.HairStyle==0||appearance.HairStyle==3)
            {
                var fringe=Shape(head,"Falling fringe",new Vector3(-0.060f,0.14f,0.11f),new[]{-0.075f,0.01f,0.065f},new[]{0.024f,0.046f,0.057f},new[]{0.017f,0.026f,0.029f},hair,5);
                fringe.localRotation=Quaternion.Euler(0,0,-18);
            }
            if(appearance.IsGuard)
            {
                Shape(head,"Uniform cap",new Vector3(0,0.245f,0),new[]{-0.035f,0.035f},new[]{0.18f,0.19f},new[]{0.125f,0.13f},shirt*0.65f,8);
                Box(head,"Cap peak",new Vector3(0,0.215f,0.14f),new Vector3(0.30f,0.026f,0.17f),pants*0.6f);
            }
            var upper=new List<Transform>();
            foreach(Transform part in body)
                if(part!=chest && part.name!="Trouser seat" && part.name!="Belt" && part.name!="Buckle" && part.name!="Duty pouch")upper.Add(part);
            foreach(var part in upper)part.SetParent(chest,true);
            for(int side=0;side<2;side++)
            {
                float sign=side==0?-1:1;
                thighs[side]=Joint(body,"Hip",new Vector3(sign*0.135f,0.98f,0));
                Shape(thighs[side],"Trousers thigh",new Vector3(0,-0.22f,0),new[]{-0.23f,-0.17f,0.02f,0.22f},new[]{0.086f,0.09f,0.112f,0.12f},new[]{0.088f,0.10f,0.12f,0.115f},pants,8);
                shins[side]=Joint(thighs[side],"Knee",new Vector3(0,-0.44f,0));
                Shape(shins[side],"Trousers shin",new Vector3(0,-0.22f,0),new[]{-0.22f,-0.12f,0.08f,0.23f},new[]{0.081f,0.078f,0.091f,0.086f},new[]{0.083f,0.079f,0.095f,0.088f},pants,8);
                Box(shins[side],"Trouser cuff",new Vector3(0,-0.415f,0),new Vector3(0.17f,0.035f,0.18f),pants*1.12f);
                feet[side]=Joint(shins[side],"Ankle",new Vector3(0,-0.44f,0));
                Shape(shins[side],"Leather shoe",new Vector3(0,-0.48f,0.065f),new[]{-0.055f,0.01f,0.06f},new[]{0.095f,0.098f,0.071f},new[]{0.17f,0.17f,0.105f},shoes,8);
                Box(shins[side],"Shoe vamp",new Vector3(0,-0.44f,0.08f),new Vector3(0.12f,0.015f,0.14f),shoes*0.8f);
                Box(shins[side],"Shoe sole",new Vector3(0,-0.54f,0.065f),new Vector3(0.20f,0.025f,0.33f),shoes*0.6f);
                var shoeParts=new List<Transform>();
                foreach(Transform part in shins[side])if(part.name=="Leather shoe"||part.name=="Shoe vamp"||part.name=="Shoe sole")shoeParts.Add(part);
                foreach(var part in shoeParts)part.SetParent(feet[side],true);
                arms[side]=Joint(chest,"Shoulder",new Vector3(sign*0.275f,0.41f,0));
                Shape(arms[side],"Shirt sleeve",new Vector3(sign*0.025f,-0.15f,0),new[]{-0.16f,-0.10f,0.02f,0.12f,0.19f},new[]{0.077f,0.088f,0.095f,0.09f,0.055f},new[]{0.075f,0.085f,0.095f,0.085f,0.05f},shirt,6);
                Shape(arms[side],"Rolled sleeve",new Vector3(sign*0.025f,-0.285f,0),new[]{-0.035f,0,0.035f},new[]{0.083f,0.09f,0.08f},new[]{0.08f,0.087f,0.08f},shirt*1.06f,8);
                forearms[side]=Joint(arms[side],"Elbow",new Vector3(sign*0.025f,-0.31f,0));
                Shape(forearms[side],"Forearm",new Vector3(0,-0.145f,0),new[]{-0.145f,0.145f},new[]{0.041f,0.064f},new[]{0.044f,0.068f},skin,6);
                Shape(forearms[side],"Palm",new Vector3(0,-0.31f,0.015f),new[]{-0.075f,0.025f,0.07f},new[]{0.035f,0.053f,0.038f},new[]{0.028f,0.034f,0.025f},skin,6);
                for(int finger=0;finger<4;finger++){var digit=Shape(forearms[side],"Curled finger",new Vector3(-0.034f+finger*0.022f,-0.382f,0.025f),new[]{-0.039f,-0.012f,0.028f},new[]{0.008f,0.011f,0.012f},new[]{0.013f,0.017f,0.015f},skin*(0.97f+finger*0.012f),5);digit.localRotation=Quaternion.Euler(-24,0,(finger-1.5f)*3);}
                Box(forearms[side],"Thumb",new Vector3(-sign*0.058f,-0.32f,0.025f),new Vector3(0.038f,0.068f,0.046f),skin*0.94f);
            }
        }

        public void Animate(float velocity,float strideSpeed)
        {
            float dt=Mathf.Min(Time.deltaTime,0.05f);
            // Distance-based cadence: slowing down actually slows the steps, rather than shuffling at full cadence.
            float speed=Mathf.Max(0,velocity);
            blend=Mathf.Lerp(blend,Mathf.Clamp01(speed/0.65f),1-Mathf.Exp(-dt*9));
            float stride=Mathf.Lerp(0.82f,1.45f,Mathf.Clamp01(speed/3f))*appearance.Height/1.8f;
            phase+=dt*speed/stride*Mathf.PI*2;
            float amplitude=Mathf.Clamp01(speed/1.25f)*blend;
            float sway=Mathf.Sin(phase);
            float breath=Mathf.Sin(Time.time*1.6f+variant*0.8f);
            Vector3 direction=travel.sqrMagnitude>0.01f?travel.normalized:Vector3.forward;
            float forward=direction.z,sideways=direction.x;
            for(int i=0;i<2;i++)
            {
                float t=phase+i*Mathf.PI;
                float swing=Mathf.Sin(t);
                float lift=Mathf.Pow(Mathf.Max(0,Mathf.Cos(t)),2)*amplitude;
                float hip=-swing*27*amplitude*forward;
                float knee=lift*48+3*blend;
                thighs[i].localRotation=Quaternion.Euler(hip,0,swing*20*amplitude*sideways);
                shins[i].localRotation=Quaternion.Euler(knee,0,0);
                // Counter-rotate the ankle through support, then release the heel during swing.
                feet[i].localRotation=Quaternion.Euler(-hip-knee+lift*12,0,0);
                float sign=i==0?-1:1;
                arms[i].localRotation=Quaternion.Euler(swing*19*amplitude*forward-3,0,-sign*(5+Mathf.Abs(sideways)*3*blend));
                forearms[i].localRotation=Quaternion.Euler(-10-9*lift,0,0);
            }
            // Keep the lowest sole on the flat support plane as the leg extends.
            // This corrects floating from rigid-joint arcs; terrain IK remains a separate future feature.
            float soleY=float.PositiveInfinity;
            foreach(var foot in feet)
                for(int corner=0;corner<2;corner++)
                    soleY=Mathf.Min(soleY,body.InverseTransformPoint(foot.TransformPoint(new Vector3(0,-0.1125f,corner==0?-0.10f:0.23f))).y);
            body.localPosition=new Vector3(sway*0.012f*amplitude,-soleY*body.localScale.y,playerPose?-0.22f:0);
            body.localRotation=Quaternion.Euler(0,0,-sway*1.5f*amplitude);
            chest.localRotation=Quaternion.Euler(appearance.Posture+speed*0.6f+breath*0.3f,-sway*3.5f*amplitude,sway*1.1f*amplitude);
            head.localRotation=Quaternion.Euler(-appearance.Posture*0.4f+breath*0.5f,Mathf.Sin(Time.time*0.4f+variant)*3+sway*1.5f*amplitude,0);
        }

        private void CombineRigidParts(Transform joint)
        {
            // One mesh per moving joint, instead of one renderer for every eyebrow, finger or button.
            var combine=new List<CombineInstance>();var pieces=new List<GameObject>();var joints=new List<Transform>();
            foreach(Transform child in joint)
            {
                var filter=child.GetComponent<MeshFilter>();
                if(filter!=null){combine.Add(new CombineInstance{mesh=filter.sharedMesh,transform=joint.worldToLocalMatrix*child.localToWorldMatrix});pieces.Add(child.gameObject);}
                else joints.Add(child);
            }
            if(combine.Count>0)
            {
                var mesh=new Mesh{name=joint.name+" rigid mesh"};mesh.CombineMeshes(combine.ToArray());
                joint.gameObject.AddComponent<MeshFilter>().sharedMesh=mesh;joint.gameObject.AddComponent<MeshRenderer>().sharedMaterial=material;
                foreach(var piece in pieces){piece.SetActive(false);Destroy(piece.GetComponent<MeshFilter>().sharedMesh);Destroy(piece);}
            }
            foreach(var child in joints)CombineRigidParts(child);
        }

        private void OnDestroy()
        {
            foreach(var filter in GetComponentsInChildren<MeshFilter>(true))if(filter.sharedMesh!=null)Destroy(filter.sharedMesh);
        }
        private Transform Joint(Transform root,string name,Vector3 point)
        {var go=new GameObject(name);go.transform.SetParent(root,false);go.transform.localPosition=point;return go.transform;}
        private Transform Box(Transform root,string name,Vector3 p,Vector3 size,Color color)
            =>Shape(root,name,p,new[]{-size.y/2,size.y/2},new[]{size.x*0.707107f,size.x*0.707107f},new[]{size.z*0.707107f,size.z*0.707107f},color,4,Mathf.PI/4);
        private Transform Shape(Transform root,string name,Vector3 p,float[] ys,float[] xs,float[] zs,Color color,int sides,float angle=0)
        {
            var vertices=new List<Vector3>();var colors=new List<Color>();var triangles=new List<int>();
            for(int r=0;r<ys.Length-1;r++)for(int s=0;s<sides;s++)
            {
                Vector3 a=Ring(r,s),b=Ring(r,(s+1)%sides),c=Ring(r+1,(s+1)%sides),d=Ring(r+1,s);
                Add(a,b,c);Add(a,c,d);
            }
            for(int s=0;s<sides;s++){Add(new Vector3(0,ys[0],0),Ring(0,(s+1)%sides),Ring(0,s));int last=ys.Length-1;Add(new Vector3(0,ys[last],0),Ring(last,s),Ring(last,(s+1)%sides));}
            var go=Joint(root,name,p);var mesh=new Mesh{name=name};mesh.SetVertices(vertices);mesh.SetColors(colors);mesh.SetTriangles(triangles,0);mesh.RecalculateNormals();mesh.RecalculateBounds();
            go.gameObject.AddComponent<MeshFilter>().sharedMesh=mesh;go.gameObject.AddComponent<MeshRenderer>().sharedMaterial=material;return go;
            Vector3 Ring(int r,int s){float a=angle+s*Mathf.PI*2/sides;return new Vector3(Mathf.Sin(a)*xs[r],ys[r],Mathf.Cos(a)*zs[r]);}
            void Add(Vector3 a,Vector3 b,Vector3 c)
            {
                int start=vertices.Count;vertices.Add(a);vertices.Add(b);vertices.Add(c);triangles.Add(start);triangles.Add(start+1);triangles.Add(start+2);
                float shade=0.94f+((start*13+variant*7)%17)*0.008f;Color paint=color*shade;paint.a=1;colors.Add(paint);colors.Add(paint);colors.Add(paint);
            }
        }
        private void Triangle(Transform root,string name,Vector3 a,Vector3 b,Vector3 c,Color color)
        {
            var go=Joint(root,name,Vector3.zero);var mesh=new Mesh{name=name};mesh.vertices=new[]{a,b,c,a,c,b};mesh.triangles=new[]{0,1,2,3,4,5};mesh.colors=new[]{color,color,color,color,color,color};mesh.RecalculateNormals();
            go.gameObject.AddComponent<MeshFilter>().sharedMesh=mesh;go.gameObject.AddComponent<MeshRenderer>().sharedMaterial=material;
        }
        private void SculptFace()
        {
            float[] ys={-0.205f,-0.16f,-0.105f,-0.04f,0.025f,0.085f,0.15f,0.205f};
            float[] widths={0.075f,0.108f,0.13f,0.157f,0.163f,0.158f,0.149f,0.11f};
            float[] depths={0.085f,0.12f,0.139f,0.145f,0.14f,0.133f,0.119f,0.09f};
            for(int row=0;row<ys.Length-1;row++)for(int col=0;col<4;col++)
            {
                var a=Point(row,col);var b=Point(row,col+1);var c=Point(row+1,col+1);var d=Point(row+1,col);
                Color tone=skin*(0.94f+((row*7+col*3+variant)%7)*0.015f);
                if(row<2)tone=Color.Lerp(tone,hair,appearance.Beard==0?0.04f:0.13f);
                Triangle(head,"Facial plane",a,b,c,tone);Triangle(head,"Facial plane",a,c,d,tone*1.015f);
            }
            Vector3 Point(int row,int col)
            {
                float u=(col-2)*0.5f;
                float jaw=row<3?appearance.JawWidth:1;
                float z=depths[row]-Mathf.Pow(Mathf.Abs(u),2)*0.055f;
                return new Vector3(u*widths[row]*appearance.FaceWidth*jaw,ys[row],z);
            }
        }

        private void Nose()
        {
            Vector3 a=new Vector3(-0.024f,0.07f,0.14f),b=new Vector3(0.024f,0.07f,0.14f),c=new Vector3(0.037f,-0.050f,0.15f),d=new Vector3(-0.037f,-0.050f,0.15f),tip=new Vector3(0,-0.025f,0.145f+0.066f*appearance.NoseLength);
            Triangle(head,"Nose plane",a,b,tip,skin*1.04f);Triangle(head,"Nose plane",b,c,tip,skin*0.91f);Triangle(head,"Nose plane",c,d,tip,skin*0.77f);Triangle(head,"Nose plane",d,a,tip,skin*0.96f);
        }
    }
}
