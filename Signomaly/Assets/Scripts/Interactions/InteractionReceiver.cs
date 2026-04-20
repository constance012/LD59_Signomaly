using UnityEngine;
using UnityEngine.Events;

namespace AforgeStudios.Signomaly
{
	[RequireComponent(typeof(Collider))]
	public sealed class InteractionReceiver : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] private Collider _ownCollider;

		[Header("Interaction Events"), Space]
		public UnityEvent OnInteract;

		public bool AllowsInteraction { get; set; }

		public void Interact()
		{
			OnInteract?.Invoke();
		}

		public bool IsVisibleByCamera()
		{
			return PlayerCamera.IsInsideCameraFrustum(_ownCollider);
		}

		public bool IsPointedAtByMouseCursor(float interactRadius)
		{
			return PlayerCamera.IsPointedAtByMouseCursor(this, interactRadius, out _);
		}
	}
}