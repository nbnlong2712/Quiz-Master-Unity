using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Questions")]
public class QuestionSO : ScriptableObject
{
    [TextArea(1, 6)]
    [SerializeField] private string question = "Enter new question here!";
    [SerializeField] private string[] answer = new string[4];
    [SerializeField] private int correctAnswer;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answer[index];
    }

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }
}
