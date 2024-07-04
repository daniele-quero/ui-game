using UnityEngine;
using UnityEngine.UI;

public class HalfCommand : OperationCommand
{
    public override string Sign => "h";
    public override int Points => 1500;
    public override string Name => "Halving";

    public override void Set(Text a, Text b, Text sign, Text feedback, out int aNum, out int bNum, InputField result)
    {
        int n = Random.Range(0, 51);
        aNum = 2 * n;
        bNum = 2;

        CommonSet(a, b, sign, feedback, aNum, bNum, Sign, result);
        a.text = "the half of";
        b.text = aNum.ToString();
        sign.text = string.Empty;
    }

    public override bool isCorrect(int a, int b, int result) => result == a / b;
}
