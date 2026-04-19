using CSTGames.SharedResources;

namespace AforgeStudios.Signomaly
{
	public sealed class PlayerInteractionSource : InteractionSource
	{
		protected override void CheckForInteraction()
		{
			if (LegacyInputManager.Instance.GetKeyDown(KeybindingAction.Interact))
			{
				InteractWithNearestReceiver();
			}
		}
	}
}