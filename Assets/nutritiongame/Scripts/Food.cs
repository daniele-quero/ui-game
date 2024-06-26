using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Drag))]
public class Food : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private FoodScriptableObject _food;
    private Image _image;

    public static Action<string> PointerOnFood;

    public FoodScriptableObject FoodSO { get => _food; set => _food = value; }

    public void OnPointerEnter(PointerEventData eventData) => PointerOnFood?.Invoke(_food.name);

    public void OnPointerExit(PointerEventData eventData) => PointerOnFood?.Invoke(string.Empty);

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _food.icon;
    }
}
