using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public abstract class InteractionSource : MonoBehaviour
	{
		[Header("Interaction Settings"), Space]
		[SerializeField] protected float _interactRadius = 2f;
		[SerializeField] protected int _maximumOverlappedColliders = 3;
		[SerializeField] protected LayerMask _includedLayers;

		public bool AreReceiversInRange { get; protected set; }

		protected Collider[] _overlappedColliders;
		protected InteractionReceiver[] _receiversInRange;
		protected int _previousReceiversInRangeCount;

		protected virtual void Awake()
		{
			Setup();
		}

		protected virtual void OnEnable()
		{
			SubscribeEvents();
		}

		protected virtual void OnDisable()
		{
			UnsubscribeEvents();
		}

		protected virtual void LateUpdate()
		{
			DetectReceivers();
		}

#region Setup
		protected virtual void SubscribeEvents() { }

		protected virtual void UnsubscribeEvents() { }

		protected virtual void Setup()
		{
			_maximumOverlappedColliders = Mathf.Max(1, _maximumOverlappedColliders);

			_overlappedColliders = new Collider[_maximumOverlappedColliders];
			_receiversInRange = new InteractionReceiver[_maximumOverlappedColliders];
		}
#endregion

#region Interaction Detection and Handling
		protected void DetectReceivers()
		{
			ClearCollidersArray();
			int newReceiversCount = Physics.OverlapSphereNonAlloc(transform.position, _interactRadius, _overlappedColliders, _includedLayers);

			AreReceiversInRange = newReceiversCount > 0;

			if (newReceiversCount != _previousReceiversInRangeCount)
			{
				DisposeOldReceivers();
				FetchNewReceivers();

				_previousReceiversInRangeCount = newReceiversCount;
			}

			if (AreReceiversInRange)
			{
				CheckForInteraction();
			}
		}

		protected void ClearCollidersArray()
		{
			for (int i = 0; i < _overlappedColliders.Length; i++)
			{
				_overlappedColliders[i] = null;
			}
		}
#endregion

#region Receiver Interactions
		protected void FetchNewReceivers()
		{
			for (int i = 0; i < _overlappedColliders.Length; i++)
			{
				var collider = _overlappedColliders[i];

				if (collider != null && collider.TryGetComponent(out InteractionReceiver newReceiver))
				{
					newReceiver.AllowsInteraction = true;
					_receiversInRange[i] = newReceiver;
				}
			}
		}

		protected void DisposeOldReceivers()
		{
			for (int i = 0; i < _receiversInRange.Length; i++)
			{
				var oldReceiver = _receiversInRange[i];
				if (oldReceiver != null)
				{
					oldReceiver.AllowsInteraction = false;
					_receiversInRange[i] = null;
				}
			}
		}

		protected InteractionReceiver GetNearestReceiver()
		{
			InteractionReceiver nearestReceiver = null;
			float nearestDistanceSqr = float.MaxValue;

			foreach (var receiver in _receiversInRange)
			{
				if (receiver == null || !receiver.AllowsInteraction || !receiver.IsVisibleByCamera())
				{
					continue;
				}

				float distanceSqr = (receiver.transform.position - transform.position).sqrMagnitude;

				if (distanceSqr < nearestDistanceSqr)
				{
					nearestDistanceSqr = distanceSqr;
					nearestReceiver = receiver;
				}
			}

			return nearestReceiver;
		}

		protected InteractionReceiver GetNearestReceiverWithLayerMask(LayerMask layerMask)
		{
			InteractionReceiver nearestReceiver = null;
			float nearestDistanceSqr = float.MaxValue;

			foreach (var receiver in _receiversInRange)
			{
				if (receiver == null || !receiver.AllowsInteraction || !receiver.IsVisibleByCamera() || !IsReceiverInLayerMask(receiver, layerMask))
				{
					continue;
				}

				float distanceSqr = (receiver.transform.position - transform.position).sqrMagnitude;

				if (distanceSqr < nearestDistanceSqr)
				{
					nearestDistanceSqr = distanceSqr;
					nearestReceiver = receiver;
				}
			}

			return nearestReceiver;
		}

		protected void InteractWithNearestReceiver()
		{
			var nearestReceiver = GetNearestReceiver();

			if (nearestReceiver != null)
			{
				nearestReceiver.Interact();
			}
		}

		protected void InteractWithAllReceivers()
		{
			foreach (var receiver in _receiversInRange)
			{
				if (receiver == null || !receiver.AllowsInteraction || !receiver.IsVisibleByCamera())
				{
					continue;
				}

				receiver.Interact();
			}
		}
		
		protected abstract void CheckForInteraction();
#endregion

		private bool IsReceiverInLayerMask(InteractionReceiver receiver, LayerMask layerMask)
		{
			return (layerMask.value & (1 << receiver.gameObject.layer)) > 0;
		}

		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, _interactRadius);
		}
	}
}