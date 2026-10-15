using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PrisonCoop.Editor
{
    public static class VisualPrisonBuilder
    {
        private const string Scene="Assets/PrisonCoop/Scenes/VisualPrison.unity";
        private const string Generated="Assets/PrisonCoop/Art/Generated03";

        [MenuItem("Prison Coop/Open Visual Prison")]
        public static void Open()
        {
            if(!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())return;
            if(!File.Exists(Scene))Bake();
            EditorSceneManager.OpenScene(Scene);
        }

        [MenuItem("Prison Coop/Rebuild Visual Prison")]
        public static void Bake()
        {
            if(!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())return;
            Directory.CreateDirectory(Generated);
            if(File.Exists(Scene))
            {
                Directory.CreateDirectory("Builds/SceneBackups");
                File.Copy(Scene,"Builds/SceneBackups/VisualPrison-"+System.DateTime.Now.ToString("yyyyMMdd-HHmmss")+".unity",true);
            }
            var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
            var root=new GameObject("Visual prison");
            var environment=root.AddComponent<PrisonEnvironment>();
            environment.PlasterTexture=AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/PrisonCoop/Art/Textures/prison-plaster-01.png");
            environment.SurfaceShader=AssetDatabase.LoadAssetAtPath<Shader>("Assets/PrisonCoop/Art/Shaders/PrisonSurface.shader");
            environment.SkyShader=Shader.Find("Skybox/Procedural");
            environment.FilmShader=AssetDatabase.LoadAssetAtPath<Shader>("Assets/PrisonCoop/Art/Shaders/WarmFilm.shader");
            environment.SignShader=AssetDatabase.LoadAssetAtPath<Shader>("Assets/PrisonCoop/Art/Shaders/PrisonSign.shader");
            environment.CharacterShader=AssetDatabase.LoadAssetAtPath<Shader>("Assets/PrisonCoop/Art/Shaders/CharacterPaint.shader");
            const string profiles="Assets/PrisonCoop/Art/Characters";
            Directory.CreateDirectory(profiles);
            environment.CharacterProfiles=new PrisonerAppearance[8];
            for(int i=0;i<8;i++)
            {
                string path=profiles+"/Prisoner-"+i+".asset";
                var profile=AssetDatabase.LoadAssetAtPath<PrisonerAppearance>(path);
                if(profile==null){profile=ScriptableObject.CreateInstance<PrisonerAppearance>();AssetDatabase.CreateAsset(profile,path);}
                if(profile.AppearanceRevision<2)
                {
                    profile.Face=i%4;profile.AppearanceRevision=2;
                    profile.Height=new[]{1.83f,1.96f,1.74f,1.63f,1.88f,1.71f,1.80f,1.68f}[i];
                    profile.Width=new[]{0.88f,1.24f,1.04f,0.87f,0.98f,1.30f,0.92f,1.08f}[i];
                    profile.FaceWidth=new[]{0.94f,1.14f,0.89f,0.98f,0.91f,1.18f,1.02f,0.94f}[i];
                    profile.HeadHeight=new[]{1.08f,0.95f,1.1f,1f,1.04f,0.96f,1f,1.1f}[i];
                    profile.JawWidth=new[]{0.88f,1.2f,1.08f,0.83f,1f,1.16f,0.9f,1.05f}[i];
                    profile.HairStyle=new[]{0,1,2,3,4,1,2,0}[i];profile.Beard=new[]{1,0,2,0,0,3,1,0}[i];
                    profile.NoseLength=0.85f+(i%4)*0.16f;profile.Posture=new[]{2f,-1f,5f,0f,2f,3f,0f,4f}[i];
                    profile.ShirtStyle=i==4?2:i%2;
                    profile.Skin=new[]{new Color(0.79f,0.54f,0.36f),new Color(0.85f,0.62f,0.45f),new Color(0.75f,0.58f,0.44f),new Color(0.84f,0.60f,0.40f)}[i%4];
                    profile.Shirt=new[]{new Color(0.66f,0.63f,0.50f),new Color(0.43f,0.50f,0.52f),new Color(0.55f,0.57f,0.53f),new Color(0.72f,0.68f,0.57f),new Color(0.70f,0.69f,0.59f),new Color(0.38f,0.44f,0.47f),new Color(0.56f,0.52f,0.43f),new Color(0.61f,0.64f,0.61f)}[i];
                    profile.Trousers=i%2==0?new Color(0.25f,0.27f,0.23f):new Color(0.21f,0.24f,0.27f);
                    profile.Hair=i==2||i==6?new Color(0.38f,0.37f,0.32f):i==1?new Color(0.36f,0.24f,0.13f):new Color(0.13f,0.10f,0.07f);
                    EditorUtility.SetDirty(profile);
                }
                if(profile.AppearanceRevision<3)
                {
                    profile.AppearanceRevision=3;
                    // Fictional institution: washed chambray blue, grey and work-detail khaki.
                    profile.Shirt=new[]{new Color(0.53f,0.59f,0.61f),new Color(0.43f,0.51f,0.56f),new Color(0.56f,0.59f,0.57f),new Color(0.62f,0.61f,0.52f),new Color(0.64f,0.65f,0.60f),new Color(0.40f,0.49f,0.54f),new Color(0.56f,0.55f,0.48f),new Color(0.49f,0.55f,0.58f)}[i];
                    EditorUtility.SetDirty(profile);
                }
                environment.CharacterProfiles[i]=profile;
            }
            environment.Build();
            int meshId=0;
            foreach(var filter in root.GetComponentsInChildren<MeshFilter>())
            {
                var mesh=filter.sharedMesh;if(mesh==null||!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(mesh)))continue;
                string path=Generated+"/mesh-"+(meshId++)+".asset";
                var existing=AssetDatabase.LoadAssetAtPath<Mesh>(path);
                if(existing!=null){EditorUtility.CopySerialized(mesh,existing);filter.sharedMesh=existing;}
                else AssetDatabase.CreateAsset(mesh,path);
            }
            var materialMap=new System.Collections.Generic.Dictionary<Material,Material>();
            foreach(var renderer in root.GetComponentsInChildren<MeshRenderer>())
            {
                if(renderer.GetComponent<TextMesh>()!=null)continue;
                var material=renderer.sharedMaterial;if(material==null||!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(material)))continue;
                if(!materialMap.TryGetValue(material,out var saved))
                {
                    string slug=material.name.Replace(' ','-');
                    if(material.mainTexture is Texture2D texture && string.IsNullOrEmpty(AssetDatabase.GetAssetPath(texture)))
                    {
                        string texturePath=Generated+"/"+slug+"-texture.asset";
                        var storedTexture=AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
                        if(storedTexture!=null){EditorUtility.CopySerialized(texture,storedTexture);material.mainTexture=storedTexture;}
                        else AssetDatabase.CreateAsset(texture,texturePath);
                    }
                    string path=Generated+"/"+slug+".mat";
                    saved=AssetDatabase.LoadAssetAtPath<Material>(path);
                    if(saved!=null)EditorUtility.CopySerialized(material,saved);
                    else{AssetDatabase.CreateAsset(material,path);saved=material;}
                    materialMap.Add(material,saved);
                }
                renderer.sharedMaterial=saved;
            }
            var importer=AssetImporter.GetAtPath("Assets/PrisonCoop/Art/Textures/prison-plaster-01.png") as TextureImporter;
            if(importer!=null){importer.wrapMode=TextureWrapMode.Repeat;importer.maxTextureSize=1024;importer.anisoLevel=4;importer.SaveAndReimport();}
            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveScene(scene,Scene);
            EditorBuildSettings.scenes=new[]{new EditorBuildSettingsScene(Scene,true)};
        }

        [MenuItem("Prison Coop/Build Visual Mac")]
        public static void BuildMac()
        {
            Bake();
            PlayerSettings.defaultScreenWidth=1440;PlayerSettings.defaultScreenHeight=900;
            PlayerSettings.fullScreenMode=FullScreenMode.Windowed;
            // Keep the same settings identity as the previous build.
            PlayerSettings.colorSpace=ColorSpace.Linear;
            Directory.CreateDirectory("Builds/macOS");
            var report=BuildPipeline.BuildPlayer(new BuildPlayerOptions{scenes=new[]{Scene},locationPathName="Builds/macOS/PrisonCoop-10.app",target=BuildTarget.StandaloneOSX,options=BuildOptions.None});
            if(report.summary.result!=UnityEditor.Build.Reporting.BuildResult.Succeeded)
                throw new System.Exception("Visual Mac build failed: "+report.summary.result);
            Debug.Log("VISUAL_BUILD_SUCCESS "+report.summary.totalSize+" bytes");
        }
    }
}
