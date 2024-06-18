public class GameChoice : BaseScene
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GameChoiceButton.OpenGameChoice += Activate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
}
