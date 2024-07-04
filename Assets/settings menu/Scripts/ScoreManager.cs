using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private List<Score> _scores = new List<Score>();
    public static Action<Games, int> ScoreUpdated;

    public List<Score> Scores { get => _scores; }

    private void Awake() => ScoreUpdated += UpdateScore;
    private void OnDestroy() => ScoreUpdated -= UpdateScore;

    void Start() => ((Games[])Enum.GetValues(typeof(Games))).ToList()
            .ForEach(g => _scores.Add(new Score(g)));

    private void UpdateScore(Games game, int score) => _scores.Select(s => s).Where(s => s.Game == game).First().CurrentScore = score;
}
