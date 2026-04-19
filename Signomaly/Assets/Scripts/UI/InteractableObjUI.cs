using TMPro;
using UnityEngine;

namespace AforgeStudios.Signomaly
{
    public class InteractableObjUI : MonoBehaviour
    {
        [Header("References"), Space]
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private TextMeshProUGUI interactText;

        public void Show(string keyStr, string interactStr)
        {
            keyText.text = keyStr;
            interactText.text = interactStr;
        }
    }
}