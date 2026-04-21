using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class MainLevelDoor : DoorBase
	{
		protected override void SubscribeEvents()
		{
			base.SubscribeEvents();
			GameStateManager.OnGameEnded += MainLevelTimer.Instance.StopTimer;
		}

		protected override void UnsubscribeEvents()
		{
			base.UnsubscribeEvents();
			GameStateManager.OnGameEnded -= MainLevelTimer.Instance.StopTimer;
		}

		public override void Interact()
		{
			if (!InteractiveNPC.IsFirstTalkEverHappened)
			{
				return;
			}

			base.Interact();

			MainLevelTimer.Instance.StartTimer();
		}
	}
}