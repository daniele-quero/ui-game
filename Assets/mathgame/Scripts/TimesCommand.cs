using UnityEngine;
using UnityEngine.UI;

public class TimesCommand : OperationCommand
{
    public override string Sign => "x";
    public override int Points => 1500;
    public override string Name => "Multiplication";

    public override void Set(Text a, Text b, Text sign, Text feedback, out int aNum, out int bNum, InputField result)
    {
        aNum = Random.Range(2, 10);
        bNum = Random.Range(2, 10);

        CommonSet(a, b, sign, feedback, aNum, bNum, Sign, result);
    }

    public override bool isCorrect(int a, int b, int result) => result == a * b;
}
