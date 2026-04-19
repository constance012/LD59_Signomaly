using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public abstract class InteractableRoomObject : MonoBehaviour
	{
		[Header("General Info"), Space]
		[SerializeField] protected string _objectID;
		[SerializeField] protected string _objectName;

		[Header("Interaction Settings"), Space]
		[SerializeField] protected InteractionReceiver _interactionReceiver;

		public string ObjectID => _objectID;
		public string ObjectName => _objectName;

		protected virtual void Awake()
		{
			SetupComponents();
		}

		protected virtual void OnEnable()
		{
			SubscribeEvents();
		}

		protected virtual void OnDisable()
		{
			UnsubscribeEvents();
		}

		protected virtual void SubscribeEvents()
		{
			_interactionReceiver.OnInteract.AddListener(Interact);
		}

		protected virtual void UnsubscribeEvents()
		{
			_interactionReceiver.OnInteract.RemoveListener(Interact);
		}

		protected virtual void SetupComponents()
		{
			_objectID = _objectID.Trim().ToUpper();
			_objectName = _objectName.Trim();
		}

		public abstract void Interact();
	}
}