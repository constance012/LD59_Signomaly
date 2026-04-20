using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References"), Space]
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Color hoverColor;
    private Color oldColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        textMesh.color = oldColor;
    }

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
