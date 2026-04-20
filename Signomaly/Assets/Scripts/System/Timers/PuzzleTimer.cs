using System;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class PuzzleTimer : TimerBase
	{
		protected override void OnTimerComplete()
		{
			GameStateManager.Instance.GameOver();
		}
	}
}