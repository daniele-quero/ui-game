using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public CardOptions[] cardOptions;
    public string label;
    public float time;
}
