using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AforgeStudios.Signomaly
{
    public class SettingMenu : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private GameObject containerGameObj;
        [SerializeField] private GameObject videoContainerGameObj;
        [SerializeField] private GameObject audioContainerGameObj;
        [SerializeField] private TextMeshProUGUI videoTitle;
        [SerializeField] private TextMeshProUGUI audioTitle;
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        [Header("Audio Mixer"), Space]
        [SerializeField] private AudioMixer mixer;

        private Resolution[] resolutions;
        private bool isVideoPanel;

        private void Start()
        {
            GameManager.Instance.OnPauseGame += GameManager_SettingMenu_OnPauseGame;
            GameManager.Instance.OnSettingGame += GameManager_SettingMenu_OnSettingGame;
            GameManager.Instance.OnOpenStartGame += GameManager_SettingMenu_OnStartGame;
        }

        private void GameManager_SettingMenu_OnStartGame()
        {
            containerGameObj.SetActive(false);
        }

        private void GameManager_SettingMenu_OnPauseGame()
        {
            containerGameObj.SetActive(false);
        }

        private void GameManager_SettingMenu_OnSettingGame()
        {
            ShowResolutionDropDown();
            containerGameObj.SetActive(true);
        }

        public void VideoButtonClick()
        {
            OpenVideoPanel(true);
        }

        public void AudioButtonClick()
        {
            OpenVideoPanel(false);
        }

        private void OpenVideoPanel(bool openVideo)
        {
            ColorUtility.TryParseHtmlString("#D4D4E0", out Color chosencolor);
            ColorUtility.TryParseHtmlString("#6B6B7A", out Color normalcolor);

            if(openVideo)
            {
                videoTitle.color = chosencolor;
                audioTitle.color = normalcolor;
                videoContainerGameObj.SetActive(true);
                audioContainerGameObj.SetActive(false);
            }
            else
            {
                audioTitle.color = chosencolor;
                videoTitle.color = normalcolor;
                videoContainerGameObj.SetActive(false);
                audioContainerGameObj.SetActive(true);
            }
        }

        public void BackButtonClick()
        {
            if(GameManager.Instance.IsStartGame)
                GameManager.Instance.PauseGame();
            else
                GameManager.Instance.OpenStartGame();
        }

        public void SetMasterVolume(float amount)
        {
            
        }
        public void SetMusicVolume(float amount)
        {
            
        }
        public void SetSoundVolume(float amount)
        {
            
        }
        public void SetAmbienceVolume(float amount)
        {
            
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        
        private void ShowResolutionDropDown()
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            
            int currentResolutionIndex = 0;
            for(int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }   
}