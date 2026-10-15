using UnityEngine;

namespace PrisonCoop
{
    // Composition point: future schedules issue destinations to the motor; visuals only observe movement.
    public sealed class WalkingPrisoner : MonoBehaviour
    {
        public PrisonerMotor Motor { get; private set; }
        private PrisonerVisual visual;
        private float walkSpeed;
        public void Initialize(PrisonerAppearance appearance,Material paint,Vector3[] route,float speed,float offset,int identity)
        {
            walkSpeed=speed;
            visual=gameObject.AddComponent<PrisonerVisual>();visual.Initialize(appearance,paint,offset);
            Motor=gameObject.AddComponent<PrisonerMotor>();Motor.Initialize(route,speed,appearance.Height,appearance.Width,identity);
        }
        private void LateUpdate(){if(Motor!=null)visual.Animate(Motor.ActualSpeed,walkSpeed);}
    }
}
