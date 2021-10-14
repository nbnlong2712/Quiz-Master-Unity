using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;
    public bool isComplete;


    [Header("Answer")]
    [SerializeField] private GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnswerEarly = true;

    [Header("Button colors")]
    [SerializeField] private Sprite userAnswerImage;
    [SerializeField] private Sprite correctAnswerImage;

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    private Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    void Start()
    {
        //khoi tao timer (tham chieu den Timer trong unity)
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

        //GetNextQuestion();
    }

    private void Update()
    {
        //timerImage se hien thi fraction theo fillFraction cua bien timer
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnswerEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!timer.isAnsweringQuestion && !hasAnswerEarly)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnswerEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        UpdateScoreText(scoreKeeper.GetQuestionSeen());
    }

    public void DisplayAnswer(int index)
    {
        Image imageAnswer;
        if (index == currentQuestion.GetCorrectAnswer())
        {
            questionText.text = "Correct";
            imageAnswer = answerButtons[index].GetComponent<Image>();
            imageAnswer.sprite = correctAnswerImage;

            scoreKeeper.IncrementCorrectAmount();
        }
        else
        {
            questionText.text = "Sorry, but correct answer is: " + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswer());
            imageAnswer = answerButtons[currentQuestion.GetCorrectAnswer()].GetComponent<Image>();
            imageAnswer.sprite = correctAnswerImage;
        }
    }

    public void GetRandomQuestion()
    {
        if (questions.Count > 0)
        {
            isComplete = false;
            int index = Random.Range(0, questions.Count);

            currentQuestion = questions[index];
            if (questions.Contains(currentQuestion))
            {
                questions.Remove(currentQuestion);
            }
        }
        else
        {
            isComplete = true;
        }
    }

    public void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        //Get text(TMP) in a button
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI textButton = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            textButton.text = currentQuestion.GetAnswer(i);
        }
    }

    public void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void SetButtonToDefault()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image image = answerButtons[i].GetComponent<Image>();
            image.sprite = userAnswerImage;
        }
    }

    public void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            GetRandomQuestion();
            DisplayQuestion();
            SetButtonToDefault();
            SetButtonState(true);
            scoreKeeper.IncrementQuestionSeen();
            UpdateScoreText(scoreKeeper.GetQuestionSeen());
            progressBar.value++;
        }
        else
        {
            isComplete = true;
        }
    }

    public void UpdateScoreText(int questionSeenAmount)
    {
        scoreText.text = (scoreKeeper.CalculateScore()) + "/" + (questionSeenAmount * 10);
    }
}
