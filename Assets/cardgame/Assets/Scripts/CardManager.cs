using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    private GridLayoutGroup _cardContainer;
    [SerializeField] private GameObject _cardObjTemplate;
    [SerializeField] private GameObject _background;

    [SerializeField] private Slider _slider;

    [SerializeField] Animator _anim;

    public static Action UncoverCoverCards;
    public static Action<Vector2> ResizingCells;
    public static Action<int> AssertCardsLeft;
    public static Action<string> AssertLevel;

    public LevelScriptableObject LevelScriptableObject { get; set; }

    private void Awake() => GameManager.MalusEffect += Explode;

    private void OnDestroy() => GameManager.MalusEffect -= Explode;

    void Start()
    {
        InitCardsLeft();
        AssertLevel?.Invoke(LevelScriptableObject.label);
        _cardContainer = GetComponentInChildren<GridLayoutGroup>();
        InitializeCardGame();
    }

    private void InitCardsLeft()
    {
        var cardsLeft = LevelScriptableObject.cardOptions
                    .Select(c => c)
                    .Where(c => c.cardTemplate.cardType == CardScriptableObject.CardType.POINT)
                    .First()
                    .number;

        AssertCardsLeft?.Invoke(cardsLeft);
    }

    public void Restart()
    {
        foreach (var c in transform.GetComponentsInChildren<Card>())
        {
            c.Cover();
        }
        InitCardsLeft();
    }

    private void InitializeCardGame()
    {
        var cards = new List<GameObject>();
        foreach (var opt in LevelScriptableObject.cardOptions)
        {
            cards.AddRange(SpawnCards(opt.cardTemplate, opt.number));
        }

        ShuffleCards(cards);

        int cols = Mathf.CeilToInt(Mathf.Sqrt(_cardContainer.transform.childCount));
        int rows = Mathf.CeilToInt(_cardContainer.transform.childCount / (float)cols);

        Vector2 space = new Vector2(_cardContainer.spacing.x, _cardContainer.spacing.y);
        Vector2 cell = new Vector2(_cardContainer.cellSize.x, _cardContainer.cellSize.y);

        Vector2 maxSize = _background.GetComponent<RectTransform>().sizeDelta - 5 * space;
        Vector2 size = Size(cell, space, maxSize, cols, rows);

        _cardContainer.GetComponent<RectTransform>().sizeDelta = size;

        StartCoroutine(ResizeCells(maxSize));
    }

    private void ShuffleCards(List<GameObject> cards)
    {
        var indices = cards.Select(c => c.transform.GetSiblingIndex()).ToList();
        DurstenfeldShuffler<int>.shuffle(indices);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.transform.SetSiblingIndex(indices[i]);
        }
    }

    private IEnumerator ResizeCells(Vector2 maxSize)
    {
        yield return new WaitForEndOfFrame();
        while (_cardContainer.preferredWidth > maxSize.x || _cardContainer.preferredHeight > maxSize.y)
        {
            _slider.gameObject.SetActive(true);
            _cardContainer.cellSize -= Vector2.one * 2f;
            ResizingCells?.Invoke(_cardContainer.cellSize);
            float completion = (_cardContainer.preferredWidth / maxSize.x) + (_cardContainer.preferredHeight / maxSize.y) / 2f;
            _slider.value = completion;
            yield return new WaitForEndOfFrame();
        }
        _cardContainer.transform.localScale = Vector3.one;
        _slider.gameObject.SetActive(false);
        UncoverCoverCards?.Invoke();
        yield return null;
    }

    private Vector2 Size(Vector2 cell, Vector2 space, Vector2 maxSize, int cols, int rows)
    {
        var size = new Vector2(cell.x * cols + space.x * (cols - 1), cell.y * rows + space.y * (rows - 1));
        if (size.x > maxSize.x || size.y > maxSize.y)
            size = maxSize;

        return size;
    }

    private List<GameObject> SpawnCards(CardScriptableObject template, int n)
    {
        var cards = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            var card = Instantiate(_cardObjTemplate);
            card.transform.SetParent(_cardContainer.transform);
            card.GetComponent<Card>().Init(template, LevelScriptableObject.time);
            cards.Add(card);
        }
        return cards;
    }

    public void Explode() => _anim.SetTrigger("explode");
}
