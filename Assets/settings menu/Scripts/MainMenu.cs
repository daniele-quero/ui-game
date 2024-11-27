using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private GameObject _title;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        GameChoiceButton.OpenGameChoice += Deactivate;
        ProfileButton.OpenProfile += Deactivate;
        MainMenuButton.GoToMain += Activate;
        MainMenuButton.GoToMain += () => Plate.Clear?.Invoke();
    }

    private void Activate()
    {
        _canvas.enabled = true;
        FixTitle();
    }

    private void Deactivate(bool otherActive)
    {
        if (otherActive)
        {
            _canvas.enabled = false;
        }
    }

    private IEnumerator FixTitle()
    {
        _title.SetActive(false);
        yield return null;
        _title.SetActive(true);
    }
}
