using UnityEngine;
using UnityEngine.Events;

namespace AforgeStudios.Signomaly
{
	[RequireComponent(typeof(Collider))]
	public sealed class InteractionReceiver : MonoBehaviour
	{
		[Header("Interaction Events"), Space]
		public UnityEvent OnInteract;

		public bool CanBeInteractedWith { get; set; }

		public void Interact()
		{
			OnInteract?.Invoke();
		}
	}
}