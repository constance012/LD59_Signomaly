using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class PickableObjectPhysicHandler : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private Rigidbody rb;
        [SerializeField] PickableObjectBehaviour iO;

        private Transform target;

        public void PickObj(Transform transform)
        {
            this.target = transform;
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        public void DropObj()
        {
            this.target = null;
            rb.useGravity = true;
            rb.isKinematic = false;
            iO.SetCanShowUI(true);
        }

        private void FixedUpdate()
        {
            if(target != null)
            {
                rb.MovePosition(target.position);
            }
        }
    }
}
