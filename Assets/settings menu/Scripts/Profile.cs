using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BaseScene
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _bestScore;
    [SerializeField] private Dropdown _games;
    [SerializeField] private Button _clear;

    private ScoreManager _scoreManager;

    protected override void Awake()
    {
        base.Awake();
        ProfileButton.OpenProfile += Activate;
    }

    private void Start()
    {
        _scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        List<Dropdown.OptionData> options = _scoreManager.Scores.Select(x => new Dropdown.OptionData(x.Game.ToString())).ToList();
        _games.options = options;
        _games.onValueChanged.AddListener(DisplayScore);
        DisplayScore(_games.value);
    }

    protected override void Activate(bool active) => _canvas.enabled = active;

    private void DisplayScore(int sel)
    {
        _clear.interactable = true;

        Score score = GetCurrentScore(sel);

        if (score.Game == Games.NUTRITION)
        {
            _bestScore.text =
                _score.text = "No score for this game";
            _clear.interactable = false;
            return;
        }

        _score.text = score.CurrentScore.ToString();
        _bestScore.text = score.MaxScore.ToString();
    }

    private Score GetCurrentScore(int sel)
    {
        var selected = _games.options[sel].text;

        var score = _scoreManager.Scores.Select(s => s).Where(s => s.Game.ToString().Equals(selected)).First();
        return score;
    }

    public void ClearCurrentScore()
    {
        var s = GetCurrentScore(_games.value);
        s.CurrentScore = 0;
        s.MaxScore = 0;
        DisplayScore(_games.value);
    }
}
