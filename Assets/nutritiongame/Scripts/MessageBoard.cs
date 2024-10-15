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

    [SerializeField] private GameObject _tooltipTemplate;

    public static Action OK;
    public static Action KO;

    private void Awake()
    {
        Plate.Full += Show;
        Plate.Unfill += ClearUp;
        Plate.Clear += ClearUp;
    }

    private void OnDestroy()
    {
        Plate.Full -= Show;
        Plate.Unfill -= ClearUp;
        Plate.Clear -= ClearUp;
        Food.PointerOnFood -= SetFoodTooltipText;
    }

    private void OnEnable()
    {
        ClearUp();
        InitTooltip();
    }

    private void ClearUp()
    {
        _fireworks.enabled = false;
        _foodsText.text = _default;
        _notesContent.GetComponentsInChildren<Text>().ToList().ForEach(t => Destroy(t.gameObject));
    }

    private void InitTooltip()
    {
        if (_foodNameTooltipText?.gameObject != null)
            Destroy(_foodNameTooltipText.gameObject);

        var t = Instantiate(_tooltipTemplate);
        t.transform.SetParent(transform);
        t.transform.localPosition = new Vector3(8.5f, -293, 0);
        _foodNameTooltipText = t.GetComponent<Text>();

        Food.PointerOnFood += SetFoodTooltipText;
    }

    private void SetFoodTooltipText(string n)
    {
        _foodNameTooltipText.text = n;
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
            _foodsText.text = _prefix + p + " proteins, " + c + " carbs, " + v + " veggies.";
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
        GameObject n = new GameObject();
        n.transform.SetParent(_notesContent.transform);
        n.transform.localScale = Vector3.one;

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
