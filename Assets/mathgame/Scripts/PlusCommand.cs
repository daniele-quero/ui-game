using UnityEngine;
using UnityEngine.UI;

public class PlusCommand : OperationCommand
{
    public override string Sign => "+";
    public override int Points => 500;
    public override string Name => "Addition";

    public override void Set(Text a, Text b, Text sign, Text feedback, out int aNum, out int bNum, InputField result)
    {
        aNum = Random.Range(0, 31);
        bNum = Random.Range(0, 31);

        CommonSet(a, b, sign, feedback, aNum, bNum, Sign, result);
    }

    public override bool isCorrect(int a, int b, int result) => result == (a + b);
}
