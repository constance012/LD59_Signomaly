using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class PasscodePanelUI : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] private PasscodePuzzleInteraction _puzzleInteraction;

		[Header("UI References"), Space]
		[SerializeField] private PasscodeNumberSlot[] _numberSlots;
		[SerializeField] private TextMeshProUGUI _errorText;

		private const string WRONG_OBJECT_SUBMISSION_MESSAGE = "Incorrect passcode!";
		
		private string _enteredPasscode;

		public void Show()
		{
			GlobalService.Instance.ToggleLockCursor(false);
			GlobalService.Instance.TogglePlayerInput(false);

			gameObject.SetActive(true);

			ResetUI();
		}

		public void Hide()
		{
			GlobalService.Instance.ToggleLockCursor(true);
			GlobalService.Instance.TogglePlayerInput(true);

			gameObject.SetActive(false);

			ResetUI();
		}
		
		private void ResetUI()
		{
			_enteredPasscode = string.Empty;

			ClearErrorText();

			foreach (var slot in _numberSlots)
			{
				slot.Clear();
			}
		}

		private void ClearErrorText()
		{
			_errorText.text = string.Empty;
		}

		public void NumpadButton_OnClick(string number)
		{
			if (_enteredPasscode.Length >= _numberSlots.Length)
			{
				return;
			}

			number = number.Trim();

			_enteredPasscode += number;
			_numberSlots[_enteredPasscode.Length - 1].Enter(number);

			ClearErrorText();
		}

		public void DeleteButton_OnClick()
		{
			ClearErrorText();

			if (_enteredPasscode.Length <= 0)
			{
				return;
			}

			_numberSlots[_enteredPasscode.Length - 1].Clear();
			_enteredPasscode = _enteredPasscode[..^1];
		}

		public void EnterButton_OnClick()
		{
			if (_puzzleInteraction.SubmitPasscode(_enteredPasscode))
			{
				Hide();
				return;
			}

			HandleWrongPasscode();
		}

		public void BackButton_OnClick()
		{
			Hide();
		}

		private void HandleWrongPasscode()
		{
			_errorText.text = WRONG_OBJECT_SUBMISSION_MESSAGE;
		}
	}
}
