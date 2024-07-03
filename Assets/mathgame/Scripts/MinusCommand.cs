using UnityEngine;
using UnityEngine.UI;

public class MinusCommand : OperationCommand
{
    public override string Sign => "-";
    public override int Points => 10;
    public override string Name => "Subtraction";

    public override void Set(Text a, Text b, Text sign, Text feedback, out int aNum, out int bNum, InputField result)
    {
        aNum = Random.Range(1, 30);
        bNum = Random.Range(0, aNum);

        CommonSet(a, b, sign, feedback, aNum, bNum, Sign, result);
    }

    public override bool isCorrect(int a, int b, int result) => result == a - b;
}
