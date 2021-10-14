using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int questionSeen = 0;
    [SerializeField] int correctAmount = 0;

    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public int GetCorrectAmount()
    {
        return correctAmount;
    }

    public int IncrementQuestionSeen()
    {
        return questionSeen++;
    }

    public int IncrementCorrectAmount()
    {
        return correctAmount++;
    }

    public int CalculateScore()
    {
        if (correctAmount == questionSeen)
            return correctAmount * 10;
        return (correctAmount % questionSeen) * 10;
    }
}
