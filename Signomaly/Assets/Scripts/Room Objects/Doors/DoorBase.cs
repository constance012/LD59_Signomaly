using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public abstract class DoorBase : InteractableRoomObject
	{
		[Header("References"), Space]
		[SerializeField] protected Transform _hingesTransform;
		[SerializeField] protected Collider _doorCollider;
		
		[Header("Door Settings"), Space]
		[SerializeField] protected float _openAngle = 90f;
		[SerializeField] protected float _openingDuration = .5f;
		[SerializeField] protected DoorOpenOrientation _openOrientation;
		[SerializeField] protected bool _isLocked;

		protected bool _isOpen;
		protected bool _isAnimating;

		public override void Interact()
		{
			if ((_isLocked && !_isOpen) || _isAnimating)
			{
				return;
			}

			if (_isOpen)
			{
				CloseDoor();
			}
			else
			{
				OpenDoor();
			}
		}

		protected async void OpenDoor()
		{
			float openAngle = _openOrientation == DoorOpenOrientation.Inwards ? -_openAngle : _openAngle;

			await TweenDoorHinges(openAngle);
			
			_isOpen = true;
			_doorCollider.isTrigger = true;
		}

		protected async void CloseDoor()
		{
			await TweenDoorHinges(0f);
			
			_isOpen = false;
			_doorCollider.isTrigger = false;
		}

		protected async UniTask TweenDoorHinges(float angle)
		{
			Vector3 hingeRotation = new (0f, angle, 0f);

			_isAnimating = true;

			await _hingesTransform.DORotate(hingeRotation, _openingDuration)
				.SetEase(Ease.InOutSine)
				.AsyncWaitForCompletion();
			
			_isAnimating = false;
		}

		public enum DoorOpenOrientation
		{
			Inwards,
			Outwards
		}
	}
}