using CSTGames.SharedResources;

namespace AforgeStudios.Signomaly
{
	public sealed class PlayerInteraction : InteractionSource
	{
		protected override void CheckForInteraction()
		{
			if (NewInputManager.Instance.WasPressedThisFrame(KeybindingAction.Interact))
			{
				InteractWithNearestReceiver();
			}
		}
	}
}