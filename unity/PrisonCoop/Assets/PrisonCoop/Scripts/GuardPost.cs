using UnityEngine;
namespace PrisonCoop
{
    public sealed class GuardPost : MonoBehaviour
    {
        private PrisonerVisual visual;
        public void Initialize(PrisonerAppearance profile,Material material)
        {
            visual=gameObject.AddComponent<PrisonerVisual>();visual.Initialize(profile,material,0);
            var capsule=gameObject.AddComponent<CapsuleCollider>();capsule.height=profile.Height;capsule.center=Vector3.up*profile.Height/2;capsule.radius=0.25f;
        }
        private void LateUpdate(){if(visual!=null)visual.Animate(0,1);}
    }
}
