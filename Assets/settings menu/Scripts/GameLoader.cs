using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameLoader : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] string _gameName;
    [SerializeField] Games _game;
    [SerializeField] Image _loadingBar;
    private Button _loadingButton;
    public static Action GameLoaderPressed;

    public static Action<Games> GameLoaded;

    private void Awake()
    {
        GameLoaderPressed += Disable;
        GameChoiceButton.ButtonPressed += Enable;
    }

    private void Start()
    {
        _loadingButton = GetComponent<Button>();
        StartCoroutine(SetName());
    }

    private void Disable() => _loadingButton.interactable = false;

    private void Enable() => _loadingButton.interactable = true;

    private IEnumerator SetName()
    {
        //for some reason the text component is still null on start when I try to set the text value
        Text t = GetComponentInChildren<Text>();
        yield return new WaitUntil(() => t != null);
        t.text = _gameName;
    }

    public void Load()
    {
        GameLoaderPressed?.Invoke();
        BaseToggle.ButtonPressed?.Invoke();
        GameLoaded?.Invoke(_game);
    }


    public void OnPointerEnter(PointerEventData eventData) => Tooltip.ActiveTooltip?.Invoke();
}
