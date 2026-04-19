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

		[Header("Puzzle Info"), Space]
		[SerializeField] private string _clueTitle;
		[SerializeField, TextArea(3, 10)] private string _clueString;

		public bool IsPuzzleCompleted { get; set; }

		public event Action OnPuzzleCompleted;

		protected override void Setup()
		{
			base.Setup();

			_requiredObjectID = _requiredObjectID.Trim().ToUpper();
			_clueTitle = _clueTitle.Trim();
			_clueString = _clueString.Trim();
		}

		protected override void SubscribeEvents()
		{
			base.SubscribeEvents();
			_puzzleObject.OnPuzzleTriggered += PuzzleObject_OnPuzzleTriggered;
		}

		protected override void UnsubscribeEvents()
		{
			base.UnsubscribeEvents();
			_puzzleObject.OnPuzzleTriggered -= PuzzleObject_OnPuzzleTriggered;
		}

		protected override void CheckForInteraction()
		{
			if (!_puzzleObject.IsPuzzleTriggered)
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
			var nearestReceiver = GetNearestReceiver();

			if (nearestReceiver == null)
			{
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

		private void HandleWrongObjectSubmission()
		{
			Debug.Log("Wrong object submitted!");
		}

		private void PuzzleObject_OnPuzzleTriggered()
		{
			Debug.Log($"Puzzle \"{_clueTitle}\" triggered! Clue: \"{_clueString}\"");
		}
	}
}