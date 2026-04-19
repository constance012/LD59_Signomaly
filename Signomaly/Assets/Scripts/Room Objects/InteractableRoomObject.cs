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

		protected virtual void SetupComponents()
		{
			_interactionReceiver.OnInteract.RemoveAllListeners();
			_interactionReceiver.OnInteract.AddListener(Interact);
		}

		public abstract void Interact();
	}
}