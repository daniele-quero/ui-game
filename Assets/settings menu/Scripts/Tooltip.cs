using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _tooltipText;
    [SerializeField] private string _label;

    public static Action ActiveTooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipText.text = _label;
        ActiveTooltip?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData) => _tooltipText.text = string.Empty;

}
