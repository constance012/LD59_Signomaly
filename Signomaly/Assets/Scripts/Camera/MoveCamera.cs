using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class MoveCamera : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private Transform cameraPosition;

        private void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}