using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Interact Settings"), Space]
    [SerializeField] private string interactText;
    [SerializeField] private bool lockInteract = false;
    [SerializeField] private float detectedRange;
    [SerializeField] private Vector3 offset;

    [Header("References"), Space]
    [SerializeField] private Transform interactableObjUIPrefab;

    private Key interactKey;
    private Transform playerTransform;
    private Transform interactUITransform;

    private bool canShowUI = true;

    private void Update()
    {
        if(CheckPlayer())
        {
            if(interactUITransform == null && canShowUI)
            {
                interactUITransform = Instantiate(interactableObjUIPrefab, transform.position + offset, Quaternion.identity, transform);
                InteractableObjUI interactableObjUI = GetComponentInChildren<InteractableObjUI>();
                if(interactableObjUI != null)
                    interactableObjUI.Show(interactKey.ToString(), interactText);
            }

            if(interactUITransform != null)
            {
                //UI Look At Player
                Vector3 reverseDirection = transform.position - playerTransform.position;

                if(reverseDirection != Vector3.zero)
                    interactUITransform.rotation = Quaternion.LookRotation(reverseDirection);       
            }
        }
        else
        {
            if(interactUITransform != null)
                DestroyUI();
        }
    }

    public void DestroyUI()
    {
        Destroy(interactUITransform.gameObject);
    }

    private bool CheckPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectedRange);
        foreach (Collider collider in colliders)
        {
            PlayerInteract playerInteract = collider.GetComponentInParent<PlayerInteract>();
            if (playerInteract != null)
            {
                playerTransform = playerInteract.transform;

                interactKey = playerInteract.GetInteractKey();

                return true;
            }
        }

        return false;
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public bool GetLockInteract()
    {
        return lockInteract;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        SetCanShowUI(false);
        DestroyUI();
    }

    public void SetLockInteract(bool lockInteract)
    {
        this.lockInteract = lockInteract;
    }

    public void SetCanShowUI(bool canShowUI)
    {
        this.canShowUI = canShowUI;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectedRange);
    }
}
