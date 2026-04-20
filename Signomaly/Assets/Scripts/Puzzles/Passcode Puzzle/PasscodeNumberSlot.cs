using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class PasscodeNumberSlot : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] private TextMeshProUGUI _numberText;

		public string CurrentNumber { get; set; }

		public void Enter(string number)
		{
			CurrentNumber = number;
			_numberText.text = CurrentNumber;
		}

		public void Clear()
		{
			CurrentNumber = string.Empty;
			_numberText.text = CurrentNumber;
		}
	}
}