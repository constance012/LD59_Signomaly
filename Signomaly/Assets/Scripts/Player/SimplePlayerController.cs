using System;
using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	[RequireComponent(typeof(Rigidbody))]
	public class SimplePlayerController : MonoBehaviour, IPlayerController
	{
		[Header("References"), Space]
		[SerializeField] private Rigidbody rb;
		[SerializeField] private Stats stats;

		[Header("Movement Settings"), Space]
		[SerializeField] private float acceleration;
		[SerializeField] private float deceleration;

		public static Vector3 Position { get; private set; }
		public static event Action<Vector3> OnVelocityChanged;

		private Vector3 _movementDirection;
		private Vector3 _previousDirection;
		private float _maxSpeed;
		private float _currentSpeedSquared;

		private void Start()
		{
			_maxSpeed = stats.GetDynamicStat(StatType.MoveSpeed);
		}

		private void Update()
		{
			ReadInputValues();

			if (_movementDirection.sqrMagnitude > .01f)
				_previousDirection = _movementDirection;
		}

		private void FixedUpdate()
		{
			UpdateVelocity();

			Position = rb.position;
		}

		public void ReadInputValues()
		{
			var inputVector = NewInputManager.Instance.ReadValue<Vector2>(KeybindingAction.Movement);

			_movementDirection.x = inputVector.x;
			_movementDirection.z = inputVector.y;
			_movementDirection.Normalize();
		}

		public void UpdateVelocity()
		{
			if (_movementDirection.sqrMagnitude > .01f)
			{
				Vector3 targetSpeed = _movementDirection * _maxSpeed;
				rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, targetSpeed, acceleration * Time.deltaTime);
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
			}

			OnVelocityChanged?.Invoke(rb.linearVelocity);
			_currentSpeedSquared = rb.linearVelocity.sqrMagnitude;
		}
	}
}
