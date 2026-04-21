using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class PasscodeClueObject : InteractableRoomObject
	{
		[Header("Clue Info"), Space]
		[SerializeField] private string _clueTitle;
		[SerializeField, TextArea(3, 10)] private string _clueString;

		public override void Interact()
		{
			AudioManager.Instance.Play("Read Clue");
			PuzzleInstructionUIHandler.Instance.ShowInstructions(_clueTitle, _clueString);
		}
	}
}