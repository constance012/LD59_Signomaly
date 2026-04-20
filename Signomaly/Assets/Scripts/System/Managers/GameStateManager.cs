using System;
using CSTGames.SharedResources;
using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class GameStateManager : Singleton<GameStateManager>
	{
		[Header("UI References"), Space]
		[SerializeField] private GameObject _gameOverUI;
		[SerializeField] private GameObject _gameWinUI;
		[SerializeField] private TextMeshProUGUI _remainingTimeText;

		[Header("Win Conditions"), Space]
		[SerializeField] private int _totalPuzzlesToSolve = 3;

		public int TotalPuzzlesToSolve => _totalPuzzlesToSolve;
		public int PuzzlesCompleted => _puzzlesCompleted;
		public bool IsWinConditionMet => _puzzlesCompleted >= _totalPuzzlesToSolve;

		public static event Action OnCompletedPuzzlesChanged;
		public static event Action OnGameEnded;

		private const string GAMEPLAY_SCENE_NAME = "Scenes/GamePlay";
		private const string MAIN_MENU_SCENE_NAME = "Scenes/Main Menu";

		private int _puzzlesCompleted = 0;

		private void Start()
		{
			GlobalService.Instance.TogglePlayerInput(true);
			GlobalService.Instance.ToggleLockCursor(true);

			_gameOverUI.SetActive(false);
			_gameWinUI.SetActive(false);
		}

#region Game State Management
		public void GameOver()
		{
			GlobalService.Instance.TogglePlayerInput(false);
			GlobalService.Instance.ToggleLockCursor(false);

			_gameOverUI.SetActive(true);
			OnGameEnded?.Invoke();
		}

		public void IncrementPuzzleCompletion()
		{
			_puzzlesCompleted++;
			OnCompletedPuzzlesChanged?.Invoke();
		}

		public void CheckForWinCondition()
		{
			if (IsWinConditionMet)
			{
				GameWin();
			}
		}

		private void GameWin()
		{
			GlobalService.Instance.TogglePlayerInput(false);
			GlobalService.Instance.ToggleLockCursor(false);

			_remainingTimeText.text = $"Remaining Time: {MainLevelTimer.Instance.RemainingTimeFormatted}";

			_gameWinUI.SetActive(true);
			OnGameEnded?.Invoke();
		}
#endregion

#region Button Callbacks
		public void RetryButton_OnClicked()
		{
			SceneLoader.Instance.LoadSceneAsync(GAMEPLAY_SCENE_NAME);
		}

		public void BackToMenuButton_OnClicked()
		{
			SceneLoader.Instance.LoadSceneAsync(MAIN_MENU_SCENE_NAME);
		}
#endregion
	}
}