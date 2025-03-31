using System.Collections.Generic;

public class QuestionManager
{
    private int currentStage = 1;
    private int maxQuestions = 10;
    private List<MathQuestion> questions = new List<MathQuestion>();

    public List<MathQuestion> GenerateQuestions()
    {
        questions.Clear();
        int maxNumber = GetMaxNumberForStage(currentStage);
        for (int i = 0; i < maxQuestions; i++)
        {
            string operation = i % 2 == 0 ? "+" : "-";
            questions.Add(new MathQuestion(maxNumber, operation));
        }
        return questions;
    }

    private int GetMaxNumberForStage(int stage)
    {
        switch (stage)
        {
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 20;
            default:
                return 5;
        }
    }

    public void NextStage()
    {
        currentStage++;
    }

    public int CurrentStage => currentStage;
    public int MaxQuestions => maxQuestions;
}
