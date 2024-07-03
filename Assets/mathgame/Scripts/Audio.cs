using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource _ok;
    [SerializeField] AudioSource _ko;
    [SerializeField] AudioSource _victory;

    private void Awake()
    {
        MathGameDeluxe.CorrectAnswer += OK;
        MathGameDeluxe.WrongAnswer += KO;
        MathGameDeluxe.Victory += Victory;
    }

    private void OnDestroy()
    {
        MathGameDeluxe.CorrectAnswer -= OK;
        MathGameDeluxe.WrongAnswer -= KO;
        MathGameDeluxe.Victory -= Victory;
    }

    private void OK() => _ok.Play();
    private void KO() => _ko.Play();
    private void Victory(int score) => _victory.Play();

}
