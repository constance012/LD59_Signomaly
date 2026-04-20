using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class PlayerInteraction : InteractionSource
	{
		protected override void CheckForInteraction()
		{
			if (NewInputManager.Instance.WasPressedThisFrame(KeybindingAction.Interact))
			{
				InteractWithReceiverAtMousePosition();
			}
		}

		private void InteractWithReceiverAtMousePosition()
		{
			var nearestReceiver = GetNearestReceiver();

			if (PlayerCamera.IsPointedAtByMouseCursor(nearestReceiver, _interactRadius, out RaycastHit hitInfo))
			{
				if (hitInfo.collider.TryGetComponent(out InteractionReceiver receiver) && receiver.AllowsInteraction)
				{
					receiver.Interact();
				}
			}
		}
	}
}