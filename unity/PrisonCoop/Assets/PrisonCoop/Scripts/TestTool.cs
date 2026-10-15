using UnityEngine;

namespace PrisonCoop
{
    // A reversible local interaction, deliberately not a network inventory.
    public sealed class TestTool : MonoBehaviour
    {
        private Vector3 homePosition;
        private Quaternion homeRotation;
        private Vector3 homeScale;
        private Transform owner;
        public bool IsHeld => owner != null;

        public void SetOwner(Transform hand)
        {
            if (owner == hand) return;
            if (IsHeld) Toggle(owner);
            if (hand != null) Toggle(hand);
        }

        private void Awake()
        {
            homePosition = transform.position;
            homeRotation = transform.rotation;
            homeScale = transform.localScale;
        }

        public void Toggle(Transform hand)
        {
            if (IsHeld)
            {
                transform.SetParent(null, true);
                transform.SetPositionAndRotation(homePosition, homeRotation);
                transform.localScale = homeScale;
                owner = null;
                GetComponent<Collider>().enabled = true;
            }
            else
            {
                owner = hand;
                GetComponent<Collider>().enabled = false;
                transform.SetParent(hand, false);
                var scale = hand.lossyScale;
                transform.localScale = new Vector3(homeScale.x / scale.x, homeScale.y / scale.y, homeScale.z / scale.z);
                transform.localPosition = new Vector3(0.32f, -0.28f, 0.65f);
                transform.localRotation = Quaternion.Euler(0, 25, -20);
            }
        }
    }
}
