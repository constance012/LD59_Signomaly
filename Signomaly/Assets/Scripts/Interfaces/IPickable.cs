using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public interface IPickable
    {
        public void PickUp(Transform pickerTransform);
        public string GetPickUpText();
        public Transform GetTransform();
        public bool GetLockPickUpState();
        public void SetLockPickUpState(bool isLockedPickUp);
    }
}