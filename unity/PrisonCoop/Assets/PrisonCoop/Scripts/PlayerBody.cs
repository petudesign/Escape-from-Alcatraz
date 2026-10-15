using UnityEngine;

namespace PrisonCoop
{
    // Same character geometry for the local body and the peer avatar. Only the local head is hidden.
    public sealed class PlayerBody : MonoBehaviour
    {
        private PrisonerVisual visual;
        private static Material sharedPaint;
        private Vector3 previous,target;
        private float yaw,pitch;
        private bool local,hasPose;
        private GameObject model;
        public bool HasFullBody=>visual!=null;
        public void Initialize(PrisonerAppearance appearance,Shader shader,bool firstPerson)
        {
            if(model!=null){model.SetActive(false);Destroy(model);}
            local=firstPerson;previous=transform.position;
            model=new GameObject("Player character model");model.transform.SetParent(transform,false);model.transform.localPosition=Vector3.zero;
            visual=model.AddComponent<PrisonerVisual>();
            if(sharedPaint==null)sharedPaint=new Material(shader){name="Shared player character paint"};
            visual.Initialize(appearance,sharedPaint,0);
            visual.SetPlayerPose(firstPerson);visual.Animate(0,3);
        }
        public void SetModelVisible(bool value){if(model!=null)model.SetActive(value);}
        public void ReceivePose(Vector3 position,float rotation,float lookPitch)
        {target=position;yaw=rotation;pitch=lookPitch;if(!hasPose){transform.position=position;hasPose=true;previous=position;}}
        private void LateUpdate()
        {
            if(visual==null)return;
            if(!local&&hasPose)
            {
                transform.position=Vector3.Lerp(transform.position,target,1-Mathf.Exp(-Time.unscaledDeltaTime*16));
                transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,yaw,0),1-Mathf.Exp(-Time.unscaledDeltaTime*16));
            }
            float velocity=new Vector2(transform.position.x-previous.x,transform.position.z-previous.z).magnitude/Mathf.Max(Time.deltaTime,0.001f);
            visual.SetTravel(transform.InverseTransformDirection((transform.position-previous)/Mathf.Max(Time.deltaTime,0.001f)));
            previous=transform.position;visual.Animate(Mathf.Min(velocity,3),3);
            if(!local)visual.SetLookPitch(pitch);
        }
    }
}
