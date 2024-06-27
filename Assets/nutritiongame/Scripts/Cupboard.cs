using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cupboard : MonoBehaviour
{
    void Start()
    {
        List<FoodScriptableObject> fsos = Resources.LoadAll<FoodScriptableObject>("").ToList();
        DurstenfeldShuffler<FoodScriptableObject>.shuffle(fsos);
        fsos.ForEach(f => AddFood(f));
    }

    private void AddFood(FoodScriptableObject fo)
    {
        GameObject o = new GameObject();
        o.transform.SetParent(transform);
        Food food = o.AddComponent<Food>();
        food.FoodSO = fo;
        o.name = food.FoodSO.name;
    }
}
