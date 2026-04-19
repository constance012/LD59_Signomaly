using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class DragableObject : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider col;
        [SerializeField] InteractableObject iO;

        private Transform target;

        public void PickObj(Transform transform)
        {
            this.target = transform;
            rb.useGravity = false;
            rb.isKinematic = true;
            col.enabled = false;
        }

        public void DropObj()
        {
            this.target = null;
            rb.useGravity = true;
            rb.isKinematic = false;
            col.enabled = true;
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
