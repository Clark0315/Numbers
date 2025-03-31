using System;
using System.Collections;
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
    private DateTime startTime; // 遊戲開始時間
    private bool isGameOver = false; // 是否遊戲結束
    private bool isGameStarted = false; // 是否遊戲開始
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

        StartCoroutine(StartCountdown());
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
            else if(key == Key.R)
            {
                RestartGame();
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
    // Game Process
    private IEnumerator StartCountdown()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            if (questionUIController != null)
            {
                questionUIController.UpdateTime($"{countdown} s");
            }
            yield return new WaitForSeconds(1);
            countdown--;
        }
        isGameOver = false;
        isGameStarted = true;
        startTime = DateTime.Now;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameStarted && !isGameOver)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            if (questionUIController != null)
            {
                questionUIController.UpdateTime($"{elapsedTime.ToString(@"mm\:ss")}");
            }
            if (currentQuestionIndex >= questionManager.MaxQuestions)
            {
                Debug.Log("Game Over");
                isGameOver = true;
                isGameStarted = false;
                // Handle game over logic here
            }
        }
    }
    /// 重新開始
    public void RestartGame()
    {
        currentQuestionIndex = 0;
        correctAnswers = 0;
        currentAnswer = "";
        isGameOver = false;
        isGameStarted = false;
        questions = questionManager.GenerateQuestions();

        if (questionUIController != null)
        {
            questionUIController.UpdateQuestion(questions[currentQuestionIndex], questionManager.CurrentStage, currentQuestionIndex, questionManager.MaxQuestions);
            questionUIController.UpdateScore(correctAnswers, questionManager.MaxQuestions);
        }

        StartCoroutine(StartCountdown());
    }
   
    // End Game Process
}
