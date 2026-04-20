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
                IPickable interactable = GetClosetInteractableObject(GetInteractableList());
                
                if (interactable != null && !interactable.GetLockPickUpState() && 
                    PlayerCamera.IsInsideCameraFrustum(interactable.GetTransform().GetComponent<Collider>()) &&
                    grabbingObjTransform == null)
                {
                    interactable.PickUp(transform);
                    grabbingObjTransform = interactable.GetTransform();
                    PickUpObj(grabbingObjTransform);
                }
            }

            if (NewInputManager.Instance.GetKeyDown(dropKey))
            {
                if(grabbingObjTransform != null)
                {
                    DropDownObj(grabbingObjTransform);
                    grabbingObjTransform = null;
                }
            }
        }

        private void PickUpObj(Transform pickupObject)
        {
            if(pickupObject.TryGetComponent<PickableObjectPhysicHandler>(out PickableObjectPhysicHandler dragable))
            {
                dragable.PickObj(objectGrabTransform);
            }
        }

        private void DropDownObj(Transform pickupObject)
        {
            if(pickupObject.TryGetComponent<PickableObjectPhysicHandler>(out PickableObjectPhysicHandler dragable))
            {
                dragable.DropObj();
            }
        }

        public List<IPickable> GetInteractableList()
        {
            List<IPickable> interactableList = new List<IPickable>();

            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IPickable interactableObj))
                {
                    interactableList.Add(interactableObj);
                }
            }

            return interactableList;
        }

        public IPickable GetClosetInteractableObject(List<IPickable> interactables)
        {
            IPickable closetInteractableObj = null;

            foreach (IPickable obj in interactables)
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