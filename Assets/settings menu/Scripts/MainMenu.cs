using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        GameChoiceButton.OpenGameChoice += Deactivate;
        ProfileButton.OpenProfile += Deactivate;
        MainMenuButton.GoToMain += Activate;
        MainMenuButton.GoToMain += () => Plate.Clear?.Invoke();
    }

    private void Activate() => _canvas.enabled = true;

    private void Deactivate(bool otherActive)
    {
        if (otherActive)
        {
            _canvas.enabled = false;
        }
    }
}
