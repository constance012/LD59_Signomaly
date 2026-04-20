using System;
using CSTGames.SharedResources;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnPauseGame;
        public event Action OnSettingGame;

        public void PauseGame()
        {
            GlobalService.Instance.ToggleLockCursor(false);
            OnPauseGame?.Invoke();
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            GlobalService.Instance.ToggleLockCursor(true);
            Time.timeScale = 1;
        }

        public void OpenSettingGame()
        {
            OnSettingGame?.Invoke();
        }
    }
}