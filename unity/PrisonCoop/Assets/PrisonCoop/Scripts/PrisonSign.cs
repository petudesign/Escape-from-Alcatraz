using UnityEngine;
namespace PrisonCoop
{
    // Font's default UI material draws through geometry; world signs need depth and a front face.
    [RequireComponent(typeof(TextMesh))]
    public sealed class PrisonSign : MonoBehaviour
    {
        public Shader InkShader;
        private Material ink;
        private TextMesh label;
        private void Awake()
        {
            label=GetComponent<TextMesh>();ink=new Material(InkShader){name="World sign ink"};
            ink.mainTexture=label.font.material.mainTexture;GetComponent<MeshRenderer>().sharedMaterial=ink;
        }
        private void LateUpdate(){if(ink!=null)ink.mainTexture=label.font.material.mainTexture;}
        private void OnDestroy(){if(ink!=null)Destroy(ink);}
    }
}
