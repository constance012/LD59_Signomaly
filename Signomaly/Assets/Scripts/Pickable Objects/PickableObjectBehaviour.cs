using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AforgeStudios.Signomaly
{
    public class PickableObjectBehaviour : MonoBehaviour, IPickable
    {
        [Header("Interact Settings"), Space]
        [SerializeField] private string interactText;
        [SerializeField] private bool lockInteract = false;
        [SerializeField] private float detectedRange;
        [SerializeField] private Vector3 offset;

        [Header("References"), Space]
        [SerializeField] private Transform interactableObjUIPrefab;

        private Key interactKey;
        private Transform interactUITransform;

        private bool canShowUI = true;

        private void Update()
        {
            if (CheckPlayer())
            {
                if (interactUITransform == null)
                {
                    interactUITransform = Instantiate(interactableObjUIPrefab, transform.position + offset, Quaternion.identity);
                }

                TryShowUIPrompt();
            }
            else if (interactUITransform != null)
            {
                SetUIPromptActive(false);
            }
        }

        private bool CheckPlayer()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectedRange);
            foreach (Collider collider in colliders)
            {
                PlayerPickAndDrop playerInteract = collider.GetComponentInParent<PlayerPickAndDrop>();
                if (playerInteract != null)
                {
                    interactKey = playerInteract.GetInteractKey();

                    return true;
                }
            }

            return false;
        }

        public string GetPickUpText()
        {
            return interactText;
        }

        public bool GetLockPickUpState()
        {
            return lockInteract;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void PickUp(Transform interactorTransform)
        {
            SetCanShowUI(false);
            if(interactUITransform != null)
            {
                SetUIPromptActive(false);
            }
        }

        public void SetLockPickUpState(bool lockInteract)
        {
            this.lockInteract = lockInteract;
        }

        public void SetCanShowUI(bool canShowUI)
        {
            this.canShowUI = canShowUI;
        }

        private void SetUIPromptActive(bool isActive)
        {
            interactUITransform.gameObject.SetActive(isActive);
        }

        private void TryShowUIPrompt()
        {
            if (canShowUI && interactUITransform != null)
            {
                SetUIPromptActive(true);
                InteractableObjUI interactableObjUI = interactUITransform.GetComponentInChildren<InteractableObjUI>();
                if (interactableObjUI != null)
                {
                    interactableObjUI.Show(interactKey.ToString(), interactText);
                    interactUITransform.position = transform.position + offset;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectedRange);
        }
    }
}
