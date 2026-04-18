using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
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

		private float _anomalyStateSwitchTimer;
		private float _anomalyStateElapsedTime;
		private float _autoInteractTimer;
		private bool _isPuzzleTimerStarted;

		private void Update()
		{
			if (_isPuzzleTimerStarted)
			{
				return;
			}

			CheckForAutoInteract();
			HandleStateSwitching();
		}

		protected override void SetupComponents()
		{
			base.SetupComponents();

			_anomalyStateSwitchTimer = _anomalyStateSwitchDelayRangeSeconds.RandomBetweenEnds();
			_autoInteractTimer = _autoInteractDelaySeconds;

			_timer.gameObject.SetActive(false);
		}

#region Interaction
		public override void Interact()
		{
			ShowAndStartTimer();
		}

		private void CheckForAutoInteract()
		{
			if (IsPlayerInRange)
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
			_timer.gameObject.SetActive(true);
			_timer.StartTimer();

			_isPuzzleTimerStarted = true;
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
	}
}