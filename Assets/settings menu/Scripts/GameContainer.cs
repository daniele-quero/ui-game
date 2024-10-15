using UnityEngine;

public class GameContainer : MonoBehaviour
{
    [SerializeField] private Games _gameType;
    [SerializeField] private GameObject _gamePrefab;
    [SerializeField] private GameObject _game;

    void Awake()
    {
        GameLoader.GameLoaded += Load;
        MainMenuButton.GoToMain += PrepareGame;
    }

    private void Start()
    {
        PrepareGame();
    }

    void Load(Games game)
    {
        if (game == _gameType)
            _game.SetActive(true);
        else
            _game.SetActive(false);
    }

    public void PrepareGame()
    {

        //if (_gameType == Games.NUTRITION)
        //{
        //    _game.SetActive(false);
        //    return;
        //}

        if (transform.childCount == 1)
            Destroy(transform.GetChild(0).gameObject);

        _game = Instantiate(_gamePrefab, transform);
        _game.SetActive(false);
    }
}
