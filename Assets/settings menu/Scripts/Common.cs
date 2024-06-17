using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : BaseScene
{
    protected override void Activate(bool active)
    {
        if (active)
            _canvas.enabled = true;
    }


}
