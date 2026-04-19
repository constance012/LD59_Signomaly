using System;
using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	[RequireComponent(typeof(IPuzzle))]
	public sealed class PuzzleObjectBehaviour : InteractableRoomObject
	{
		[Header("Visual Handler"), Space]
		[SerializeField] private PuzzleObjectVisualHandler _visualHandler;

		[Header("Puzzle Timer"), Space]
		[SerializeField] private TimerBase _timer;
		
		[Header("State Change Settings"), Space]
		[SerializeField] private float _autoInteractDelaySeconds = 2.5f;
		[SerializeField] private float _anomalyStateDurationSeconds = 7f;
		[SerializeField] private Vector2 _anomalyStateSwitchDelayRangeSeconds = new(10f, 15f);

		public bool IsPuzzleTriggered => _isPuzzleTriggered;

		public event Action OnPuzzleTriggered;

		private IPuzzle _currentPuzzle;
		private float _anomalyStateSwitchTimer;
		private float _anomalyStateElapsedTime;
		private float _autoInteractTimer;
		private bool _isPuzzleTriggered;

		private void Update()
		{
			if (_isPuzzleTriggered)
			{
				return;
			}

			CheckForAutoInteract();
			HandleStateSwitching();
		}

		protected override void SubscribeEvents()
		{
			base.SubscribeEvents();
			_currentPuzzle.OnPuzzleCompleted += IPuzzle_OnPuzzleCompleted;
		}

		protected override void UnsubscribeEvents()
		{
			base.UnsubscribeEvents();
			_currentPuzzle.OnPuzzleCompleted -= IPuzzle_OnPuzzleCompleted;
		}

		protected override void SetupComponents()
		{
			base.SetupComponents();

			_currentPuzzle = GetComponent<IPuzzle>();

			_anomalyStateSwitchTimer = _anomalyStateSwitchDelayRangeSeconds.RandomBetweenEnds();
			_autoInteractTimer = _autoInteractDelaySeconds;

			_timer.gameObject.SetActive(false);
		}

#region Interaction
		public override void Interact()
		{
			if (_isPuzzleTriggered)
			{
				return;
			}

			ShowAndStartTimer();
		}

		private void CheckForAutoInteract()
		{
			if (_interactionReceiver.CanBeInteractedWith)
			{
				_autoInteractTimer -= Time.deltaTime;

				if (_autoInteractTimer <= 0f)
				{
					Interact();
				}
			}
			else
			{
				_autoInteractTimer = _autoInteractDelaySeconds;
			}
		}

		private void ShowAndStartTimer()
		{
			_visualHandler.SwitchState(PuzzleObjectVisualHandler.VisualState.Anomaly);

			_timer.gameObject.SetActive(true);
			_timer.StartTimer();

			_isPuzzleTriggered = true;
			OnPuzzleTriggered?.Invoke();
		}
#endregion

#region State Handling
		private void HandleStateSwitching()
		{
			if (_visualHandler.IsInAnomalyState)
			{
				HandleAnomalyState();
			}
			else
			{
				CheckForAnomalyStateSwitch();
			}
		}

		private void CheckForAnomalyStateSwitch()
		{
			_anomalyStateSwitchTimer -= Time.deltaTime;

			if (_anomalyStateSwitchTimer <= 0f)
			{
				_visualHandler.SwitchState(PuzzleObjectVisualHandler.VisualState.Anomaly);
			}
		}

		private void HandleAnomalyState()
		{
			_anomalyStateElapsedTime += Time.deltaTime;

			if (_anomalyStateElapsedTime >= _anomalyStateDurationSeconds)
			{
				_visualHandler.SwitchState(PuzzleObjectVisualHandler.VisualState.Normal);

				_anomalyStateElapsedTime = 0f;
				_anomalyStateSwitchTimer = _anomalyStateSwitchDelayRangeSeconds.RandomBetweenEnds();
			}
		}
#endregion

#region Puzzle Handling
		private void IPuzzle_OnPuzzleCompleted()
		{
			_visualHandler.SwitchState(PuzzleObjectVisualHandler.VisualState.Normal);

			_timer.StopTimer();
			_timer.gameObject.SetActive(false);

			Debug.Log($"Puzzle solved: {gameObject.name}, remaining time: {_timer.RemainingTimeFormatted}", this);
		}
#endregion
	}
}