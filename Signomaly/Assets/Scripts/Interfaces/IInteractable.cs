using UnityEngine;

public interface IInteractable
{
    public void Interact(Transform interactorTransform);
    public string GetInteractText();
    public Transform GetTransform();
    public bool GetLockInteract();
    public void SetLockInteract(bool lockInteract);
}
