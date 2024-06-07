using UnityEngine;

public class ExplosionFX : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    public Vector3 Position { set => Position_(value); }

    public void Position_(Vector3 pos)
    {
        rectTransform.position = pos;
    }
}
