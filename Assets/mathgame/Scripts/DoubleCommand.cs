using UnityEngine;
using UnityEngine.UI;

public class DoubleCommand : OperationCommand
{
    public override string Sign => "d";
    public override int Points => 1000;
    public override string Name => "Doubling";

    public override void Set(Text a, Text b, Text sign, Text feedback, out int aNum, out int bNum, InputField result)
    {
        aNum = Random.Range(0, 51);
        bNum = 2;

        CommonSet(a, b, sign, feedback, aNum, bNum, Sign, result);
        a.text = "the double of";
        b.text = aNum.ToString();
        sign.text = string.Empty;
    }

    public override bool isCorrect(int a, int b, int result) => result == a * b;
}
