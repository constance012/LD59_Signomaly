using System;
using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class MainLevelTimer : TimerBase
	{
		[Header("Sound Settings"), Space]
		[SerializeField] private int _tickingSoundStartThresholdSeconds = 180;

		public static MainLevelTimer Instance { get; private set; }

		private bool _isTickingSoundPlayed;

		protected override void Awake()
		{
			base.Awake();
			MakeSingleton();
		}

		private void Update()
		{
#if UNITY_EDITOR
			DebugTimer();
#endif
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
			HandleTickingSound();
		}

		public void SubstractTime(TimeSpan timeToSubtract)
		{
			_duration -= timeToSubtract;
		}

		protected override void OnTimerComplete()
		{
			GameStateManager.Instance.GameOver();
		}

		private void HandleTickingSound()
		{
			if (_isTickingSoundPlayed)
			{
				return;
			}

			if (Duration <= TimeSpan.FromSeconds(_tickingSoundStartThresholdSeconds))
			{
				AudioManager.Instance.Play("Timer Alert");
				_isTickingSoundPlayed = true;
			}
		}

		private void MakeSingleton()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this.gameObject);
			}
		}

#if UNITY_EDITOR
		private void DebugTimer()
		{
			if (Input.GetKeyDown(KeyCode.PageUp))
			{
				StartTimer();
			}
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				SubstractTime(TimeSpan.FromSeconds(_debugSubtractSeconds));
			}
		}
#endif
	}
}