using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected Canvas _canvas;
    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    protected abstract void Activate(bool active);
}
