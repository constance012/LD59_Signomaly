using System;

namespace AforgeStudios.Signomaly
{
	public interface IPuzzle
	{
		bool IsPuzzleCompleted { get; set; }
		event Action OnPuzzleCompleted;

		void SolvePuzzle();
		void ShowInstructions();
	}
}