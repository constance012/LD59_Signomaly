using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class DistractionObject : InteractableRoomObject
	{
		public override void Interact()
		{
			Debug.Log($"Interacted with {ObjectName}");
		}
	}
}