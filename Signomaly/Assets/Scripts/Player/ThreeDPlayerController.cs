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
        [SerializeField] private Stats stats;

        [Header("Movement Settings"), Space]
        [SerializeField] private float acceleration;
        [SerializeField] private float deceleration;
        [SerializeField] private float footstepInterval = 0.5f;

        private const string FOOTSTEP_AUDIO_NAME = "Footsteps";

        private Vector3 _inputValues;
        private float _maxSpeed;
        private float _currentSpeedSquared;
        private float _footstepTimer;

        private void Start()
        {
            _maxSpeed = stats.GetDynamicStat(StatType.MoveSpeed);
        }

        private void Update()
        {
            ReadInputValues();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        public void ReadInputValues()
        {
            if (NewInputManager.Instance.GetKeyDown(Key.Escape) && InteractiveNPC.IsFirstTalkEverHappened)
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

            if (_inputValues.sqrMagnitude > .01f)
            {
                Vector3 targetSpeed = moveDirection * _maxSpeed;
                rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, targetSpeed, acceleration * Time.deltaTime);
                
                PlayFootstepAudio();
            }

            else if (_currentSpeedSquared > 0f)
            {
                Vector3 restVelocity = new Vector3
                {
                    x = 0f,
                    y = rb.linearVelocity.y,
                    z = 0f
                };
                
                rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, restVelocity, deceleration * Time.deltaTime);
                StopFootstepAudio();
            }

            _currentSpeedSquared = rb.linearVelocity.sqrMagnitude;
        }

        private void PlayFootstepAudio()
        {
            _footstepTimer -= Time.deltaTime;

            if (_footstepTimer <= 0f)
            {
                AudioManager.Instance.PlayWithRandomPitch(FOOTSTEP_AUDIO_NAME, .8f, 1.2f);
                _footstepTimer = footstepInterval;
            }
        }

        private void StopFootstepAudio()
        {
            AudioManager.Instance.Stop(FOOTSTEP_AUDIO_NAME);
            _footstepTimer = 0f;
        }
    }
}