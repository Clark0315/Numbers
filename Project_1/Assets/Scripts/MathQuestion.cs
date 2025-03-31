using System;

public class MathQuestion
{
    public int Number1 { get; private set; }
    public int Number2 { get; private set; }
    public string Operation { get; private set; }
    public int Answer
    {
        get
        {
            return Operation == "+" ? Number1 + Number2 : Number1 - Number2;
        }
    }

    public MathQuestion(int maxNumber, string operation)
    {
        Operation = operation;
        GenerateQuestion(maxNumber);
    }

    private void GenerateQuestion(int maxNumber)
    {
        Random random = new Random();
        Number1 = random.Next(0, maxNumber + 1);
        Number2 = random.Next(0, maxNumber + 1);

        if (Operation == "-" && Number1 < Number2)
        {
            // Swap to avoid negative results
            int temp = Number1;
            Number1 = Number2;
            Number2 = temp;
        }
    }

    public override string ToString()
    {
        return $"{Number1} {Operation} {Number2} =";
    }
}
