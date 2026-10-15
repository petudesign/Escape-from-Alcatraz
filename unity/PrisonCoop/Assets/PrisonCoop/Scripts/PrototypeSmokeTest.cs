using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PrisonCoop
{
    // Runs only with an explicit CLI test flag; never in a normal play session.
    public sealed class PrototypeSmokeTest : MonoBehaviour
    {
        private string output;
        private readonly List<string> results = new List<string>();
        private int failures;
        private bool settingsPreview;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Install()
        {
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i + 1 < args.Length; i++)
                if (args[i] == "--prototype-smoke" || args[i] == "--settings-preview")
                {
                    var runner = new GameObject("CLI test runner").AddComponent<PrototypeSmokeTest>();
                    runner.output = args[i + 1];
                    runner.settingsPreview = args[i] == "--settings-preview";
                    return;
                }
        }

        private IEnumerator Start()
        {
            Directory.CreateDirectory(output);
            Application.logMessageReceived += CheckLog;
            yield return null;
            yield return new WaitForFixedUpdate();
            var player = FindFirstObjectByType<PrototypePlayer>();
            var tool = FindFirstObjectByType<TestTool>();
            if (settingsPreview)
            {
                var settings = player.GetComponent<PrototypeSettings>();
                settings.Open();
                for (int i = 0; i < 5; i++) yield return null;
                ScreenCapture.CaptureScreenshot(Path.Combine(output, "settings-view.png"));
                for (int i = 0; i < 10; i++) yield return null;
                Application.Quit(0);
                yield break;
            }
            Check(player != null && tool != null, "Player and tool present in standalone build");
            if (player == null || tool == null) { Finish(); yield break; }
            var preferences = player.GetComponent<PrototypeSettings>();
            preferences.Open();
            Check(preferences.IsOpen && Cursor.lockState == CursorLockMode.None, "Settings opens and releases cursor");
            preferences.SetLookOptions(1.75f, true);
            preferences.Resume();
            var reloadObject = new GameObject("Settings persistence check");
            var reloaded = reloadObject.AddComponent<PrototypeSettings>();
            Check(Mathf.Approximately(reloaded.Sensitivity, 1.75f) && reloaded.InvertY,
                "Sensitivity and invert Y survive a settings instance reload");
            Destroy(reloadObject);
            PlayerPrefs.DeleteKey("prototype.smoke.sensitivity");
            PlayerPrefs.DeleteKey("prototype.smoke.invertY");
            PlayerPrefs.Save();
            var controller = player.GetComponent<CharacterController>();
            var camera = player.GetComponentInChildren<Camera>();
            player.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Reset(controller, new Vector3(-1.2f, 0.02f, 0.8f));
            controller.Move(new Vector3(-8, 0, 0));
            Check(player.transform.position.x > -2.2f, "Outer wall blocks lateral movement");
            Reset(controller, new Vector3(-1.2f, 0.02f, 0.8f));
            controller.Move(new Vector3(5, 0, 0));
            Check(player.transform.position.x < -0.25f, "Cell divider blocks direct crossing");
            Reset(controller, new Vector3(-1.2f, 0.02f, 0.8f));
            controller.Move(new Vector3(0, 0, 3));
            Check(player.transform.position.z > 3, "Player passes through cell opening to corridor");
            controller.Move(new Vector3(2.4f, 0, 0));
            controller.Move(new Vector3(0, 0, -3));
            Check(player.transform.position.x > 1 && player.transform.position.z < 1.2f, "Second cell reachable via corridor");

            var home = tool.transform.position;
            tool.Toggle(camera.transform);
            Check(tool.IsHeld && !tool.GetComponent<Collider>().enabled, "Held tool attaches without blocking player");
            tool.Toggle(camera.transform);
            Check(!tool.IsHeld && Vector3.Distance(tool.transform.position, home) < 0.01f && tool.GetComponent<Collider>().enabled,
                "Tool returns to table with collider restored");
            Reset(controller, new Vector3(-1.2f, 0.02f, 0.8f));
            var direction = home - camera.transform.position;
            Physics.SyncTransforms();
            bool canUse = Physics.Raycast(camera.transform.position, direction.normalized, out var hit, 2.5f)
                          && hit.collider.GetComponent<TestTool>() == tool;
            Check(canUse, "Tool is reachable by player interaction ray from clear floor; hit=" +
                (hit.collider != null ? hit.collider.name : "none") + "; origin=" + camera.transform.position + "; tool=" + home);

            Capture(camera, new Vector3(-1.2f, 1.65f, 1.35f), new Vector3(-0.8f, 1.1f, -0.8f), "cell-view.png");
            Capture(camera, new Vector3(1.8f, 1.65f, 4.6f), new Vector3(-0.2f, 1.2f, 0.3f), "corridor-view.png");
            yield return null;
            Finish();
        }

        private static void Reset(CharacterController controller, Vector3 position)
        {
            controller.enabled = false;
            controller.transform.position = position;
            controller.enabled = true;
            Physics.SyncTransforms();
        }

        private void Capture(Camera camera, Vector3 position, Vector3 target, string filename)
        {
            camera.transform.position = position;
            camera.transform.LookAt(target);
            var render = new RenderTexture(1280, 720, 24);
            var previous = RenderTexture.active;
            var priorTarget = camera.targetTexture;
            camera.targetTexture = render;
            camera.Render();
            RenderTexture.active = render;
            var texture = new Texture2D(1280, 720, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, 1280, 720), 0, 0);
            texture.Apply();
            File.WriteAllBytes(Path.Combine(output, filename), texture.EncodeToPNG());
            camera.targetTexture = priorTarget;
            RenderTexture.active = previous;
            render.Release();
            Destroy(render);
            Destroy(texture);
        }

        private void Check(bool passed, string label)
        {
            results.Add((passed ? "PASS " : "FAIL ") + label);
            if (!passed) failures++;
        }

        private void CheckLog(string message, string trace, LogType type)
        {
            if (type == LogType.Exception || type == LogType.Error || type == LogType.Assert)
                Check(false, "Runtime error: " + message);
        }

        private void Finish()
        {
            Application.logMessageReceived -= CheckLog;
            results.Add("Microphone not activated by automated test. Human co-op test pending.");
            File.WriteAllLines(Path.Combine(output, "smoke-results.txt"), results);
            Application.Quit(failures == 0 ? 0 : 1);
        }
    }
}
