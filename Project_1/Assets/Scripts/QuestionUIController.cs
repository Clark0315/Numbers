using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionUIController : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI questionIndexText;
    public TextMeshProUGUI answerText; // 用於顯示使用者輸入的答案
    public TextMeshProUGUI scoreText; // 用於顯示目前答對的題數和全部題數
    public TextMeshProUGUI previousQuestionText; // 用於顯示上一個題目和答案
    public Color correctColor = Color.green; // 正確答案的顏色
    public Color incorrectColor = Color.red; // 錯誤答案的顏色

    private void Start()
    {
        if (questionText == null || stageText == null || questionIndexText == null || answerText == null || scoreText == null || previousQuestionText == null)
        {
            Debug.LogError("UI components are not assigned.");
        }
        Clear();
    }

    public void Clear()
    {
        if (previousQuestionText != null)
            previousQuestionText.text = "";
    }

    public void UpdateQuestion(MathQuestion question, int currentStage, int currentQuestionIndex, int maxQuestions)
    {
        if (questionText != null)
        {
            questionText.text = question.ToString();
        }

        if (stageText != null)
        {
            stageText.text = $"Stage: {currentStage}";
        }

        if (questionIndexText != null)
        {
            questionIndexText.text = $"Question: {currentQuestionIndex + 1}/{maxQuestions}";
        }

        if (answerText != null)
        {
            answerText.text = "";
        }
    }

    public void UpdateAnswer(string answer)
    {
        if (answerText != null)
        {
            answerText.text = answer;
        }
    }

    public void UpdateScore(int correctAnswers, int totalQuestions)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {correctAnswers}/{totalQuestions}";
        }
    }

    public void ShowPreviousQuestion(MathQuestion question, string userAnswer, bool isCorrect)
    {
        if (previousQuestionText != null)
        {
            previousQuestionText.text = $"{question.Number1} {question.Operation} {question.Number2} = {userAnswer} ({(isCorrect ? "Correct" : "Incorrect")})";
            previousQuestionText.color = isCorrect ? correctColor : incorrectColor;
        }
    }
}