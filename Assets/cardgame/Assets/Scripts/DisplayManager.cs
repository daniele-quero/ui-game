using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{

    [SerializeField] private Text _livesLeftText;
    [SerializeField] private Text _cardsLeftText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _scoreText;

    [SerializeField] private Image _cardsImg;

    [SerializeField] CardScriptableObject _template;

    private void Awake()
    {
        GameManager.LivesUpdated += Lives;
        GameManager.CardsUpdated += Cards;
        CardManager.AssertLevel += Level;
        GameManager.ScoreUpdated += Score;
    }

    private void OnDestroy()
    {
        GameManager.LivesUpdated -= Lives;
        GameManager.CardsUpdated -= Cards;
        CardManager.AssertLevel -= Level;
        GameManager.ScoreUpdated -= Score;
    }

    private void Start() => _cardsImg.sprite = _template.icon;

    public void Lives(int l) => _livesLeftText.text = l.ToString();
    public void Cards(int c) => _cardsLeftText.text = c.ToString();
    public void Level(string s) => _levelText.text = s;
    public void Score(int s) => _scoreText.text = s.ToString();

}
