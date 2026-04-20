using System;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class StartMenu : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private GameObject containerGameObj;

        private void Start()
        {
            GameManager.Instance.OnOpenStartGame += GameManager_StartMenu_OnStartGame;
            GameManager.Instance.OnPauseGame += GameManager_StartMenu_OnPauseGame;
            GameManager.Instance.OnSettingGame += GameManager_StartMenu_OnSettingGame;
        }

        private void GameManager_StartMenu_OnStartGame()
        {
            containerGameObj.SetActive(true);
        }

        private void GameManager_StartMenu_OnSettingGame()
        {
            containerGameObj.SetActive(false);
        }

        private void GameManager_StartMenu_OnPauseGame()
        {
            containerGameObj.SetActive(false);
        }

        public void NewGameButtonClick()
        {
            containerGameObj.SetActive(false);
            GameManager.Instance.NewGame();
        }

        public void SettingButtonClick()
        {
            GameManager.Instance.OpenSettingGame();
        }

        public void QuitButtonClick()
        {
            GameManager.Instance.QuitGame();
        }
    }
}