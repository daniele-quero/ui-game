using UnityEngine;

public class GameContainer : MonoBehaviour
{
    [SerializeField] private Games _gameType;
    [SerializeField] private GameObject _gamePrefab;
    [SerializeField] private GameObject _game;

    void Start()
    {
        GameLoader.GameLoaded += Load;
        MainMenuButton.GoToMain += PrepareGame;
        PrepareGame();
    }

    void Load(Games game)
    {
        Debug.Log(game.ToString());
        if (game == _gameType)
        {
            _game.SetActive(true);
        }
    }

    public void PrepareGame()
    {
        if (transform.childCount == 1)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        _game = Instantiate(_gamePrefab, transform);
        _game.SetActive(false);
    }
}
