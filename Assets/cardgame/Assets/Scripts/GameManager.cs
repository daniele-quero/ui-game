using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string POINT_KEY = "Point";
    private const string VICTORY_KEY = "Victory";
    private const string MALUS_KEY = "Malus";
    private const string BONUS_KEY = "Bonus";
    private const string GAMEOVER_KEY = "Gameover";
    private int _cardsLeft;
    [SerializeField] private int _livesLeft = 3;
    [SerializeField] private int _pointScore = 500;
    [SerializeField] private int _malusScore = -150;
    [SerializeField] private int _bonusScore = 150;

    [SerializeField] private LevelScriptableObject[] _levels;
    [SerializeField] private GameObject _gameTemplate;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _endLevel;
    [SerializeField] private GameObject _audioManager;

    private Dictionary<string, AudioSource> _audioSources = new Dictionary<string, AudioSource>();

    private int _level;
    private int _score = 0;
    private float _endLeveltime = 3f;

    public static Action UncoverCoverCards;
    public static Action MalusEffect;
    public static Action<int> LivesUpdated;
    public static Action<int> CardsUpdated;

    private void Awake()
    {
        Card.CardAction += OnUncover;
        CardManager.AssertCardsLeft += InitializePlayerParameters;
    }

    private void OnDestroy()
    {
        Card.CardAction -= OnUncover;
        CardManager.AssertCardsLeft -= InitializePlayerParameters;
    }

    private void OnEnable()
    {
        foreach (var l in _levels)
        {
            var g = Instantiate(_gameTemplate, _canvas.transform);
            g.SetActive(false);
            g.GetComponent<CardManager>().LevelScriptableObject = l;
        }

        _level = 0;
        LoadLevel();
    }

    private void Start() => PrepareSFX();

    private void InitializePlayerParameters(int cardsLeft)
    {
        _cardsLeft = cardsLeft;
        CardsUpdated?.Invoke(_cardsLeft);
        LivesUpdated?.Invoke(_livesLeft);
    }

    private void PrepareSFX()
    {
        var sources = _audioManager.GetComponents<AudioSource>();
        _audioSources.Add(POINT_KEY, sources[0]);
        _audioSources.Add(VICTORY_KEY, sources[1]);
        _audioSources.Add(MALUS_KEY, sources[2]);
        _audioSources.Add(BONUS_KEY, sources[3]);
        _audioSources.Add(GAMEOVER_KEY, sources[4]);
    }

    private void LoadLevel() => _canvas.transform.GetChild(_level).gameObject.SetActive(true);

    private void UnloadLevel()
    {
        if (_level > 0)
        {
            _canvas.transform.GetChild(_level - 1).gameObject.SetActive(false);
        }
    }

    private void OnUncover(CardScriptableObject.CardType cardType)
    {
        switch (cardType)
        {
            case CardScriptableObject.CardType.POINT:
                {
                    _audioSources[POINT_KEY].Play();
                    _score += _pointScore;
                    if (--_cardsLeft <= 0)
                    {
                        _audioSources[VICTORY_KEY].Play();
                        StartCoroutine(VictoryCoroutine(EndLevel("Victory!")));
                        break;
                    }
                    CardsUpdated?.Invoke(_cardsLeft);
                    break;
                }
            case CardScriptableObject.CardType.MALUS:
                {
                    _score = Mathf.Max(0, _score + _malusScore);
                    _audioSources[MALUS_KEY].Play();
                    MalusEffect?.Invoke();
                    if (--_livesLeft < 0)
                    {
                        _audioSources[GAMEOVER_KEY].Play();
                        StartCoroutine(GameOverCoroutine(EndLevel(GAMEOVER_KEY)));
                    }
                    LivesUpdated?.Invoke(_livesLeft);
                    break;
                }
            case CardScriptableObject.CardType.BONUS:
                {
                    _score += _bonusScore;
                    _audioSources[BONUS_KEY].Play();
                    UncoverCoverCards?.Invoke();
                    break;
                }
        }
    }

    private GameObject EndLevel(String s)
    {
        var o = Instantiate(_endLevel, _canvas.transform);
        o.GetComponentInChildren<Text>().text = s;
        Destroy(o, _endLeveltime);
        return o;
    }

    private IEnumerator VictoryCoroutine(GameObject o)
    {
        yield return new WaitUntil(() => o.IsDestroyed());
        _level++;
        if (_canvas.transform.childCount > _level)
        {
            LoadLevel();
            yield return new WaitUntil(() => _canvas.transform.GetChild(_level).gameObject.activeInHierarchy);//todo
            UnloadLevel();
        }
        else yield return null;
    }

    private IEnumerator GameOverCoroutine(GameObject o)
    {
        yield return new WaitUntil(() => o.IsDestroyed());
        _livesLeft = 1;
        LivesUpdated?.Invoke(_livesLeft);
        _canvas.transform.GetChild(_level).GetComponent<CardManager>().Restart();
    }
}
