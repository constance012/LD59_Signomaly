using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class DistractionObject : InteractableRoomObject
	{
		public override void Interact()
		{
			Debug.Log($"Interacted with {ObjectName}");
		}
	}
}