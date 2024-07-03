using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MathGameDeluxe : MonoBehaviour
{
    [Header("Game Design Options")]
    [SerializeField] private float _waitTime = 4f;
    [SerializeField] private int _questionNum = 5;

    [Header("UI References")]
    [SerializeField] private Text _a;
    [SerializeField] private Text _b;
    [SerializeField] private Text _feedback;
    [SerializeField] private Text _sign;

    [SerializeField] private InputField _result;

    private Color _errorColor = new Color(0.65f, 0.14f, 0.14f);//Dark red
    private Color _okColor = Color.white;

    private WaitForSeconds _wait = new WaitForSeconds(4f);

    private int _aNum;
    private int _bNum;

    private int _score = 0;

    private List<OperationCommand> _questions = new List<OperationCommand>();
    private OperationCommand _operation;

    public static Action CorrectAnswer;
    public static Action WrongAnswer;
    public static Action<int> Victory;
    public static Action<List<OperationCommand>> QuestionLoaded;

    private void Start()
    {
        _result.onValueChanged.AddListener((v) => Tooltip.ActiveTooltip?.Invoke());
        _result.Select();
        _wait = new WaitForSeconds(_waitTime);
        SetQuestions(_questionNum);
        DurstenfeldShuffler<OperationCommand>.shuffle(_questions);
        LoadNextQuestion();
    }

    private void LoadNextQuestion()
    {
        _operation = _questions[0];
        _operation.Set(_a, _b, _sign, _feedback, out _aNum, out _bNum, _result);
        _result.ActivateInputField();
        QuestionLoaded?.Invoke(_questions);
    }

    private void SetQuestions(int num)
    {
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

        asm.GetTypes().ToList()
            .Select(t => t)
            .Where(t => t.IsSubclassOf(typeof(OperationCommand)))
            .ToList()
            .ForEach(t => AddQuestionSubSet(num, t));
    }

    private void AddQuestionSubSet(int num, Type t)
    {
        for (int i = 0; i < num; i++) { _questions.Add((OperationCommand)Activator.CreateInstance(t)); }
    }

    private void SetWrongFeedback()
    {
        _feedback.color = _errorColor;
        _feedback.text = "Oh, no! Try again!";
        _result.ActivateInputField();
        _score = Mathf.Max(0, _score - 2);
        WrongAnswer?.Invoke();
    }

    private IEnumerator Correct()
    {
        _feedback.color = _okColor;
        _feedback.text = "Great! Guess the next one now!";
        _result.interactable = false;
        _score += _operation.Points;

        CorrectAnswer?.Invoke();

        yield return _wait;
        _questions.RemoveAt(0);
        if (_questions.Count > 0)
            LoadNextQuestion();
        else
            Victory?.Invoke(_score);
    }

    public void ReadAnswer(string r)
    {
        int answer = int.Parse(r);

        if (_operation.isCorrect(_aNum, _bNum, answer))
            StartCoroutine(Correct());
        else
            SetWrongFeedback();
    }
}
