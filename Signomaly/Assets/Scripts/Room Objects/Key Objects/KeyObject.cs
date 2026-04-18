using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class KeyObject : InteractableRoomObject
	{
		public override void Interact()
		{
			Debug.Log($"Interacted with key object: {ObjectName}. Perform pick up or other actions.");
		}
	}
}