using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class ExitDoor : DoorBase
	{
		public override void Interact()
		{
			base.Interact();

			GameStateManager.Instance.CheckForWinCondition();
		}
	}
}