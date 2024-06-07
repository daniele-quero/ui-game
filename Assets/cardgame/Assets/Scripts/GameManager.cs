using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private int _cardsLeft;
    [SerializeField] private int _livesLeft = 3;

    [SerializeField] LevelScriptableObject[] _levels;
    [SerializeField] GameObject _gameTemplate;
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _endLevel;

    private int _thisLevel;
    private int _level;

    private float _endLeveltime = 3f;

    public static Action UncoverCoverCards;
    public static Action MalusEffect;
    public static Action<int> LivesUpdated;
    public static Action<int> CardsUpdated;

    private void OnEnable()
    {
        Card.CardAction += OnUncover;
        CardManager.AssertCardsLeft += InitializePlayerParameters;
        foreach (var l in _levels)
        {
            var g = Instantiate(_gameTemplate, _canvas.transform);
            g.SetActive(false);
            g.GetComponent<CardManager>().LevelScriptableObject = l;
        }

        _level = 0;
        LoadLevel();
    }

    private void InitializePlayerParameters(int cardsLeft)
    {
        _cardsLeft = cardsLeft;
        CardsUpdated?.Invoke(_cardsLeft);
        LivesUpdated?.Invoke(_livesLeft);
    }

    private void Start()
    {

        
    }

    private void LoadLevel()
    {
        if (_canvas.transform.childCount > _level)
        {
            _canvas.transform.GetChild(_level).gameObject.SetActive(true);
        }
    }

    private void UnloadLevel()
    {
        if (_level > 0)
        {
            _canvas.transform.GetChild(_level - 1).gameObject.SetActive(false);
        }
    }

    private void OnUncover(CardScriptableObject.CardType cardType)
    {
        //play sfx
        switch (cardType)
        {
            case CardScriptableObject.CardType.POINT:
                {
                    if (--_cardsLeft <= 0)
                    {
                        StartCoroutine(VictoryCoroutine(EndLevel("Victory!")));
                        break;
                    }
                    CardsUpdated?.Invoke(_cardsLeft);
                    break;
                }
            case CardScriptableObject.CardType.MALUS:
                {
                    //play sfx
                    MalusEffect?.Invoke();
                    if (--_livesLeft < 0)
                    {
                        //UnloadLevel();
                        StartCoroutine(GameOverCoroutine(EndLevel("Gameover")));
                        //break;
                    }
                    LivesUpdated?.Invoke(_livesLeft);
                    break;
                }
            case CardScriptableObject.CardType.BONUS:
                {
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
        LoadLevel();
        yield return new WaitUntil(() => _canvas.transform.GetChild(_level).gameObject.activeInHierarchy);
        UnloadLevel();
    }

    private IEnumerator GameOverCoroutine(GameObject o)
    {
        yield return new WaitUntil(() => o.IsDestroyed());
        _livesLeft = 1;
        LivesUpdated?.Invoke(_livesLeft);
        _canvas.transform.GetChild(_level).GetComponent<CardManager>().Restart();
    }
}
