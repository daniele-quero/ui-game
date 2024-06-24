using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClearButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { Debug.Log("Clearing"); Plate.Clear?.Invoke(); });
    }
}
