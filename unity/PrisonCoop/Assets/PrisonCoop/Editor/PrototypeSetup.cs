using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PrisonCoop.Editor
{
    [InitializeOnLoad]
    public static class PrototypeSetup
    {
        private const string ScenePath = "Assets/PrisonCoop/Scenes/CellBlock.unity";

        static PrototypeSetup()
        {
            EditorApplication.delayCall += Prepare;
        }

        private static void Prepare()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling) return;
            if (File.Exists(ScenePath)) return;
            Directory.CreateDirectory(Path.GetDirectoryName(ScenePath));
            var current = SceneManager.GetActiveScene();
            if (string.IsNullOrEmpty(current.path) && current.isDirty && !Application.isBatchMode)
            {
                Debug.Log("Save the current scene, then choose Prison Coop > Open Cell Test.");
                return;
            }
            var mode = string.IsNullOrEmpty(current.path) ? NewSceneMode.Single : NewSceneMode.Additive;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, mode);
            var bootstrap = new GameObject("Prototype world");
            SceneManager.MoveGameObjectToScene(bootstrap, scene);
            bootstrap.AddComponent<PrototypeWorld>().SurfaceShader = Shader.Find("Standard");
            EditorSceneManager.SaveScene(scene, ScenePath);
            if (mode == NewSceneMode.Additive) EditorSceneManager.CloseScene(scene, true);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            PlayerSettings.companyName = "PrisonCoopPrototype";
            PlayerSettings.productName = "Prison Coop - Cell Test";
            PlayerSettings.defaultScreenWidth = 1280;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            SetMicrophoneDescription();
            AssetDatabase.SaveAssets();
            Debug.Log("Prison Coop prepared. Choose Prison Coop > Open Cell Test, then Play.");
        }

        private static void SetMicrophoneDescription()
        {
            // Use serialized setting so this scaffold does not depend on a platform-specific API.
            var assets = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/ProjectSettings.asset");
            if (assets.Length == 0)
            {
                Debug.LogWarning("Player Settings not available yet. Configure microphone description before building.");
                return;
            }
            var settings = new SerializedObject(assets);
            var inputHandling = settings.FindProperty("activeInputHandler");
            if (inputHandling != null) inputHandling.intValue = 1;
            var description = settings.FindProperty("microphoneUsageDescription");
            if (description != null)
            {
                description.stringValue = "Optional local microphone level test. Audio is not saved or uploaded.";
                settings.ApplyModifiedPropertiesWithoutUndo();
            }
            else Debug.LogWarning("Check Player Settings > microphone usage description before a Mac microphone build.");
            settings.ApplyModifiedPropertiesWithoutUndo();
        }

        [MenuItem("Prison Coop/Open Cell Test")]
        public static void OpenTest()
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                Prepare();
                EditorSceneManager.OpenScene(ScenePath);
            }
        }

        [MenuItem("Prison Coop/Build Mac Test")]
        public static void BuildMac()
        {
            Prepare();
            SetMicrophoneDescription();
            Directory.CreateDirectory("Builds/macOS");
            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { ScenePath },
                locationPathName = "Builds/macOS/PrisonCoop-02.app",
                target = BuildTarget.StandaloneOSX,
                options = BuildOptions.Development
            });
            Debug.Log("Mac test build: " + report.summary.result);
            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
                EditorUtility.RevealInFinder("Builds/macOS/PrisonCoop-02.app");
        }
    }
}
