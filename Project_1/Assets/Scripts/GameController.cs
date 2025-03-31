using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public List<ButtonClick> numButtons = new List<ButtonClick>();
    public List<ButtonClick> otherButtons = new List<ButtonClick>();
    public QuestionUIController questionUIController;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private QuestionManager questionManager = new QuestionManager();
    private List<MathQuestion> questions;
    private string currentAnswer = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questions = questionManager.GenerateQuestions();

        foreach (var button in numButtons)
        {
            button.SetupRegisterKeys(button.buttonTypes, button.Text, OnNumberClick);
        }
        foreach (var button in otherButtons)
        {
            button.SetupRegisterKeys(button.buttonTypes, string.Empty, OnOtherClick);
        }
        
        if (questionUIController != null)
        {
            questionUIController.UpdateQuestion(questions[currentQuestionIndex], questionManager.CurrentStage, currentQuestionIndex, questionManager.MaxQuestions);
            questionUIController.UpdateScore(correctAnswers, questionManager.MaxQuestions);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentQuestionIndex >= questionManager.MaxQuestions)
        {
            Debug.Log("Game Over");
            // Handle game over logic here
        }
    }

    public void OnOtherClick(Key key)
    {
        if (currentQuestionIndex < questionManager.MaxQuestions)
        {
            if (key == Key.Enter || key == Key.Equals)
            {
                MathQuestion currentQuestion = questions[currentQuestionIndex];
                Debug.Log($"Question {currentQuestionIndex + 1}: {currentQuestion}");
                bool isCorrect = int.TryParse(currentAnswer, out int answer) && answer == currentQuestion.Answer;

                if (isCorrect)
                {
                    correctAnswers++;
                }

                if (questionUIController != null)
                {
                    questionUIController.ShowPreviousQuestion(currentQuestion, currentAnswer, isCorrect);
                    questionUIController.UpdateScore(correctAnswers, questionManager.MaxQuestions);
                }

                currentQuestionIndex++;
                if (currentQuestionIndex % questionManager.MaxQuestions == 0)
                {
                    questionManager.NextStage();
                    questions = questionManager.GenerateQuestions();
                }

                if (questionUIController != null && questions.Count > currentQuestionIndex)
                {
                    questionUIController.UpdateQuestion(questions[currentQuestionIndex], questionManager.CurrentStage, currentQuestionIndex, questionManager.MaxQuestions);
                }

                currentAnswer = "";
            }
            else if (key == Key.Backspace || key == Key.C)
            {
                if (currentAnswer.Length > 0)
                {
                    currentAnswer = key == Key.C ? string.Empty : currentAnswer.Substring(0, currentAnswer.Length - 1);
                    if (questionUIController != null)
                    {
                        questionUIController.UpdateAnswer(currentAnswer);
                    }
                }
            }
        }
    }

    public void OnNumberClick(Key key)
    {
        if (currentQuestionIndex < questionManager.MaxQuestions)
        {
            currentAnswer += key.ToString().Replace("Digit", "").Replace("Numpad", "");
            if (questionUIController != null)
            {
                questionUIController.UpdateAnswer(currentAnswer);
            }
        }
    }
}
