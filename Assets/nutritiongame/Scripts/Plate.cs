using System;
using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour
{
    uint _count = 0;

    public static Action Fill;
    public static Action Unfill;
    public static Action Clear;
    public static Action<FoodScriptableObject[]> Full;

    private void OnEnable()
    {
        Fill += () => { _count++; if (_count == 3) Full?.Invoke(GetFoods()); };
        Unfill += () => _count--;
        Clear += () => _count = 0;
    }

    private FoodScriptableObject[] GetFoods()
    {
        FoodScriptableObject[] foods = GetComponentsInChildren<Food>().Select(d => d.FoodSO).ToArray();
        if (foods?.Length == 3)
            return foods;

        throw new Exception("Error getting foods");
    }

}
