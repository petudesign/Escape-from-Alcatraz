using UnityEngine;

namespace PrisonCoop
{
    // Disposable blockout: keep production art separate from this test scene.
    public sealed class PrototypeWorld : MonoBehaviour
    {
        // Serialized reference keeps this shader included in standalone builds.
        public Shader SurfaceShader;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            RenderSettings.ambientLight = new Color(0.38f, 0.42f, 0.47f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.16f, 0.19f, 0.22f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 15;
            RenderSettings.fogEndDistance = 35;

            var concrete = Material("Warm plaster", new Color(0.67f, 0.63f, 0.48f));
            var lowerWall = Material("Faded green paint", new Color(0.36f, 0.42f, 0.28f));
            var floor = Material("Floor", new Color(0.29f, 0.28f, 0.23f));
            var metal = Material("Iron", new Color(0.14f, 0.18f, 0.19f));
            var cloth = Material("Blanket", new Color(0.42f, 0.37f, 0.29f));
            var porcelain = Material("WC", new Color(0.68f, 0.67f, 0.58f));
            var tool = Material("Test tool", new Color(0.72f, 0.48f, 0.20f));

            // Nominal cell module 2.4 x 3.6 m, excluding wall thickness.
            // Rear service passage is reserved for the future escape route.
            Box("Floor", new Vector3(0, -0.15f, 1), new Vector3(5.2f, 0.3f, 8.4f), floor);
            Box("Service passage rear wall", new Vector3(0, 1.4f, -3.1f), new Vector3(5.2f, 2.8f, 0.2f), concrete);
            Box("Left wall", new Vector3(-2.5f, 1.4f, 1), new Vector3(0.2f, 2.8f, 8.4f), concrete);
            Box("Right wall", new Vector3(2.5f, 1.4f, 1), new Vector3(0.2f, 2.8f, 8.4f), concrete);
            Box("Corridor end", new Vector3(0, 1.4f, 5.1f), new Vector3(5.2f, 2.8f, 0.2f), concrete);
            Box("Ceiling", new Vector3(0, 2.9f, 1), new Vector3(5.2f, 0.2f, 8.4f), concrete);
            Box("Cell divider", new Vector3(0, 1.4f, 0.2f), new Vector3(0.12f, 2.8f, 3.6f), concrete);

            for (int cell = 0; cell < 2; cell++)
            {
                float center = cell == 0 ? -1.2f : 1.2f;
                for (int i = 0; i <= 8; i++)
                {
                    float x = center - 1.1f + i * (2.2f / 8);
                    if (Mathf.Abs(x - center) < 0.45f) continue;
                    Box("Cell bar", new Vector3(x, 1.4f, 2), new Vector3(0.045f, 2.8f, 0.045f), metal);
                }
                Box("Bar top", new Vector3(center, 2.7f, 2), new Vector3(2.3f, 0.1f, 0.1f), metal);
                Box("Bed frame", new Vector3(center - 0.67f, 0.42f, -0.2f), new Vector3(0.75f, 0.15f, 1.85f), metal);
                Box("Mattress", new Vector3(center - 0.67f, 0.56f, -0.2f), new Vector3(0.73f, 0.13f, 1.83f), cloth);
                Box("Table", new Vector3(center + 0.75f, 0.85f, 1.1f), new Vector3(0.65f, 0.1f, 0.5f), concrete);
                Box("Table support", new Vector3(center + 0.75f, 0.4f, 1.1f), new Vector3(0.15f, 0.8f, 0.15f), metal);
                Box("Shelf", new Vector3(center, 1.85f, -1.4f), new Vector3(1.8f, 0.07f, 0.25f), concrete);

                float ventX = center + 0.65f;
                // Real gap in blockout wall, presently sealed by a solid removable-cover placeholder.
                Box("Rear wall left", new Vector3(center - 0.4f, 1.4f, -1.6f), new Vector3(1.6f, 2.8f, 0.2f), concrete);
                Box("Rear wall right", new Vector3(center + 1.05f, 1.4f, -1.6f), new Vector3(0.3f, 2.8f, 0.2f), concrete);
                Box("Below future escape opening", new Vector3(ventX, 0.225f, -1.6f), new Vector3(0.5f, 0.45f, 0.2f), lowerWall);
                Box("Above future escape opening", new Vector3(ventX, 1.925f, -1.6f), new Vector3(0.5f, 1.75f, 0.2f), concrete);
                Box("Escape cover - not interactive yet", new Vector3(ventX, 0.75f, -1.6f), new Vector3(0.5f, 0.6f, 0.08f), metal);
                Box("WC tank", new Vector3(ventX, 0.65f, -1.3f), new Vector3(0.4f, 0.48f, 0.18f), porcelain);
                Box("WC pedestal", new Vector3(ventX, 0.2f, -1.0f), new Vector3(0.28f, 0.4f, 0.35f), porcelain);
                Box("WC bowl blockout", new Vector3(ventX, 0.44f, -1.0f), new Vector3(0.44f, 0.18f, 0.55f), porcelain);
                // Thin paint layer on wall faces; no extra collision geometry.
                var paint = Box("Lower wall paint", new Vector3(center - 0.4f, 0.65f, -1.489f), new Vector3(1.6f, 1.3f, 0.01f), lowerWall);
                paint.GetComponent<Collider>().enabled = false;
            }

            // Continue the green lower paint around the side walls and both sides of the divider.
            foreach (float x in new[] { -2.389f, 2.389f, -0.066f, 0.066f })
            {
                var paint = Box("Side wall lower paint", new Vector3(x, 0.65f, 0.2f), new Vector3(0.01f, 1.3f, 3.6f), lowerWall);
                paint.GetComponent<Collider>().enabled = false;
            }

            var testTool = Box("Tool: interact with E", new Vector3(-0.45f, 0.98f, 1.1f), new Vector3(0.38f, 0.1f, 0.12f), tool);
            testTool.AddComponent<TestTool>();

            var lamp = new GameObject("Cell light").AddComponent<Light>();
            lamp.type = LightType.Point;
            lamp.transform.position = new Vector3(-1.2f, 2.5f, 0);
            lamp.color = new Color(1, 0.86f, 0.64f);
            lamp.range = 11;
            lamp.intensity = 1;
            lamp.shadows = LightShadows.Soft;
            var corridorLamp = new GameObject("Corridor light").AddComponent<Light>();
            corridorLamp.type = LightType.Point;
            corridorLamp.transform.position = new Vector3(1, 2.5f, 3.5f);
            corridorLamp.color = new Color(0.65f, 0.78f, 1);
            corridorLamp.range = 12;
            corridorLamp.intensity = 1;

            var player = new GameObject("Local test player");
            // Ignore Raycast layer: interaction rays must not hit the player's own capsule.
            // CharacterController collision with the environment remains enabled.
            player.layer = 2;
            player.transform.position = new Vector3(-1.2f, 0.05f, 0.8f);
            var controller = player.AddComponent<CharacterController>();
            controller.height = 1.8f;
            controller.radius = 0.3f;
            controller.center = new Vector3(0, 0.9f, 0);
            var cameraObject = new GameObject("Player view");
            cameraObject.transform.SetParent(player.transform, false);
            cameraObject.transform.localPosition = new Vector3(0, 1.65f, 0);
            cameraObject.tag = "MainCamera";
            var view = cameraObject.AddComponent<Camera>();
            view.fieldOfView = 75;
            view.nearClipPlane = 0.05f;
            view.farClipPlane = 60;
            view.backgroundColor = RenderSettings.fogColor;
            cameraObject.AddComponent<AudioListener>();
            player.AddComponent<PrototypeSettings>();
            player.AddComponent<PrototypePlayer>().Initialize(view, controller);
            gameObject.AddComponent<PrototypeNetwork>();
        }

        private Material Material(string name, Color color)
        {
            var shader = SurfaceShader != null ? SurfaceShader : Shader.Find("Standard");
            var material = new Material(shader) { name = name, color = color };
            material.SetFloat("_Glossiness", 0.05f);
            return material;
        }

        private GameObject Box(string label, Vector3 position, Vector3 scale, Material material)
        {
            var item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            item.name = label;
            item.transform.SetParent(transform, false);
            item.transform.position = position;
            item.transform.localScale = scale;
            item.GetComponent<Renderer>().sharedMaterial = material;
            return item;
        }
    }
}
