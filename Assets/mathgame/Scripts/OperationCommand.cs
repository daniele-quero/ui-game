using UnityEngine.UI;

public abstract class OperationCommand
{
    public abstract string Sign { get; }
    public abstract int Points { get; }
    public abstract string Name { get; }

    public abstract void Set(Text a, Text b, Text sign, Text feedback, out int aNum, out int bNum, InputField result);

    public abstract bool isCorrect(int a, int b, int result);

    public void CommonSet(Text a, Text b, Text sign, Text feedback, int aNum, int bNum, string signStr, InputField result)
    {
        a.text = aNum.ToString();
        b.text = bNum.ToString();
        feedback.text = "";
        result.interactable = true;
        result.text = "";
        sign.text = Sign;
    }
}
