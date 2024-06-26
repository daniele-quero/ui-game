using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _dragSound;
    [SerializeField] AudioSource _OKSound;
    [SerializeField] AudioSource _KOSound;

    private void OnEnable()
    {
        Drag.Dragging += _dragSound.Play;
        MessageBoard.KO += _KOSound.Play;
        MessageBoard.OK += _OKSound.Play;
    }
}
