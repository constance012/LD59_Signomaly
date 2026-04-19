using System;
using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class ObjectFindingPuzzleInteraction : InteractionSource, IPuzzle
	{
		[Header("Refernces"), Space]
		[SerializeField] private PuzzleObjectBehaviour _puzzleObject;
		
		[Header("Required Object Settings"), Space]
		[SerializeField] private string _requiredObjectID;
		[SerializeField] private LayerMask _keyObjectLayerMask;

		[Header("Puzzle Info"), Space]
		[SerializeField] private string _clueTitle;
		[SerializeField, TextArea(3, 10)] private string _clueString;

		public bool IsPuzzleCompleted { get; set; }

		public event Action OnPuzzleCompleted;

		private const string WRONG_OBJECT_SUBMISSION_MESSAGE = "This object is incorrect!";

		protected override void Setup()
		{
			base.Setup();

			_requiredObjectID = _requiredObjectID.Trim().ToUpper();
			_clueTitle = _clueTitle.Trim();
			_clueString = _clueString.Trim();
		}

		protected override void CheckForInteraction()
		{
			if (!_puzzleObject.IsPuzzleTriggered || IsPuzzleCompleted)
			{
				return;
			}

			if (LegacyInputManager.Instance.GetKeyDown(KeybindingAction.Interact))
			{
				TrySubmitKeyObject();
			}
		}

		private bool TrySubmitKeyObject()
		{
			var nearestReceiver = GetNearestReceiverWithLayerMask(_keyObjectLayerMask);

			if (nearestReceiver == null)
			{
				ShowInstructions();
				return false;
			}

			KeyObject keyObject = nearestReceiver.GetComponentInParent<KeyObject>();

			if (keyObject != null && keyObject.ObjectID == _requiredObjectID)
			{
				SolvePuzzle();
				return true;
			}

			HandleWrongObjectSubmission();
			return false;
		}

		public void SolvePuzzle()
		{
			IsPuzzleCompleted = true;
			OnPuzzleCompleted?.Invoke();
		}

		public void ShowInstructions()
		{
			PuzzleInstructionUIHandler.Instance.ShowInstructions(_clueTitle, _clueString);
		}
		
		private void HandleWrongObjectSubmission()
		{
			PuzzleInstructionUIHandler.Instance.ShowInstructions(_clueTitle, WRONG_OBJECT_SUBMISSION_MESSAGE);
		}
	}
}