using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected Canvas _canvas;
    virtual protected void OnEnable()
    {
        _canvas = GetComponent<Canvas>();
    }

    protected abstract void Activate(bool active);
}
