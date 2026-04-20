using System;
using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class PasscodePuzzleInteraction : InteractionSource, IPuzzle
	{
		[Header("References"), Space]
		[SerializeField] private PuzzleObjectBehaviour _puzzleObject;
		[SerializeField] private PasscodePanelUI _passcodePanelUI;
		
		[Header("Required Object Settings"), Space]
		[SerializeField] private string _requiredPasscode;

		[Header("Instruction Note"), Space]
		[SerializeField] private GameObject _instructionNotePrefab;

		public bool IsPuzzleCompleted { get; set; }

		public event Action OnPuzzleCompleted;

		protected override void Setup()
		{
			base.Setup();

			_requiredPasscode = _requiredPasscode.Trim().ToUpper();
		}

		protected override void CheckForInteraction()
		{
			if (!_puzzleObject.IsThisPuzzleStarted() || IsPuzzleCompleted)
			{
				return;
			}

			if (LegacyInputManager.Instance.GetKeyDown(KeybindingAction.Interact))
			{
				OpenPasscodePanelUI();
			}
		}

		private void OpenPasscodePanelUI()
		{
			_passcodePanelUI.Show();
		}

		public bool SubmitPasscode(string submittedPasscode)
		{
			if (submittedPasscode.Trim().ToUpper() == _requiredPasscode)
			{
				SolvePuzzle();
				return true;
			}

			return false;
		}

		public void SolvePuzzle()
		{
			IsPuzzleCompleted = true;
			OnPuzzleCompleted?.Invoke();
		}

		public void ShowInstructions()
		{
			Instantiate(_instructionNotePrefab, transform.position + transform.forward * 0.5f, Quaternion.identity);
		}
	}
}