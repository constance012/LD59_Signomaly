using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class FindingRequiredObject : InteractableRoomObject
	{
		public override void Interact()
		{
			Debug.Log($"Interacted with required object: {ObjectName}.");
		}
	}
}