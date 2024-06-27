using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool _isEmpty = true;

    private void Awake()
    {
        Plate.Clear += EmptySlot;
    }

    private void OnDestroy()
    {
        Plate.Clear -= EmptySlot;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Drag drag;
        if (eventData.pointerDrag.TryGetComponent<Drag>(out drag))
        {
            if (_isEmpty)
            {
                Debug.Log("Dropping");
                eventData.pointerDrag.transform.position = transform.position;
                eventData.pointerDrag.transform.SetParent(transform);
                _isEmpty = false;
                Plate.Fill?.Invoke(drag.GetComponent<Food>().FoodSO);
                BaseToggle.ButtonPressed?.Invoke();
            }
            else
            {
                eventData.pointerDrag.transform.SetParent(drag.StartingParent);
            }
        }

    }

    public void EmptySlot() => _isEmpty = true;
}
