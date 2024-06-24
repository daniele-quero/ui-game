using UnityEngine;

[CreateAssetMenu(fileName = "Food_", menuName = "ScriptableObjects/FoodScriptabldObject", order = 1)]
public class FoodScriptableObject : ScriptableObject
{
    public enum FoodType
    {
        PROTEIN, CARB, VEGGIE
    }
    public Sprite icon;
    public FoodType foodType;
    public string[] notes;
}
