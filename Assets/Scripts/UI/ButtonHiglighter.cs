using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHiglighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Material defaultMaterial;
    public Material highlightMaterial;

    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontMaterial = highlightMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontMaterial = defaultMaterial;
    }
}
