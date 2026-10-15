using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace PrisonCoop
{
    public sealed class PrototypePlayer : MonoBehaviour
    {
        private Camera view;
        private CharacterController controller;
        private TestTool target;
        private TestTool heldTool;
        private float pitch;
        private float fallSpeed;
        private PrototypeSettings settings;
        private string feedback = "Etsi sellin pöydältä työkalu.";
        private float feedbackUntil;
        private bool visualScene;
        public float ViewPitch=>pitch;

        public void Initialize(Camera camera, CharacterController character)
        {
            view = camera;
            controller = character;
            settings = GetComponent<PrototypeSettings>();
            visualScene = FindFirstObjectByType<PrisonEnvironment>() != null;
        }

        private void Start() => SetCapture(true);

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            var keys = Keyboard.current;
            var mouse = Mouse.current;
            if (keys == null || mouse == null) return;
            if (keys.escapeKey.wasPressedThisFrame)
            {
                settings.Toggle();
                return;
            }
            if (settings.IsOpen) return;
            var network = PrototypeNetwork.Instance;
            if (keys.hKey.wasPressedThisFrame && network != null && !network.Running) network.Begin(true);
            if (keys.jKey.wasPressedThisFrame && network != null && !network.Running) network.Begin(false);
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                if (mouse.leftButton.wasPressedThisFrame) SetCapture(true);
                return;
            }

            var delta = mouse.delta.ReadValue() * (0.10f * settings.Sensitivity);
            transform.Rotate(0, delta.x, 0);
            pitch = Mathf.Clamp(pitch + delta.y * (settings.InvertY ? 1 : -1), -80, 80);
            view.transform.localRotation = Quaternion.Euler(pitch, 0, 0);

            var input = new Vector2(
                (keys.dKey.isPressed ? 1 : 0) - (keys.aKey.isPressed ? 1 : 0),
                (keys.wKey.isPressed ? 1 : 0) - (keys.sKey.isPressed ? 1 : 0));
            input = Vector2.ClampMagnitude(input, 1);
            if (controller.isGrounded && fallSpeed < 0) fallSpeed = -2;
            fallSpeed += Physics.gravity.y * Time.deltaTime;
            var movement = (transform.right * input.x + transform.forward * input.y) * 3;
            movement.y = fallSpeed;
            controller.Move(movement * Time.deltaTime);

            target = null;
            if (Physics.Raycast(view.transform.position, view.transform.forward, out var hit, 2.5f))
                target = hit.collider.GetComponent<TestTool>();
            if (keys.eKey.wasPressedThisFrame)
            {
                if (network != null && network.Running)
                {
                    if (network.ToolOwner == network.LocalId || target != null) network.ToggleTool();
                    return;
                }
                if (heldTool != null)
                {
                    heldTool.Toggle(view.transform);
                    heldTool = null;
                    feedback = "Työkalu palautettu pöydälle.";
                    feedbackUntil = Time.time + 3;
                }
                else if (target != null)
                {
                    heldTool = target;
                    heldTool.Toggle(view.transform);
                    feedback = "Työkalu kädessä. E palauttaa sen.";
                    feedbackUntil = Time.time + 3;
                }
            }
#endif
        }

        private static void SetCapture(bool captured)
        {
            Cursor.lockState = captured ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !captured;
        }

        private void OnApplicationFocus(bool focused)
        {
            if (!focused && settings != null) settings.Open();
        }

        private void OnDisable() => SetCapture(false);

        private void OnGUI()
        {
            if (settings != null && settings.IsOpen) return;
            if (visualScene)
            {
                var matrix=GUI.matrix;
                GUI.matrix=Matrix4x4.Scale(new Vector3(Screen.width/1280f,Screen.height/720f,1));
                var label=new GUIStyle(GUI.skin.label){fontSize=15};
                GUI.Label(new Rect(30,22,320,28),"SELLIOSASTO B",label);
                GUI.Label(new Rect(30,670,780,28),"WASD liiku · hiiri katso · E käytä · Esc asetukset",label);
                var prior=GUI.color;GUI.color=new Color(1,1,1,0.55f);GUI.DrawTexture(new Rect(639,359,2,2),Texture2D.whiteTexture);GUI.color=prior;
                string action=heldTool!=null?"E  Palauta viila":target!=null?"E  Ota viila":"";
                GUI.Label(new Rect(530,430,300,28),action,label);
                GUI.matrix=matrix;
                return;
            }
            var oldMatrix = GUI.matrix;
            GUI.matrix = Matrix4x4.Scale(new Vector3(Screen.width / 1280f, Screen.height / 720f, 1));
            GUI.Box(new Rect(22, 20, 430, 120), "");
            GUI.Label(new Rect(38, 32, 400, 28), "VANKILAPAKO — TILAKOE 01");
            GUI.Label(new Rect(38, 62, 400, 24), "WASD liiku   ·   Hiiri katso   ·   E käytä");
            GUI.Label(new Rect(38, 90, 400, 24), "Esc — asetukset ja hiiren herkkyys");
            GUI.Label(new Rect(635, 345, 30, 30), "+");
            var network = PrototypeNetwork.Instance;
            bool holding = network != null && network.Running ? network.ToolOwner == network.LocalId : heldTool != null;
            string prompt = holding ? "E — palauta työkalu" : target != null ? "E — ota työkalu" : "";
            GUI.Label(new Rect(500, 390, 320, 32), prompt);
            if (Time.time < feedbackUntil || (heldTool == null && feedbackUntil == 0))
                GUI.Label(new Rect(38, 620, 600, 28), feedback);
            if (Cursor.lockState != CursorLockMode.Locked)
                GUI.Box(new Rect(430, 270, 420, 60), "Klikkaa jatkaaksesi / Esc lukitsee hiiren");
#if !ENABLE_INPUT_SYSTEM
            GUI.Box(new Rect(220, 200, 840, 70), "Ota Input System käyttöön ja käynnistä Editor uudelleen.\nEdit > Project Settings > Player > Active Input Handling");
#endif
            GUI.Label(new Rect(38, 660, 1000, 28), "H — host · J — liity samalla koneella · NPC-puhe myöhemmässä kokeessa");
            GUI.matrix = oldMatrix;
        }
    }
}
