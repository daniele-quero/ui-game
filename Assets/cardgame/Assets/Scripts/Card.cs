using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Text _name;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _cover;
    [SerializeField] private Image _background;
    [SerializeField] private CardScriptableObject.CardType _cardType;

    private WaitForSeconds _coverTime = new WaitForSeconds(2f);

    private bool _uncovered = false;
    private float _originalSize = 100;

    public static Action<CardScriptableObject.CardType> CardAction;

    private void OnEnable()
    {
        CardManager.ResizingCells += ResizeAll;
        GameManager.UncoverCoverCards += OnUncoverCover;
        CardManager.UncoverCoverCards += OnUncoverCover;
        GetComponentInChildren<Button>().onClick.AddListener(Uncover);
    }

    private void ResizeAll(Vector2 size)
    {
        _icon.rectTransform.sizeDelta = size;
        _cover.rectTransform.sizeDelta = size;
        _background.rectTransform.sizeDelta = size;
        _name.fontSize = (int)(_name.fontSize * size.x / _originalSize);
    }

    public void Init(CardScriptableObject obj)
    {
        _name.text = obj.cardName;
        _icon.sprite = obj.icon;
        _cardType = obj.cardType;
        _originalSize = _background.rectTransform.sizeDelta.x;
    }

    private void OnUncoverCover()
    {
        StartCoroutine(UncoverCover());
    }

    public void Init(CardScriptableObject obj, float time)
    {
        Init(obj);
        _coverTime = new WaitForSeconds(time);
    }

    private IEnumerator UncoverCover()
    {
        if (!_uncovered)
        {
            transform.localScale = Vector2.one;
            _cover.gameObject.SetActive(false);
            yield return _coverTime;
            _cover.gameObject.SetActive(true);
        }
    }

    public void Uncover()
    {
        _cover.gameObject.SetActive(false);
        _uncovered = true;
        CardAction?.Invoke(_cardType);
    }

    public void Cover()
    {
        _cover.gameObject.SetActive(true);
        _uncovered = false;
    }

}
