using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardScriptableObject", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public enum CardType
    {
        MALUS, BONUS, POINT
    }

    public string cardName;
    public Sprite icon;
    public CardType cardType;
    //SFX
}
