using System;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class PuzzleTimer : TimerBase
	{
		protected override void OnTimerComplete()
		{
			Debug.Log("Game over! Player failed to solve the puzzle in time.");
		}
	}
}