using UnityEngine;

namespace PrisonCoop
{
    public sealed partial class PrisonEnvironment
    {
        private Material oldPaint,plasterRepair,greenRepair,shoeRub,concreteRepair,scuffedIron,wornWood,floorRub;
        private void PrepareWearMaterials()
        {
            oldPaint=Mat("Exposed older paint",new Color(0.46f,0.49f,0.40f));
            plasterRepair=Mat("Plaster touch up",new Color(0.80f,0.79f,0.68f));
            greenRepair=Mat("Sage touch up",new Color(0.47f,0.55f,0.44f));
            shoeRub=Mat("Rubber contact marks",new Color(0.29f,0.31f,0.27f));
            floorRub=Mat("Faded floor scuffs",new Color(0.41f,0.40f,0.36f));
            concreteRepair=Mat("Concrete repair",new Color(0.43f,0.43f,0.39f));
            scuffedIron=Mat("Exposed steel at contact points",new Color(0.36f,0.36f,0.31f));
            wornWood=Mat("Worn timber edges",new Color(0.43f,0.33f,0.22f));
        }
        private void AddCellWear(Transform root,int seed)
        {
            // The side of a bed, a door jamb and wet fittings have different wear stories.
            for(int i=0;i<5;i++)
            {
                float z=-0.3f-Hash(seed+i*11)*3.2f;
                float y=0.18f+Hash(seed+i*17+3)*0.95f;
                Patch(root,"Scuffed old wall paint",new Vector3(1.404f,y,z),Quaternion.Euler(0,-90,0),
                    new Vector2(0.08f+Hash(seed+i)*0.20f,0.025f+Hash(seed+i+6)*0.06f),oldPaint,seed+i);
            }
            for(int i=0;i<3;i++)
            {
                Patch(root,"Bed-side paint abrasion",new Vector3(-1.404f,0.60f+i*0.035f,-2.1f+Hash(seed+i)*0.4f),Quaternion.Euler(0,90,0),new Vector2(0.32f,0.025f),oldPaint,seed+i*8);
                Patch(root,"Door jamb chips",new Vector3(i%2==0?-1.42f:1.42f,0.35f+i*0.5f+Hash(seed+i)*0.15f,0.145f),Quaternion.identity,
                    new Vector2(0.027f,0.065f),oldPaint,seed+i+22);
                Patch(root,"Boot marks below wall table",new Vector3(1.403f,0.15f+i*0.06f,-0.62f),Quaternion.Euler(0,-90,0),new Vector2(0.18f,0.012f),shoeRub,seed+i);
            }
            if(seed%3==0)
                Patch(root,"Old plaster repair",new Vector3(-0.55f,1.55f,-3.891f),Quaternion.identity,new Vector2(0.24f,0.34f),plasterRepair,seed+51);
            if(seed%4==1)
                Patch(root,"Local repaint",new Vector3(1.402f,0.76f,-2.75f),Quaternion.Euler(0,-90,0),new Vector2(0.32f,0.21f),greenRepair,seed+5);
            for(int i=0;i<5;i++)
            {
                Patch(root,"Chair and shoe floor scrape",new Vector3(0.45f+Hash(seed+i)*0.32f,0.006f,-0.6f-Hash(seed+i+5)*1.7f),Quaternion.Euler(-90,0,0),new Vector2(0.006f,0.10f+Hash(seed+i)*0.13f),floorRub,seed+i+33);
            }
            // Small exposed edges; no pervasive orange corrosion on a maintained gate.
            foreach(float y in new[]{1.14f,1.22f})
                Box(root,"Gate contact metal",new Vector3(-0.79f,y,0.026f),new Vector3(0.036f,0.05f,0.009f),scuffedIron,false);
            Box(root,"Table front edge abrasion",new Vector3(0.98f,0.905f,-0.363f),new Vector3(0.39f,0.012f,0.004f),wornWood,false);
            Patch(root,"Old damp mark under basin",new Vector3(-0.30f,0.51f,-3.889f),Quaternion.identity,new Vector2(0.06f,0.19f),greenRepair,seed+12);
        }
        private void AddHallWear()
        {
            for(int i=0;i<10;i++)
            {
                float x=-8.2f+i*1.75f;
                for(int j=0;j<3;j++)
                    Patch(transform,"Faint heel scrape",new Vector3(x+Hash(i*19+j)*0.4f,0.007f,2.0f+Hash(i*29+j)*3.9f),Quaternion.Euler(-90,Hash(i+j)*35,0),new Vector2(0.008f,0.08f),floorRub,i*31+j);
            }
            Patch(transform,"Old concrete infill",new Vector3(7.6f,0.005f,3.2f),Quaternion.Euler(-90,0,0),new Vector2(0.44f,0.65f),concreteRepair,64);
            // A small, repaired hairline, rather than widespread structural failure.
            for(int i=0;i<5;i++)
                Patch(transform,"Repaired floor hairline",new Vector3(-4.4f+i*0.13f,0.006f,4.5f+i*0.23f),Quaternion.Euler(-90,25,0),new Vector2(0.007f,0.15f),concreteRepair,90+i);
            Patch(transform,"Repainted wall near yard entrance",new Vector3(17.869f,1.72f,2.0f),Quaternion.Euler(0,-90,0),new Vector2(0.25f,0.32f),plasterRepair,145);
        }
        private static float Hash(int seed)=>Mathf.Repeat(Mathf.Sin(seed*127.1f+19.7f)*43758.5453f,1);
        private void Patch(Transform root,string name,Vector3 centre,Quaternion rotation,Vector2 radius,Material material,int seed)
        {
            const int count=17;
            var vertices=new Vector3[count+1];var uv=new Vector2[count+1];var triangles=new int[count*3];
            vertices[0]=Vector3.zero;uv[0]=Vector2.one*0.5f;
            for(int i=0;i<count;i++)
            {
                float angle=i*Mathf.PI*2/count;
                float irregular=0.52f+Hash(seed+i*7)*0.48f;
                Vector2 edge=new Vector2(Mathf.Cos(angle),Mathf.Sin(angle))*irregular;
                vertices[i+1]=new Vector3(edge.x*radius.x,edge.y*radius.y,0);uv[i+1]=edge*0.5f+Vector2.one*0.5f;
                triangles[i*3]=0;triangles[i*3+1]=i+1;triangles[i*3+2]=(i+1)%count+1;
            }
            var mesh=new Mesh{name=name};mesh.vertices=vertices;mesh.uv=uv;mesh.triangles=triangles;mesh.RecalculateNormals();mesh.RecalculateBounds();
            var go=new GameObject(name);go.transform.SetParent(root,false);go.transform.localPosition=centre;go.transform.localRotation=rotation;
            go.AddComponent<MeshFilter>().sharedMesh=mesh;var renderer=go.AddComponent<MeshRenderer>();renderer.sharedMaterial=material;renderer.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;merge.Add(go);
        }
    }
}
