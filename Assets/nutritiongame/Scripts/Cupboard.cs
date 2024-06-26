using System.Linq;
using UnityEngine;

public class Cupboard : MonoBehaviour
{
    public string path;

    void Start()
    {
        System.Collections.Generic.List<FoodScriptableObject> fsos = Resources.LoadAll<FoodScriptableObject>("").ToList();
        DurstenfeldShuffler<FoodScriptableObject>.shuffle(fsos);
        fsos.ForEach(f => AddFood(f));
    }

    private void AddFood(FoodScriptableObject fo)
    {
        Debug.Log(fo.name);
        GameObject o = Instantiate(new GameObject(), transform);
        Food food = o.AddComponent<Food>();
        food.FoodSO = fo;
    }
}
