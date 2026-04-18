using System;
using CSTGames.SharedResources;
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            horizontalRotation = startXCamera;
        }

        private void Update()
        {
            GetMouseInput();

            HandleCameraMovement();
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