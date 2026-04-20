using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class MainLevelDoor : DoorBase
	{

		public override void Interact()
		{
			base.Interact();

			MainLevelTimer.Instance.StartTimer();
		}
	}
}