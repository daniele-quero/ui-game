using System;
using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private uint _count = 0;

    public static Action<FoodScriptableObject> Fill;
    public static Action Unfill;
    public static Action Clear;
    public static Action<FoodScriptableObject[]> Full;

    private void Awake()
    {
        Fill += FillWithFood;
        Unfill += UnfillFood;
        Clear += ClearFood;
    }

    private void OnDestroy()
    {
        Fill -= FillWithFood;
        Unfill -= UnfillFood;
        Clear -= ClearFood;
    }

    private void UnfillFood() => _count--;

    private void ClearFood() => _count = 0;

    private void FillWithFood(FoodScriptableObject fso)
    {

        Debug.Log("Filling");
        _count++;
        if (_count == 3)
            Full?.Invoke(GetFoods());

    }

    private FoodScriptableObject[] GetFoods()
    {
        var foods = GetComponentsInChildren<Food>().Select(d => d.FoodSO).ToArray();
        if (foods?.Length == 3)
            return foods;

        throw new Exception("Error getting foods");
    }

}
