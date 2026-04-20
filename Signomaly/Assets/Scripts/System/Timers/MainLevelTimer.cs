using System;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class MainLevelTimer : TimerBase
	{
		public static MainLevelTimer Instance { get; private set; }

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

		public void SubstractTime(TimeSpan timeToSubtract)
		{
			_duration -= timeToSubtract;
		}

		protected override void OnTimerComplete()
		{
			GameStateManager.Instance.GameOver();
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