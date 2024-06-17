using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameChoice : BaseScene
{

    protected override void OnEnable()
    {
        base.OnEnable();
        GameChoiceButton.OpenGameChoice += Activate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
}
