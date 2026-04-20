using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class PickableObjectPhysicHandler : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider col;
        [SerializeField] PickableObjectBehaviour pickableObjBehaviour;
        [SerializeField] PlayerPickAndDrop playerPickAndDrop;

        private Transform target;
        private bool isPicked;

        public void PickObj(Transform transform)
        {
            this.target = transform;
            rb.useGravity = false;
            col.isTrigger = true;
            // rb.isKinematic = true;
            isPicked = true;
        }

        public void DropObj()
        {
            this.target = null;
            rb.useGravity = true;
            col.isTrigger = false;
            // rb.isKinematic = false;
            isPicked = false;
            pickableObjBehaviour.SetCanShowUI(true);
        }

        private void FixedUpdate()
        {
            if(target != null)
            {
                rb.MovePosition(target.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(isPicked && other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                playerPickAndDrop.DropDownObj(transform);
            }
        }
    }
}
