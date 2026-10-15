using UnityEngine;

namespace PrisonCoop
{
    // Local steering on authored walkable routes. This is not global pathfinding.
    public sealed class PrisonerMotor : MonoBehaviour
    {
        private CharacterController controller;
        private PrisonPopulation population;
        private Transform player;
        private Vector3[] route;
        private int next,identity;
        private float speed,vertical,pauseUntil,blockedFor;
        private Vector3 velocity;
        private readonly RaycastHit[] hits=new RaycastHit[32];
        private static readonly float[] Angles={0,25,-25,50,-50,80,-80,110,-110,160};
        public float ActualSpeed { get; private set; }
        public int WaypointsReached { get; private set; }
        public float LongestBlock { get; private set; }
        public Vector3 Velocity=>velocity;
        public void Initialize(Vector3[] points,float pace,float height,float width,int id)
        {
            route=points;next=1;speed=pace;identity=id;
            population=GetComponentInParent<PrisonPopulation>();
            var actor=FindFirstObjectByType<PrototypePlayer>();player=actor==null?null:actor.transform;
            controller=gameObject.AddComponent<CharacterController>();controller.height=height;controller.center=Vector3.up*height/2;
            controller.radius=0.24f*width;controller.stepOffset=0.22f;controller.skinWidth=0.025f;
            transform.rotation=Quaternion.LookRotation((route[next]-transform.position).normalized);
        }
        public void SetRoute(Vector3[] points)
        {
            if(points==null||points.Length<2)return;
            route=points;next=1;pauseUntil=0;blockedFor=0;WaypointsReached=0;
        }
        private void Update()
        {
            if(route==null)return;
            Vector3 delta=Flat(route[next]-transform.position);
            if(delta.magnitude<0.23f)
            {
                WaypointsReached++;next=(next+1)%route.Length;pauseUntil=Time.time+0.35f+(identity%3)*0.16f;
                delta=Flat(route[next]-transform.position);
            }
            Vector3 desired=delta.normalized,steer=desired;
            bool nearPlayer=false;
            if(player!=null&&Mathf.Abs(player.position.y-transform.position.y)<1)
            {
                Vector3 offset=Flat(player.position-transform.position);
                nearPlayer=offset.magnitude<0.78f;
                if(offset.magnitude<1.7f&&Vector3.Dot(offset,desired)>0)
                    steer+=Vector3.Cross(Vector3.up,desired)*1.4f;
            }
            if(population!=null&&population.Actors!=null)
                foreach(var actor in population.Actors)
                {
                    if(actor==null||!actor.isActiveAndEnabled||actor.Motor==null||actor.Motor==this)continue;
                    Vector3 offset=actor.transform.position-transform.position;
                    if(Mathf.Abs(offset.y)>0.8f)continue;
                    offset.y=0;float distance=offset.magnitude;
                    float forward=Vector3.Dot(offset,desired);
                    float lateral=Vector3.Cross(desired,offset).y;
                    // Both walkers keep to their own right, rather than both waiting or mirroring left/right.
                    if(distance<2.0f&&forward>0&&Mathf.Abs(lateral)<0.85f)
                        steer+=Vector3.Cross(Vector3.up,desired)*1.65f*(1-distance/2.5f);
                    if(distance<0.8f&&distance>0.001f)steer-=offset.normalized*(0.8f-distance)*2.5f;
                }
            steer.Normalize();
            float best=float.NegativeInfinity;Vector3 direction=Vector3.zero;
            foreach(float angle in Angles)
            {
                Vector3 candidate=Quaternion.Euler(0,angle,0)*steer;
                if(!Clear(candidate,0.62f)||!HasFloor(candidate))continue;
                float score=Vector3.Dot(candidate,steer)*2+Vector3.Dot(candidate,desired)*0.4f;
                if(population!=null&&population.Actors!=null)
                    foreach(var actor in population.Actors)
                    {
                        if(actor==null||!actor.isActiveAndEnabled||actor.Motor==null||actor.Motor==this)continue;
                        Vector3 offset=actor.transform.position-transform.position;
                        if(Mathf.Abs(offset.y)>0.8f)continue;offset.y=0;
                        Vector3 relative=candidate*speed-actor.Motor.Velocity;
                        float t=Mathf.Clamp(Vector3.Dot(offset,relative)/Mathf.Max(relative.sqrMagnitude,0.01f),0,0.8f);
                        float separation=(offset-relative*t).magnitude;
                        score-=Mathf.Max(0,0.72f-separation)*7;
                    }
                if(score>best){best=score;direction=candidate;}
            }
            if(Time.time<pauseUntil||nearPlayer)direction=Vector3.zero;
            Vector3 before=transform.position;
            if(controller.isGrounded&&vertical<0)vertical=-2;
            vertical+=Physics.gravity.y*Time.deltaTime;
            controller.Move((direction*speed+Vector3.up*vertical)*Time.deltaTime);
            velocity=Flat(transform.position-before)/Mathf.Max(Time.deltaTime,0.0001f);ActualSpeed=velocity.magnitude;
            if(ActualSpeed>0.08f)transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(velocity),Time.deltaTime*5);
            if(Time.time>pauseUntil&&!nearPlayer&&ActualSpeed<0.08f)blockedFor+=Time.deltaTime;else blockedFor=0;
            LongestBlock=Mathf.Max(LongestBlock,blockedFor);
            // Recovery changes the destination, never disables collisions or teleports through the crowd.
            if(blockedFor>3.5f){next=(next+1)%route.Length;blockedFor=0;pauseUntil=Time.time+identity*0.09f;}
        }
        private bool Clear(Vector3 direction,float distance)
        {
            Vector3 bottom=transform.position+Vector3.up*(controller.radius+0.10f);
            Vector3 top=transform.position+Vector3.up*(controller.height-controller.radius);
            int count=Physics.CapsuleCastNonAlloc(bottom,top,controller.radius+0.025f,direction,hits,distance,~0,QueryTriggerInteraction.Ignore);
            if(count==hits.Length)return false;
            for(int i=0;i<count;i++)
            {
                var collider=hits[i].collider;
                if(collider==controller||collider.GetComponent<PrisonerMotor>()!=null)continue;
                if(hits[i].normal.y>0.65f)continue;
                return false;
            }
            return true;
        }
        private bool HasFloor(Vector3 direction)
        {
            return Physics.Raycast(transform.position+direction*0.55f+Vector3.up*0.22f,Vector3.down,0.6f,~(1<<2),QueryTriggerInteraction.Ignore);
        }
        private static Vector3 Flat(Vector3 p){p.y=0;return p;}
    }
}
