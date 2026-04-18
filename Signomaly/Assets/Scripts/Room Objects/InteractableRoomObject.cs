using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public abstract class InteractableRoomObject : MonoBehaviour
	{
		[Header("Shared Settings"), Space]
		[SerializeField] protected string _objectName;
		[SerializeField] protected float _interactRadius = 2f;
		[SerializeField] protected int _maximumOverlappedColliders = 1;
		[SerializeField] protected LayerMask _playerLayer;

		public string ObjectName => _objectName;
		public bool IsPlayerInRange { get; protected set; }

		protected Collider[] _overlappedColliders;

		protected virtual void Awake()
		{
			SetupComponents();
		}

		protected virtual void LateUpdate()
		{
			CheckForPlayerInteraction();
		}

		protected virtual void SetupComponents()
		{
			_maximumOverlappedColliders = Mathf.Max(1, _maximumOverlappedColliders);
			_overlappedColliders = new Collider[_maximumOverlappedColliders];
		}

		protected void CheckForPlayerInteraction()
		{
			int count = Physics.OverlapSphereNonAlloc(transform.position, _interactRadius, _overlappedColliders, _playerLayer);

			IsPlayerInRange = count > 0;

			if (IsPlayerInRange)
			{
				ReadPlayerInput();
			}
		}

		protected void ReadPlayerInput()
		{
			if (LegacyInputManager.Instance.GetKeyDown(KeybindingAction.Interact))
			{
				Interact();
			}
		}

		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, _interactRadius);
		}

		public abstract void Interact();
	}
}