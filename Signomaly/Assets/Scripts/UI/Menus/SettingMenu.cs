using TMPro;
using UnityEngine;

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

        private void Start()
        {
            GameManager.Instance.OnPauseGame += GameManager_SettingMenu_OnPauseGame;
            GameManager.Instance.OnSettingGame += GameManager_SettingMenu_OnSettingGame;
        }

        private void GameManager_SettingMenu_OnPauseGame()
        {
            containerGameObj.SetActive(false);
        }

        private void GameManager_SettingMenu_OnSettingGame()
        {
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
            GameManager.Instance.PauseGame();
        }
    }   
}