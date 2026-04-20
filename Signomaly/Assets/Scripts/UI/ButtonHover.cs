using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References"), Space]
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Color hoverColor;
    private Color oldColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldColor = textMesh.color;
        textMesh.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = oldColor;
    }
}
