using UnityEngine.UI;

public class Common : BaseScene
{
    private ToggleGroup _toggleGroup;
    private void OnEnable()
    {
        base.OnEnable();
        GameChoiceButton.OpenGameChoice += Activate;
        ProfileButton.OpenProfile += Activate;
        MainMenuButton.GoToMain += Deactivate;
        _toggleGroup = GetComponentInChildren<ToggleGroup>();
    }

    protected override void Activate(bool active)
    {
        if (active)
        {
            _canvas.enabled = true;
            _toggleGroup.enabled = true;
        }
    }

    private void Deactivate()
    {
        _toggleGroup.enabled = false; //disabling this other wise some toggles would be checked automatically
        _canvas.enabled = false;
    }
}
