using UnityEngine;

public class Score
{
    private static string SCORE = "_score";
    private static string MAX_SCORE = "_max_score";
    private Games game;
    private int currentScore;
    private int maxScore;

    public Score(Games game)
    {
        this.game = game;
        this.currentScore = PlayerPrefs.GetInt(ScoreKey, 0);
        this.maxScore = PlayerPrefs.GetInt(MaxScoreKey, 0);
    }

    private string ScoreKey => game.ToString() + SCORE;
    private string MaxScoreKey => game.ToString() + MAX_SCORE;

    public Games Game { get => game; }
    public int CurrentScore
    {
        get => currentScore; set
        {
            currentScore = value;
            PlayerPrefs.SetInt(ScoreKey, value);

            maxScore = Mathf.Max(maxScore, value);
            PlayerPrefs.SetInt(MaxScoreKey, value);
        }
    }
    public int MaxScore { get => maxScore; set { if (value == 0) maxScore = 0; } }
}