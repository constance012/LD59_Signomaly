using System;
using CSTGames.SharedResources;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AforgeStudios.Signomaly
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnOpenStartGame;
        public event Action OnPauseGame;
        public event Action OnSettingGame;

        public bool IsStartGame {get; private set;}

        public void OpenStartGame()
        {
            GlobalService.Instance.ToggleLockCursor(false);
            OnOpenStartGame?.Invoke();
            IsStartGame = false;
            Time.timeScale = 0;
        }

        public void PauseGame()
        {
            GlobalService.Instance.ToggleLockCursor(false);
            OnPauseGame?.Invoke();
            IsStartGame = true;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            GlobalService.Instance.ToggleLockCursor(true);
            Time.timeScale = 1;
        }

        public void NewGame()
        {
            GlobalService.Instance.ToggleLockCursor(true);
            Time.timeScale = 1;
            IsStartGame = true;

            SceneLoader.Instance.LoadSceneAsync(GameStateManager.GAMEPLAY_SCENE_NAME);
        }

        public void OpenSettingGame()
        {
            OnSettingGame?.Invoke();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}