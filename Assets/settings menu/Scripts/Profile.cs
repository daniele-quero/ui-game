using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : BaseScene
{
    protected override void OnEnable()
    {
        base.OnEnable();
        ProfileButton.OpenProfile += Activate;
    }

    protected override void Activate(bool active) => _canvas.enabled = active;
}
