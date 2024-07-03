using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [Header("Labels")]
    [SerializeField] Text _current;
    [SerializeField] Text _next;
    [SerializeField] Text _left;
    [SerializeField] Text _score;

    private const string CurrentPrefix = "Current: ";
    private const string NextPrefix = "Next: ";
    private const string LeftPrefix = "Left: ";
    private const string ScorePrefix = "Score: ";

    private void Awake()
    {
        MathGameDeluxe.QuestionLoaded += UpdateDisplay;
        MathGameDeluxe.ScoreUpdated += UpdateScore;
    }

    private void OnDestroy()
    {
        MathGameDeluxe.QuestionLoaded -= UpdateDisplay;
        MathGameDeluxe.ScoreUpdated -= UpdateScore;
    }

    private void UpdateScore(int s) => _score.text = ScorePrefix + s;

    private void UpdateDisplay(List<OperationCommand> operations)
    {
        _left.text = LeftPrefix + operations.Count;
        _current.text = CurrentPrefix + operations[0].Name;
        _next.text = NextPrefix + (operations.Count > 1 ? operations[1].Name : string.Empty);
    }
}
