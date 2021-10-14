using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public float fillFraction;
    private float timerValue;
    public bool isAnsweringQuestion;

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        //Neu da tra loi cau hoi
        if(isAnsweringQuestion)
        {
            //Neu con thoi gian
            if(timerValue > 0)
            {
                //Cap nhat fill fraction
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            //Neu het thoi gian
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        //Neu chua tra loi cau hoi
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
}
