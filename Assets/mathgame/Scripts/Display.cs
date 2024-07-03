using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [Header("Labels")]
    [SerializeField] Text _current;
    [SerializeField] Text _next;
    [SerializeField] Text _left;

    private const string CurrentPrefix = "Current: ";
    private const string NextPrefix = "Next: ";
    private const string LeftPrefix = "Left: ";

    private void Awake() => MathGameDeluxe.QuestionLoaded += UpdateDisplay;

    private void OnDestroy() => MathGameDeluxe.QuestionLoaded -= UpdateDisplay;

    private void UpdateDisplay(List<OperationCommand> operations)
    {
        _left.text = LeftPrefix + operations.Count;
        _current.text = CurrentPrefix + operations[0].Name;
        _next.text = NextPrefix + (operations.Count > 1 ? operations[1].Name : string.Empty);
    }
}
