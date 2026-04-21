using System;
using AforgeStudios.Signomaly;
using CSTGames.SharedResources;
using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private GameObject containerGameObj;

        private void Start()
        {
            GameManager.Instance.OnPauseGame += GameManager_PauseMenu_OnPauseGame;
            GameManager.Instance.OnSettingGame += GameManager_PauseMenu_OnSettingGame;
            GameManager.Instance.OnOpenStartGame += GameManager_PauseMenu_OnStartGame;
        }

        private void GameManager_PauseMenu_OnStartGame()
        {
            containerGameObj.SetActive(false);
        }

        private void GameManager_PauseMenu_OnSettingGame()
        {
            containerGameObj.SetActive(false);
        }

        private void GameManager_PauseMenu_OnPauseGame()
        {
            containerGameObj.SetActive(true);
        }

        public void ResumeButtonClick()
        {
            GameManager.Instance.ResumeGame();
            containerGameObj.SetActive(false);
        }

        public void SettingButtonClick()
        {
            GameManager.Instance.OpenSettingGame();
        }

        public void BackButtonClick()
        {
            SceneLoader.Instance.LoadSceneAsync(GameStateManager.MAIN_MENU_SCENE_NAME);
        }
    }    
}
