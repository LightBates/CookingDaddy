using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinchingManager : MonoBehaviour
{
    private bool playable = true;

    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI answer;

    [SerializeField] private GameObject finger, thumb;

    // 0 for pinch, 1 for smidge, 2 for dash
    [SerializeField] private int pinchLevel;

    [SerializeField] LevelReportCard lrc;


    [SerializeField] private string[] answers;
    [SerializeField] private string[] questions;
    private string[,] sortedAnswers;
    private int answerTable;

    [SerializeField] private TextMeshProUGUI answerText, questionText;
    [SerializeField] float secondLevelAnswer, thirdLevelAnswer;

    // Start is called before the first frame update
    void Start()
    {
        sortedAnswers = new string[answers.Length / 3, 3];
        int counter = 0;
        foreach (string str in answers)
        {
            sortedAnswers[counter / 3, counter % 3] = answers[counter];
            counter++;
        }

        answerTable = (int)(Random.value * (counter / 3));

        answerText.text = sortedAnswers[answerTable, 0];
        questionText.text = questions[answerTable];
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 0f;

        distance = Vector3.Distance(thumb.transform.localPosition, finger.transform.localPosition);
        if(distance > thirdLevelAnswer)
        {
            answerText.text = sortedAnswers[answerTable, 2];
        }else if(distance > secondLevelAnswer)
        {
            answerText.text = sortedAnswers[answerTable, 1];
        }
        else
        {
            answerText.text = sortedAnswers[answerTable, 0];
        }
    }

    public IEnumerator LockInPinch(float dist)
    {
        pinchLevel = 0;
        if(dist > secondLevelAnswer)
        {
            pinchLevel = 1;
        }
        if(dist >= thirdLevelAnswer)
        {
            pinchLevel = 2;
        }

        GameManager.GM.EvaluatePinch(pinchLevel);

        int pinchDiff = Mathf.Abs(pinchLevel - GameManager.GM.GetPreferredPinch());
        int prefPinch = GameManager.GM.GetPreferredPinch();
        int score = 0;

        string headerText;
        string scoreText;

        if(pinchLevel == prefPinch)
        {
            headerText = "A Real Sweetie!";
        }else if(pinchLevel > prefPinch)
        {
            headerText = "You’re Giving Me Diabetes!";
        }
        else
        {
            headerText = "No Sweet Tooth?";
        }

        switch (pinchDiff)
        {
            case 0:
                scoreText = "+50";
                score = 50;
                break;
            case 1:
                scoreText = "+25";
                score = 25;
                break;
            case 2:
                scoreText = "+0";
                break;
            default:
                scoreText = "+0";
                break;
        }

        lrc.SetText(headerText, scoreText);
        lrc.StartMoving();
        yield return new WaitForSeconds(2f);
        GameManager.GM.NextLevel(score);
    }
}
