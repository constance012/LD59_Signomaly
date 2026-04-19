using System;
using System.Collections.Generic;
using CSTGames.SharedResources;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AforgeStudios.Signomaly
{
    public class PlayerPickAndDrop : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] Camera playerCamera;
        [SerializeField] Transform objectGrabTransform;

        [Header("Keybinds"), Space]
        [SerializeField] private Key interactKey = Key.E;
        [SerializeField] private Key dropKey = Key.Q;

        [Header("Attributes"), Space]
        [SerializeField] private float interactRange = 2f;

        private Transform grabbingObjTransform;

        private void Update()
        {
            //Pick Up Object
            if (NewInputManager.Instance.GetKeyDown(interactKey))
            {
                IInteractable interactable = GetClosetInteractableObject(GetInteractableList());
                if (interactable != null && !interactable.GetLockInteract() && CheckObjectBoundsInCamera(interactable.GetTransform().GetComponent<MeshRenderer>()) && grabbingObjTransform == null)
                {
                    interactable.Interact(transform);
                    grabbingObjTransform = interactable.GetTransform();
                    PickUpObj(grabbingObjTransform);
                }
            }

            if (NewInputManager.Instance.GetKeyDown(dropKey))
            {
                if(grabbingObjTransform != null)
                {
                    DropDownObj(grabbingObjTransform);
                }
            }
        }

        private void PickUpObj(Transform pickupObject)
        {
            if(pickupObject.TryGetComponent<DragableObject>(out DragableObject dragable))
            {
                dragable.PickObj(objectGrabTransform);
            }
        }

        private void DropDownObj(Transform pickupObject)
        {
            if(pickupObject.TryGetComponent<DragableObject>(out DragableObject dragable))
            {
                dragable.DropObj();
            }
        }

        public List<IInteractable> GetInteractableList()
        {
            List<IInteractable> interactableList = new List<IInteractable>();

            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable interactableObj))
                {
                    interactableList.Add(interactableObj);
                }
            }

            return interactableList;
        }

        public IInteractable GetClosetInteractableObject(List<IInteractable> interactables)
        {
            IInteractable closetInteractableObj = null;

            foreach (IInteractable obj in interactables)
            {
                if (closetInteractableObj == null)
                    closetInteractableObj = obj;
                else
                {

                    if (Vector3.Distance(transform.position, closetInteractableObj.GetTransform().position)
                    > Vector3.Distance(transform.position, obj.GetTransform().position))
                        closetInteractableObj = obj;
                }
            }

            return closetInteractableObj;
        }

        public bool CheckObjectBoundsInCamera(MeshRenderer reder)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

            if(GeometryUtility.TestPlanesAABB(planes, reder.bounds))
                return true;
            
            return false;
        }

        public Key GetInteractKey()
        {
            return interactKey;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}