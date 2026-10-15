using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace PrisonCoop
{
    public sealed partial class PrisonEnvironment : MonoBehaviour
    {
        public Texture2D PlasterTexture;
        public Shader SurfaceShader;
        public Shader SkyShader;
        public Shader FilmShader;
        public Shader CharacterShader;
        public Shader SignShader;
        public PrisonerAppearance[] CharacterProfiles;
        private Material cream, green, floor, iron, rust, wood, cloth, white, dark, paper, glow, glass, sea;
        private readonly List<GameObject> merge = new List<GameObject>();
        private Font font;

        private void Awake()
        {
            if (transform.childCount == 0) Build();
            ConfigureLight();
            SpawnPlayer();
            var population=gameObject.AddComponent<PrisonPopulation>();population.PaintShader=CharacterShader;population.Profiles=CharacterProfiles;
            gameObject.AddComponent<PrototypeNetwork>();
        }

        public void Build()
        {
            font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            cream = Mat("Old ivory plaster", new Color(0.91f, 0.87f, 0.75f), PlasterTexture);
            green = Mat("Faded sage paint", new Color(0.43f, 0.53f, 0.43f), PlasterTexture);
            floor = Mat("Stone concrete", new Color(0.47f, 0.46f, 0.41f), NoiseTexture());
            iron = Mat("Painted iron", new Color(0.16f, 0.20f, 0.19f));
            rust = Mat("Weathered metal", new Color(0.33f, 0.27f, 0.20f), PlasterTexture);
            wood = Mat("Dark worn wood", new Color(0.34f, 0.24f, 0.15f));
            cloth = Mat("Blanket brown", new Color(0.37f, 0.29f, 0.23f));
            white = Mat("Ivory porcelain and linen", new Color(0.87f, 0.86f, 0.76f));
            dark = Mat("Dark recess", new Color(0.08f, 0.10f, 0.09f));
            paper = Mat("Paper", new Color(0.78f, 0.72f, 0.56f));
            glow = Mat("Warm lamps", new Color(1, 0.83f, 0.47f));
            glow.EnableKeyword("_EMISSION"); glow.SetColor("_EmissionColor", new Color(1.2f, 0.7f, 0.25f));
            glass = Mat("Blue frosted glass", new Color(0.53f, 0.65f, 0.65f));
            sea = Mat("Bay water", new Color(0.30f, 0.46f, 0.51f));

            PrepareWearMaterials();

            BuildCellBlock();
            Stairs();
            DiningRoom();
            TransferHall();
            Yard();
            AddHallWear();
            CombineStaticMeshes();
        }

        private void Cell(Vector3 position,float yaw,int number,bool playerCell)
        {
            var root = new GameObject("Cell "+number).transform;
            root.SetParent(transform,false); root.localPosition=position; root.localRotation=Quaternion.Euler(0,yaw,0);
            Box(root,"Cell ceiling",new Vector3(0,3.20f,-2),new Vector3(3,0.18f,4),cream);
            if(position.y>0.1f)Box(root,"Cell floor",new Vector3(0,-0.08f,-2),new Vector3(2.98f,0.16f,4),floor);
            foreach(float x in new[]{-1.48f,1.48f}) Wall(root,"Side wall",new Vector3(x,0,-2),new Vector3(0.14f,3.1f,4));
            Wall(root,"Back wall",new Vector3(0,0,-3.98f),new Vector3(3,3.1f,0.16f));
            foreach(float x in new[]{-1.42f,1.42f}) Wall(root,"Front pillar",new Vector3(x,0,0),new Vector3(0.17f,3.1f,0.28f));
            Box(root,"Lintel",new Vector3(0,2.78f,0),new Vector3(3,0.66f,0.28f),cream);
            // An open central doorway with bars at the sides.
            foreach(float x in new[]{-1.25f,-1.02f,-0.79f,0.79f,1.02f,1.25f})
                Box(root,"Cell grille",new Vector3(x,1.25f,0),new Vector3(0.045f,2.5f,0.045f),cellSteel);
            foreach(float y in new[]{0.5f,1.5f,2.35f})
                foreach(float x in new[]{-1.01f,1.01f})
                    Box(root,"Grille rail",new Vector3(x,y,0),new Vector3(0.65f,0.045f,0.05f),cellSteel);
            var door = new GameObject("Open cell gate").transform;
            door.SetParent(root,false);door.localPosition=new Vector3(-0.6f,0,0);door.localRotation=Quaternion.Euler(0,100,0);
            foreach(float x in new[]{0,0.22f,0.44f,0.66f,0.88f,1.1f})
                Box(door,"Gate bar",new Vector3(x,1.2f,0),new Vector3(0.045f,2.4f,0.045f),cellSteel,false);
            foreach(float y in new[]{0.08f,1.15f,2.35f})Box(door,"Gate crossbar",new Vector3(0.55f,y,0),new Vector3(1.15f,0.055f,0.055f),cellSteel,false);
            Sign(root,number.ToString(),new Vector3(0,2.7f,0.16f),new Vector3(0,180,0),0.12f);

            Box(root,"Bed frame",new Vector3(-0.92f,0.43f,-2.1f),new Vector3(0.82f,0.12f,2.05f),iron);
            foreach(float x in new[]{-1.23f,-0.62f})foreach(float z in new[]{-2.94f,-1.26f})
                Box(root,"Bed leg",new Vector3(x,0.2f,z),new Vector3(0.055f,0.4f,0.055f),iron,false);
            Box(root,"Mattress",new Vector3(-0.92f,0.55f,-2.1f),new Vector3(0.80f,0.15f,2),white);
            Box(root,"Folded blanket",new Vector3(-0.92f,0.66f,-1.89f),new Vector3(0.81f,0.095f,1.5f),cloth,false);
            Sphere(root,"Pillow",new Vector3(-0.92f,0.68f,-2.78f),new Vector3(0.66f,0.19f,0.34f),white,false);
            Box(root,"Wall table",new Vector3(0.98f,0.89f,-0.72f),new Vector3(0.66f,0.08f,0.70f),wood);
            Box(root,"Table bracket",new Vector3(1.18f,0.56f,-0.72f),new Vector3(0.07f,0.65f,0.05f),iron,false);
            Box(root,"Wall shelf",new Vector3(0,1.93f,-3.76f),new Vector3(2.5f,0.065f,0.34f),wood,false);
            Box(root,"WC tank",new Vector3(0.88f,0.70f,-3.65f),new Vector3(0.4f,0.42f,0.18f),white);
            Sphere(root,"WC bowl",new Vector3(0.88f,0.47f,-3.35f),new Vector3(0.51f,0.25f,0.65f),white);
            Sphere(root,"WC opening",new Vector3(0.88f,0.575f,-3.32f),new Vector3(0.34f,0.028f,0.43f),dark,false);
            Box(root,"WC foot",new Vector3(0.88f,0.2f,-3.38f),new Vector3(0.25f,0.40f,0.3f),white);
            Box(root,"Vent recess",new Vector3(0.88f,0.92f,-3.87f),new Vector3(0.57f,0.55f,0.035f),dark,false);
            for(int v=0;v<6;v++)Box(root,"Vent slat",new Vector3(0.88f,0.7f+v*0.085f,-3.83f),new Vector3(0.53f,0.025f,0.025f),iron,false);
            Box(root,"Wash basin",new Vector3(-0.3f,1.02f,-3.66f),new Vector3(0.60f,0.15f,0.40f),white);
            Box(root,"Basin recess",new Vector3(-0.3f,1.103f,-3.62f),new Vector3(0.42f,0.01f,0.24f),dark,false);
            Cylinder(root,"Tap",new Vector3(-0.3f,1.21f,-3.8f),new Vector3(0.025f,0.1f,0.025f),iron,false);
            Cylinder(root,"Pipe",new Vector3(-0.3f,0.59f,-3.82f),new Vector3(0.023f,0.38f,0.023f),iron,false);
            Box(root,"Personal picture",new Vector3(-1.389f,1.53f,-2.15f),new Vector3(0.012f,0.34f,0.26f),paper,false);
            for(int i=0;i<3;i++)Box(root,"Books",new Vector3(-0.65f+i*0.12f,2.07f,-3.7f),new Vector3(0.08f,0.23f,0.18f),i%2==0?cloth:green,false);
            Box(root,"Folded linen",new Vector3(0.45f,2.03f,-3.7f),new Vector3(0.55f,0.13f,0.22f),white,false);
            Cylinder(root,"Tin cup",new Vector3(0.13f,2.055f,-3.7f),new Vector3(0.07f,0.075f,0.07f),iron,false);
            for(int seam=0;seam<5;seam++)Box(root,"Blanket stitched stripe",new Vector3(-0.92f,0.710f,-1.3f-seam*0.25f),new Vector3(0.78f,0.003f,0.014f),wood,false);
            Box(root,"Letter",new Vector3(0.96f,0.935f,-0.7f),new Vector3(0.20f,0.009f,0.27f),paper,false);
            for(int i=0;i<4;i++)Box(root,"Writing",new Vector3(0.96f,0.942f,-0.80f+i*0.04f),new Vector3(0.13f,0.003f,0.006f),cloth,false);
            AddCellWear(root,number);
            if(playerCell)
            {
                Radio(root,new Vector3(1.04f,0.93f,-0.98f));
                var tool = Box(root,"Metal file",new Vector3(1.0f,0.96f,-0.43f),new Vector3(0.035f,0.035f,0.32f),iron,true,false);
                tool.AddComponent<TestTool>();
                Box(tool.transform,"File handle",new Vector3(0,0,-0.6f),new Vector3(2.4f,2.4f,0.30f),wood,false,false);
                LightAt(root,new Vector3(0,2.6f,-1.8f),new Color(1,0.75f,0.46f),1.2f,5,false);
            }
            else LightAt(root,new Vector3(0,2.6f,-2),new Color(1,0.81f,0.56f),0.65f,3.7f,false);
            Sphere(root,"Bare bulb",new Vector3(0,2.82f,-1.8f),new Vector3(0.08f,0.14f,0.08f),glow,false);
        }

        private void Rail(Vector3 a,Vector3 b)
        {
            Line(transform,"Gallery top rail",a+Vector3.up*1.12f,b+Vector3.up*1.12f,0.045f,railPaint);
            Line(transform,"Gallery middle rail",a+Vector3.up*0.56f,b+Vector3.up*0.56f,0.026f,railPaint,false);
            int n=Mathf.CeilToInt(Vector3.Distance(a,b)/0.9f);
            for(int i=0;i<=n;i++)
            {
                Vector3 p=Vector3.Lerp(a,b,(float)i/n);
                Line(transform,"Gallery baluster",p,p+Vector3.up*1.12f,0.033f,railPaint);
                if(i%3==0)Box(transform,"Red rail shoe",p+Vector3.up*0.09f,new Vector3(0.12f,0.18f,0.12f),redBracket,false);
            }
            // Thin collision guard between balusters.
            var barrier=Box(transform,"Gallery safety barrier",(a+b)*0.5f+Vector3.up*0.55f,
                new Vector3(Mathf.Abs(a.x-b.x)+0.05f,1.1f,Mathf.Abs(a.z-b.z)+0.05f),iron,true,false);
            barrier.GetComponent<Renderer>().enabled=false;
        }

        private void Window(Vector3 p,float yaw)
        {
            var root=new GameObject("High prison window").transform;root.SetParent(transform,false);root.localPosition=p;root.localRotation=Quaternion.Euler(0,yaw,0);
            Box(root,"Window glass",Vector3.zero,new Vector3(1.45f,0.65f,0.035f),glass,false);
            for(int i=0;i<6;i++)Box(root,"Window grille",new Vector3(-0.7f+i*0.28f,0,0.035f),new Vector3(0.028f,0.7f,0.025f),iron,false);
            Box(root,"Window divider",new Vector3(0,0,0.04f),new Vector3(1.5f,0.025f,0.025f),iron,false);
        }

        private void Pendant(Vector3 p)
        {
            Line(transform,"Lamp chain",p,p+Vector3.up*1.2f,0.014f,iron,false);
            var shade=Cylinder(transform,"Pendant shade",p,new Vector3(0.36f,0.08f,0.36f),iron,false);
            merge.Remove(shade);shade.GetComponent<Renderer>().shadowCastingMode=ShadowCastingMode.Off;
            Sphere(transform,"Pendant light",p-Vector3.up*0.14f,new Vector3(0.23f,0.24f,0.23f),glow,false);
            for(int rib=0;rib<6;rib++)
            {
                float a=rib*Mathf.PI/3;
                Line(transform,"Lamp cage",p+new Vector3(Mathf.Sin(a)*0.12f,-0.04f,Mathf.Cos(a)*0.12f),p+new Vector3(Mathf.Sin(a)*0.08f,-0.34f,Mathf.Cos(a)*0.08f),0.009f,iron,false);
            }
            LightAt(transform,p-Vector3.up*0.40f,new Color(1,0.80f,0.56f),1.95f,9.5f,Mathf.Abs(p.x)<4);
        }

        private void Radio(Transform root,Vector3 p)
        {
            Box(root,"Old radio",p+Vector3.up*0.14f,new Vector3(0.37f,0.28f,0.17f),wood,false);
            Box(root,"Radio speaker",p+new Vector3(-0.06f,0.14f,0.094f),new Vector3(0.20f,0.20f,0.013f),dark,false);
            for(int i=0;i<8;i++)Box(root,"Speaker grille",p+new Vector3(-0.145f+i*0.024f,0.14f,0.106f),new Vector3(0.004f,0.18f,0.008f),iron,false);
            Sphere(root,"Radio dial",p+new Vector3(0.12f,0.13f,0.10f),new Vector3(0.07f,0.07f,0.015f),iron,false);
        }

        private void Wall(Transform root,string name,Vector3 foot,Vector3 size)
        {
            Box(root,name+" paint",foot+Vector3.up*0.64f,new Vector3(size.x,1.28f,size.z),green);
            Box(root,name+" plaster",foot+Vector3.up*(1.28f+(size.y-1.28f)*0.5f),new Vector3(size.x,size.y-1.28f,size.z),cream);
        }
        private Material Mat(string name,Color color,Texture texture=null)
        {
            var m=new Material(SurfaceShader){name=name,color=color};m.SetFloat("_Glossiness",name.Contains("iron")?0.16f:0.06f);
            m.SetFloat("_SurfaceKind",name.Contains("plaster")||name.Contains("sage")?1:name.Contains("concrete")?2:name.Contains("iron")||name.Contains("metal")?3:name.Contains("wood")?4:0);if(texture!=null)m.mainTexture=texture;return m;
        }
        private Texture2D NoiseTexture()
        {
            var texture=new Texture2D(256,256,TextureFormat.RGB24,true){name="Concrete patches",wrapMode=TextureWrapMode.Repeat,filterMode=FilterMode.Trilinear,anisoLevel=8};
            var pixels=new Color[256*256];
            for(int y=0;y<256;y++)for(int x=0;x<256;x++)
            {
                float v=0.94f+Mathf.PerlinNoise(x/24f,y/24f)*0.06f;
                bool joint=x<2||y<2;pixels[y*256+x]=new Color(v*(joint?0.8f:1),v*(joint?0.8f:1),v*(joint?0.8f:1));
            }
            texture.SetPixels(pixels);texture.Apply();return texture;
        }
        private GameObject Box(Transform root,string name,Vector3 p,Vector3 size,Material m,bool collision=true,bool combine=true)
        {
            var go=Primitive(root,name,PrimitiveType.Cube,p,size,m,collision,combine);
            if(m.mainTexture!=null)
            {
                var mesh=Instantiate(go.GetComponent<MeshFilter>().sharedMesh);var uv=mesh.uv;var normals=mesh.normals;
                for(int i=0;i<uv.Length;i++)
                {
                    Vector3 n=normals[i];Vector2 repeat=Mathf.Abs(n.x)>0.5f?new Vector2(size.z,size.y):Mathf.Abs(n.y)>0.5f?new Vector2(size.x,size.z):new Vector2(size.x,size.y);
                    uv[i]=Vector2.Scale(uv[i],repeat/2);
                }
                mesh.uv=uv;go.GetComponent<MeshFilter>().sharedMesh=mesh;
            }
            return go;
        }
        private GameObject Sphere(Transform root,string name,Vector3 p,Vector3 size,Material m,bool collision=true)
            =>Primitive(root,name,PrimitiveType.Sphere,p,size,m,collision,true);
        private GameObject Cylinder(Transform root,string name,Vector3 p,Vector3 size,Material m,bool collision=true)
            =>Primitive(root,name,PrimitiveType.Cylinder,p,size,m,collision,true);
        private GameObject Primitive(Transform root,string name,PrimitiveType type,Vector3 p,Vector3 size,Material m,bool collision,bool combine)
        {
            var go=GameObject.CreatePrimitive(type);go.name=name;go.transform.SetParent(root,false);go.transform.localPosition=p;go.transform.localScale=size;
            go.GetComponent<Renderer>().sharedMaterial=m;
            if(!collision)DestroyImmediate(go.GetComponent<Collider>());
            if(combine)merge.Add(go);return go;
        }
        private void Line(Transform root,string name,Vector3 a,Vector3 b,float radius,Material m,bool collision=true)
        {
            var go=Cylinder(root,name,(a+b)*0.5f,new Vector3(radius,Vector3.Distance(a,b)*0.5f,radius),m,collision);
            go.transform.localRotation=Quaternion.FromToRotation(Vector3.up,(b-a).normalized);
        }
        private void Sign(Transform root,string text,Vector3 p,Vector3 rotation,float size)
        {
            var go=new GameObject(text);go.transform.SetParent(root,false);go.transform.localPosition=p;go.transform.localRotation=Quaternion.Euler(rotation);
            var label=go.AddComponent<TextMesh>();label.font=font;label.text=text;label.fontSize=72;label.characterSize=size*0.22f;label.anchor=TextAnchor.MiddleCenter;label.color=new Color(0.19f,0.24f,0.20f);
            go.GetComponent<MeshRenderer>().sharedMaterial=font.material;
            go.AddComponent<PrisonSign>().InkShader=SignShader;
        }
        private void LightAt(Transform root,Vector3 p,Color color,float intensity,float range,bool shadows)
        {
            var go=new GameObject("Warm practical light");go.transform.SetParent(root,false);go.transform.localPosition=p;
            var light=go.AddComponent<Light>();light.type=LightType.Point;light.color=color;light.intensity=intensity;light.range=range;light.shadows=shadows?LightShadows.Soft:LightShadows.None;light.shadowBias=0.06f;light.shadowNormalBias=0.4f;
        }
        private void CombineStaticMeshes()
        {
            var groups=new Dictionary<(Material,int,int,int,ShadowCastingMode),List<CombineInstance>>();
            foreach(var go in merge)
            {
                var renderer=go.GetComponent<MeshRenderer>();if(!renderer.enabled)continue;
                var p=go.transform.position;
                var key=(renderer.sharedMaterial,Mathf.FloorToInt(p.x/6),Mathf.FloorToInt(p.z/6),Mathf.FloorToInt(p.y/3.35f),renderer.shadowCastingMode);
                if(!groups.TryGetValue(key,out var list)){list=new List<CombineInstance>();groups.Add(key,list);}
                list.Add(new CombineInstance{mesh=go.GetComponent<MeshFilter>().sharedMesh,transform=transform.worldToLocalMatrix*go.transform.localToWorldMatrix});
                DestroyImmediate(renderer);DestroyImmediate(go.GetComponent<MeshFilter>());
                if(go.transform.childCount==0&&go.GetComponents<Component>().Length==1)DestroyImmediate(go);
            }
            foreach(var pair in groups)
            {
                var go=new GameObject(pair.Key.Item1.name+" geometry "+pair.Key.Item2+"-"+pair.Key.Item3+"-"+pair.Key.Item4);go.transform.SetParent(transform,false);
                var mesh=new Mesh{name=pair.Key.Item1.name,indexFormat=IndexFormat.UInt32};mesh.CombineMeshes(pair.Value.ToArray());mesh.RecalculateBounds();
                go.AddComponent<MeshFilter>().sharedMesh=mesh;var renderer=go.AddComponent<MeshRenderer>();renderer.sharedMaterial=pair.Key.Item1;renderer.shadowCastingMode=pair.Key.Item5;
            }
            merge.Clear();
        }
        private void ConfigureLight()
        {
            Application.targetFrameRate=60;
            RenderSettings.ambientMode=AmbientMode.Trilight;
            RenderSettings.ambientSkyColor=new Color(0.38f,0.45f,0.53f);
            RenderSettings.ambientEquatorColor=new Color(0.36f,0.38f,0.37f);
            RenderSettings.ambientGroundColor=new Color(0.16f,0.15f,0.13f);
            RenderSettings.fog=true;RenderSettings.fogColor=new Color(0.53f,0.62f,0.66f);RenderSettings.fogMode=FogMode.Linear;RenderSettings.fogStartDistance=35;RenderSettings.fogEndDistance=110;
            var sun=new GameObject("Island daylight").AddComponent<Light>();sun.transform.rotation=Quaternion.Euler(42,-35,0);sun.type=LightType.Directional;sun.intensity=0.9f;sun.color=new Color(1,0.93f,0.79f);sun.shadows=LightShadows.Soft;sun.shadowBias=0.06f;sun.shadowNormalBias=0.45f;
            QualitySettings.shadowDistance=32;QualitySettings.shadowCascades=2;QualitySettings.shadowProjection=ShadowProjection.StableFit;QualitySettings.pixelLightCount=4;QualitySettings.antiAliasing=4;QualitySettings.shadows=ShadowQuality.All;QualitySettings.shadowResolution=ShadowResolution.High;
            if(SkyShader!=null)
            {
                var sky=new Material(SkyShader);sky.SetColor("_SkyTint",new Color(0.53f,0.61f,0.67f));sky.SetColor("_GroundColor",new Color(0.38f,0.39f,0.33f));sky.SetFloat("_AtmosphereThickness",0.85f);RenderSettings.skybox=sky;RenderSettings.sun=sun;
            }
        }
        private void SpawnPlayer()
        {
            var player=new GameObject("Player");player.layer=2;player.transform.position=new Vector3(-1.45f,0.02f,-1.2f);
            var controller=player.AddComponent<CharacterController>();controller.height=1.8f;controller.radius=0.27f;controller.center=new Vector3(0,0.9f,0);controller.stepOffset=0.26f;
            var view=new GameObject("Player view");view.transform.SetParent(player.transform,false);view.transform.localPosition=new Vector3(0,1.65f,0);view.tag="MainCamera";
            var camera=view.AddComponent<Camera>();camera.fieldOfView=72;camera.nearClipPlane=0.09f;camera.farClipPlane=180;camera.allowHDR=true;
            view.AddComponent<AudioListener>();view.AddComponent<PrisonFilm>().FilmShader=FilmShader;
            player.AddComponent<PlayerBody>().Initialize(CharacterProfiles[0],CharacterShader,true);
            player.AddComponent<PrototypeSettings>();player.AddComponent<PrototypePlayer>().Initialize(camera,controller);
        }
    }
}
