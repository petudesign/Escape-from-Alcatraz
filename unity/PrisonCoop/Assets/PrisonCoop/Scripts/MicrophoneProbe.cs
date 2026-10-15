using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace PrisonCoop
{
    // Opt-in local level measurement. No playback, disk recording or network upload.
    public sealed class MicrophoneProbe : MonoBehaviour
    {
        private readonly float[] samples = new float[256];
        private AudioClip clip;
        private string device;
        private bool starting;
        private float level;
        private string status = "Mikrofoni pois päältä. M aloittaa paikallisen tasotestin.";

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
            {
                if (starting || clip != null) StopProbe();
                else StartCoroutine(StartProbe());
            }
#endif
            if (clip == null) return;
            if (!Microphone.IsRecording(device))
            {
                StopProbe();
                status = "Mikrofoni pysähtyi. Tarkista laite ja lupa, kokeile M uudelleen.";
                return;
            }
            int position = Microphone.GetPosition(device);
            if (position < samples.Length) return;
            if (!clip.GetData(samples, position - samples.Length)) return;
            float sum = 0;
            foreach (float sample in samples) sum += sample * sample;
            level = Mathf.Clamp01(Mathf.Sqrt(sum / samples.Length) * 8);
        }

        private IEnumerator StartProbe()
        {
            starting = true;
            status = "Pyydetään mikrofonilupaa…";
            yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
            if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                status = "Mikrofonilupa puuttuu. Tarkista macOS:n mikrofoniasetukset.";
                starting = false;
                yield break;
            }
            var devices = Microphone.devices;
            if (devices.Length == 0)
            {
                status = "Mikrofonia ei löytynyt. Liitä mikrofoni ja paina M.";
                starting = false;
                yield break;
            }
            device = devices[0];
            // Keep only a one-second circular buffer in memory.
            clip = Microphone.Start(device, true, 1, 16000);
            if (clip == null)
            {
                status = "Mikrofonin käynnistys epäonnistui.";
                starting = false;
                yield break;
            }
            float deadline = Time.realtimeSinceStartup + 3;
            while (Microphone.GetPosition(device) == 0 && Time.realtimeSinceStartup < deadline)
                yield return null;
            starting = false;
            if (Microphone.GetPosition(device) == 0)
            {
                StopProbe();
                status = "Mikrofonista ei tullut ääntä. Tarkista macOS:n lupa ja syöttölaite.";
                yield break;
            }
            status = "Paikallinen mikrofonitesti: " + device + " · M lopettaa";
        }

        private void StopProbe()
        {
            StopAllCoroutines();
            if (device != null) Microphone.End(device);
            if (clip != null) Destroy(clip);
            clip = null;
            device = null;
            starting = false;
            level = 0;
            status = "Mikrofoni pois päältä. M aloittaa paikallisen tasotestin.";
        }

        private void OnApplicationFocus(bool focused)
        {
            if (!focused) StopProbe();
        }

        private void OnDisable() => StopProbe();

        private void OnGUI()
        {
            GUI.Box(new Rect(22, 150, Mathf.Min(650, Screen.width - 44), 75), "");
            GUI.Label(new Rect(38, 160, Mathf.Min(610, Screen.width - 60), 25), status);
            GUI.Box(new Rect(38, 195, 200, 10), "");
            var previous = GUI.color;
            GUI.color = new Color(0.55f, 0.8f, 0.55f);
            GUI.DrawTexture(new Rect(38, 195, 200 * level, 10), Texture2D.whiteTexture);
            GUI.color = previous;
        }
    }
}
