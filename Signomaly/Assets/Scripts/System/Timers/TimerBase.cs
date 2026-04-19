using System;
using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public abstract class TimerBase : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] protected Canvas _timerCanvas;
		[SerializeField] protected TextMeshProUGUI _timerText;

		[Header("Timer Settings"), Space]
		[SerializeField] protected float _durationSeconds = 1f;
		[SerializeField] protected bool _startOnAwake = false;

		[Header("Debug Settings"), Space]
		[SerializeField] protected float _debugSubtractSeconds = 10f;

		public TimeSpan Duration => _duration;
		public string RemainingTimeFormatted => $"{_duration:mm\\:ss}";
		public bool IsRunning { get; set; }
		
		protected TimeSpan _duration;

		protected virtual void Awake()
		{
			SetupTimer();
		}

		protected virtual void Start()
		{
			if (_startOnAwake)
			{
				StartTimer();
			}
		}

		protected virtual void LateUpdate()
		{
			Tick(Time.deltaTime);
			UpdateTimerUI();
		}

		protected virtual void SetupTimer()
		{
			_timerCanvas.worldCamera = Camera.main;
			_duration = TimeSpan.FromSeconds(_durationSeconds);

			UpdateTimerUI();
		}
		
		public virtual void StartTimer()
		{
			IsRunning = true;
		}

		public virtual void StopTimer()
		{
			IsRunning = false;
		}

		public virtual void CompleteTimer()
		{
			_duration = TimeSpan.Zero;
			IsRunning = false;

			OnTimerComplete();
		}

		public void Tick(float deltaTime)
		{
			if (!IsRunning)
			{
				return;
			}

			_duration -= TimeSpan.FromSeconds(deltaTime);

			if (_duration <= TimeSpan.Zero)
			{
				CompleteTimer();
				return;
			}
		}

		protected virtual void UpdateTimerUI()
		{
			_timerText.text = RemainingTimeFormatted;
		}

		protected abstract void OnTimerComplete();
	}
}