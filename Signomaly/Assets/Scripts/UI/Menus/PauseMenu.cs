using System;
using AforgeStudios.Signomaly;
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
            
        }
    }    
}
