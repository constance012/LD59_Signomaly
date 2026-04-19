using CSTGames.SharedResources;
using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public sealed class PuzzleInstructionUIHandler : Singleton<PuzzleInstructionUIHandler>
	{
		[Header("UI References"), Space]
		[SerializeField] private TextMeshProUGUI _clueTitleText;
		[SerializeField] private TextMeshProUGUI _clueDescriptionText;

		[Header("Tween References"), Space]
		[SerializeField] private TweenableUIMaster _canvasGroupTweenable;

		private bool _isShowingInstructions;

		private void Start()
		{
			ClearTexts();
		}

		public async void ShowInstructions(string clueTitle, string clueDescription)
		{
			if (_isShowingInstructions)
			{
				return;
			}
			
			_clueTitleText.text = clueTitle;
			_clueDescriptionText.text = clueDescription;

			await _canvasGroupTweenable.SetActive(true);
			_isShowingInstructions = true;
		}

		public async void HideInstructions()
		{
			if (!_isShowingInstructions)
			{
				return;
			}

			await _canvasGroupTweenable.SetActive(false);
			_isShowingInstructions = false;
		}

		public void TweenableUIMaster_OnFadeOut()
		{
			ClearTexts();

			_canvasGroupTweenable.gameObject.SetActive(false);
		}

		public void CloseButton_OnClicked()
		{
			HideInstructions();
		}

		private void ClearTexts()
		{
			_clueTitleText.text = string.Empty;
			_clueDescriptionText.text = string.Empty;
		}
	}
}