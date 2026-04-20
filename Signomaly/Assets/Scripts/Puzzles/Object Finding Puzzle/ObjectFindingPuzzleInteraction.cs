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

		private InteractionReceiver _ownReceiver;

		protected override void Setup()
		{
			base.Setup();
			_ownReceiver = GetComponentInChildren<InteractionReceiver>();

			_requiredObjectID = _requiredObjectID.Trim().ToUpper();
			_clueTitle = _clueTitle.Trim();
			_clueString = _clueString.Trim();
		}

		protected override void CheckForInteraction()
		{
			if (!_puzzleObject.IsThisPuzzleStarted() || IsPuzzleCompleted)
			{
				return;
			}

			if (NewInputManager.Instance.WasPressedThisFrame(KeybindingAction.Interact) &&
				PlayerCamera.IsPointedAtByMouseCursor(_ownReceiver, _interactRadius, out _))
			{
				TrySubmitRequiredObject();
			}
		}

		private bool TrySubmitRequiredObject()
		{
			var nearestReceiver = GetNearestReceiverWithLayerMask(_keyObjectLayerMask);

			if (nearestReceiver == null || nearestReceiver.IsPointedAtByMouseCursor(_interactRadius))
			{
				ShowInstructions();
				return false;
			}

			FindingRequiredObject requiredObject = nearestReceiver.GetComponentInParent<FindingRequiredObject>();

			if (requiredObject != null && requiredObject.ObjectID == _requiredObjectID)
			{
				SolvePuzzle();
				Destroy(requiredObject.gameObject);

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