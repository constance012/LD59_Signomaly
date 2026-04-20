using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
	public class CompletedPuzzleCounterUI : MonoBehaviour
	{
		[Header("References"), Space]
		[SerializeField] private Canvas _counterCanvas;
		[SerializeField] private TextMeshProUGUI _counterText;

		[Header("Settings"), Space]
		[SerializeField] private Color _insufficientPuzzlesColor = Color.red;
		[SerializeField] private Color _sufficientPuzzlesColor = Color.green;

		private void Awake()
		{
			_counterCanvas.worldCamera = Camera.main;
		}

		private void Start()
		{
			UpdateCounterText();
		}

		private void OnEnable()
		{
			GameStateManager.OnCompletedPuzzlesChanged += UpdateCounterText;
		}

		private void OnDisable()
		{
			GameStateManager.OnCompletedPuzzlesChanged -= UpdateCounterText;
		}

		private void UpdateCounterText()
		{
			int puzzlesCompleted = GameStateManager.Instance.PuzzlesCompleted;
			int totalPuzzles = GameStateManager.Instance.TotalPuzzlesToSolve;
			bool isSufficient = GameStateManager.Instance.IsWinConditionMet;

			_counterText.text = $"{puzzlesCompleted} / {totalPuzzles}";
			_counterText.color = isSufficient ? _sufficientPuzzlesColor : _insufficientPuzzlesColor;
		}
	}
}