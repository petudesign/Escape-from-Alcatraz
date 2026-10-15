using UnityEngine;

namespace PrisonCoop
{
    public sealed class PrototypeSettings : MonoBehaviour
    {
        private const string SensitivityKey = "prototype.lookSensitivity";
        private const string InvertKey = "prototype.invertY";
        public float Sensitivity { get; private set; }
        public bool InvertY { get; private set; }
        public bool IsOpen { get; private set; }
        private bool dirty;
        private string sensitivityKey = SensitivityKey;
        private string invertKey = InvertKey;

        private void Awake()
        {
            if (System.Array.IndexOf(System.Environment.GetCommandLineArgs(), "--prototype-smoke") >= 0)
            {
                sensitivityKey = "prototype.smoke.sensitivity";
                invertKey = "prototype.smoke.invertY";
            }
            Sensitivity = Mathf.Clamp(PlayerPrefs.GetFloat(sensitivityKey, 1), 0.25f, 3);
            InvertY = PlayerPrefs.GetInt(invertKey, 0) == 1;
        }

        public void SetLookOptions(float sensitivity, bool invert)
        {
            Sensitivity = Mathf.Clamp(sensitivity, 0.25f, 3);
            InvertY = invert;
            dirty = true;
        }

        public void Toggle()
        {
            if (IsOpen) Resume(); else Open();
        }

        public void Open()
        {
            IsOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Resume()
        {
            Save();
            IsOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Save()
        {
            if (!dirty) return;
            PlayerPrefs.SetFloat(sensitivityKey, Sensitivity);
            PlayerPrefs.SetInt(invertKey, InvertY ? 1 : 0);
            PlayerPrefs.Save();
            dirty = false;
        }

        private void OnApplicationQuit() => Save();

        private void OnGUI()
        {
            if (!IsOpen) return;
            var previousMatrix = GUI.matrix;
            var previousColor = GUI.color;
            GUI.matrix = Matrix4x4.Scale(new Vector3(Screen.width / 1280f, Screen.height / 720f, 1));
            GUI.color = new Color(0, 0, 0, 0.8f);
            GUI.DrawTexture(new Rect(0, 0, 1280, 720), Texture2D.whiteTexture);
            GUI.color = Color.white;

            var title = new GUIStyle(GUI.skin.label) { fontSize = 28, fontStyle = FontStyle.Bold };
            var label = new GUIStyle(GUI.skin.label) { fontSize = 18 };
            var button = new GUIStyle(GUI.skin.button) { fontSize = 18 };
            var toggle = new GUIStyle(GUI.skin.toggle) { fontSize = 18 };
            GUI.Box(new Rect(350, 150, 580, 430), "");
            GUI.Label(new Rect(390, 183, 490, 48), "ASETUKSET", title);
            GUI.Label(new Rect(390, 255, 360, 32), "Hiiren herkkyys", label);
            GUI.Label(new Rect(780, 255, 110, 32), Sensitivity.ToString("0.00") + " ×", label);
            float next = GUI.HorizontalSlider(new Rect(393, 301, 490, 25), Sensitivity, 0.25f, 3);
            bool invert = GUI.Toggle(new Rect(390, 352, 490, 36), InvertY, "  Käännä pystysuuntainen katse", toggle);
            if (!Mathf.Approximately(next, Sensitivity) || invert != InvertY)
            {
                SetLookOptions(next, invert);
            }
            if (GUI.Button(new Rect(390, 419, 230, 46), "Palauta oletukset", button))
            {
                SetLookOptions(1, false);
            }
            if (GUI.Button(new Rect(645, 419, 245, 46), "Jatka peliä", button)) Resume();
            GUI.Label(new Rect(390, 500, 510, 30), "Esc jatkaa · asetukset tallentuvat tälle koneelle", label);
            GUI.matrix = previousMatrix;
            GUI.color = previousColor;
        }
    }
}
