using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Image _bg;

    private void Awake() => MathGameDeluxe.Victory += SetVictory;

    private void OnDestroy() => MathGameDeluxe.Victory -= SetVictory;

    private void Start()
    {
        _bg.enabled = false;
        GetComponentsInChildren<Text>().ToList().ForEach(t => t.enabled = false);
    }

    private void SetVictory(int score)
    {
        _bg.enabled = true;
        GetComponentsInChildren<Text>().ToList().ForEach(t => t.enabled = true);
        _score.text = score.ToString();
    }

}
