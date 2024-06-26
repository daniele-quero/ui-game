using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour
{
    private const string _prefix = "You have: ";
    private const string _notePrefix = "- ";
    private const string _default = "Choose 3 foods";
    [SerializeField] private Text _foodsText;
    [SerializeField] private Text _foodNameTooltipText;
    [SerializeField] private GameObject _notesContent;
    [SerializeField] private Font _font;
    [SerializeField] private Image _fireworks;

    public static Action OK;
    public static Action KO;

    private void OnEnable()
    {
        Plate.Full += Show;
        Plate.Unfill += ClearUp;
        Plate.Clear += ClearUp;

        Food.PointerOnFood += (n) => _foodNameTooltipText.text = n;

        ClearUp();
    }

    private void ClearUp()
    {
        _fireworks.enabled = false;
        _foodsText.text = _default;
        _notesContent.GetComponentsInChildren<Text>().ToList().ForEach(t => Destroy(t.gameObject));
    }

    private void Show(FoodScriptableObject[] foods)
    {
        int p, c, v;

        if (CorrectChoice(foods, out p, out v, out c))
        {
            CorrectChoiceDisplay(foods);
            OK?.Invoke();
            _fireworks.enabled = true;
        }
        else
        {
            KO?.Invoke();
            _foodsText.text = _prefix + p + " proteins, " + c + " carbs and " + v + " veggies.";
            AddNote("Every meal should be well balanced.");
            AddNote("You need 1 per each food type.");
            foods.ToList().ForEach(f => AddNote(f.name + " is a " + f.foodType.ToString()));
        }
    }

    private bool CorrectChoice(FoodScriptableObject[] foods, out int p, out int v, out int c)
    {
        p = 0;
        c = 0;
        v = 0;
        foreach (FoodScriptableObject food in foods)
        {
            switch (food.foodType)
            {
                case FoodScriptableObject.FoodType.PROTEIN: p++; break;
                case FoodScriptableObject.FoodType.CARB: c++; break;
                case FoodScriptableObject.FoodType.VEGGIE: v++; break;
            }
        }

        return p == 1 && c == 1 && v == 1;
    }

    private void CorrectChoiceDisplay(FoodScriptableObject[] foods)
    {
        _foodsText.text = _prefix + string.Join(", ", foods.Select(i => i.name));
        foreach (string fn in foods.SelectMany(f => f.notes))
            AddNote(fn);

        AddNote("A trick for the quantities: 2 parts veggies, 1 part proteins, 1 part carbs.");
    }

    private void AddNote(string note)
    {
        GameObject n = Instantiate(new GameObject(), _notesContent.transform);
        Text t = n.AddComponent<Text>();
        t.color = Color.black;
        t.text = _notePrefix + note;
        t.font = _font;
        t.fontSize = 22;
        ContentSizeFitter fit = n.AddComponent<ContentSizeFitter>();
        fit.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        fit.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        n.GetComponent<RectTransform>().sizeDelta = _notesContent.GetComponent<RectTransform>().sizeDelta;
    }
}
