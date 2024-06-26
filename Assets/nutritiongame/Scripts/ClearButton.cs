using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClearButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Clear);
    }

    private void Clear()
    {
        BaseToggle.ButtonPressed?.Invoke();
        Plate.Clear?.Invoke();
    }
}
