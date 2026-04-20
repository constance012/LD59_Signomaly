using CSTGames.SharedResources;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AforgeStudios.Signomaly
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThreeDPlayerController : MonoBehaviour, IPlayerController
    {
        [Header("References"), Space]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform orientation;

        [Header("Attributes"), Space]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float groundDrag;

        [Header("Ground Check"), Space]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask groundLayer;

        private Vector3 _inputValues;
        private bool _isGrounded;

        private void Update()
        {
            ReadInputValues();

            CheckForGround();

            SpeedControl();

            ApplyDrag();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        public void ReadInputValues()
		{
            if(NewInputManager.Instance.GetKeyDown(Key.Escape))
            {
                GameManager.Instance.PauseGame();
            }

			var inputVector = NewInputManager.Instance.ReadValue<Vector2>(KeybindingAction.Movement);

			_inputValues.x = inputVector.x;
			_inputValues.y = inputVector.y;
			_inputValues.Normalize();
		}

        public void UpdateVelocity()
        {
            Vector3 moveDirection = orientation.forward * _inputValues.y + orientation.right * _inputValues.x;

            if(_isGrounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            //Limit velocity if needed
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
            }
        }

        private void CheckForGround()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, groundLayer);
        }

        private void ApplyDrag()
        {
            if(_isGrounded)
            {
                rb.linearDamping = groundDrag;
            }
            else
            {
                rb.linearDamping = 0;
            }
        }
    }
}