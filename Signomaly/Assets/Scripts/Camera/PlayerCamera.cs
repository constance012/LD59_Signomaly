using UnityEngine;
using UnityEngine.InputSystem;

namespace AforgeStudios.Signomaly
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private Transform orientation;

        [Header("Camera Settings"), Space]
        [SerializeField] private float startXCamera = 0f;
        [SerializeField] private float sensitive;
        [SerializeField] private float maxVerticalCameraRotation = 90f;

        private float verticalRotation;
        private float horizontalRotation;

        private Vector2 mouse;

        private void Start()
        {
            GlobalService.Instance.ToggleLockCursor(true);
            
            horizontalRotation = startXCamera;
        }

        private void Update()
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                return;
            }

            GetMouseInput();

            HandleCameraMovement();
        }

        public static bool IsInsideCameraFrustum(Collider collider)
        {
            var camera = Camera.main;
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

            if(GeometryUtility.TestPlanesAABB(planes, collider.bounds))
                return true;
            
            return false;
        }

        public static bool IsPointedAtByMouseCursor(InteractionReceiver currentReceiver, float interactRadius, out RaycastHit hitInfo)
        {
            var camera = Camera.main;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo, interactRadius))
            {
                if (hitInfo.collider.TryGetComponent(out InteractionReceiver receiver) && receiver == currentReceiver)
                {
                    return true;
                }
            }

            return false;
        }

        private void GetMouseInput()
        {
            mouse = Mouse.current.delta.ReadValue() * Time.deltaTime * sensitive;
        }

        private void HandleCameraMovement()
        {
            horizontalRotation += mouse.x;
            verticalRotation -= mouse.y;
            verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalCameraRotation, maxVerticalCameraRotation);

            //Rotate Camera and Orientation
            transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
            orientation.rotation = Quaternion.Euler(0, horizontalRotation, 0);
        }
    }
}