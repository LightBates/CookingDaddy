using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZestManager : MonoBehaviour
{

    private bool playable = true;
    [SerializeField] private ZestableFood foodObject;
    [SerializeField] private Vector3 leftPos, rightPos;
    [SerializeField] private int zestLevel;
    [SerializeField] private TextMeshProUGUI zestText;

    [SerializeField] private string[] answers;
    [SerializeField] private string[] questions;
    private string[,] sortedAnswers;
    private int answerTable;

    [SerializeField] private TextMeshProUGUI answerText, questionText;
    [SerializeField] int secondLevelAnswer, thirdLevelAnswer;

    /// <summary>
    /// Current position of the food item.
    /// 0 means left, 1 means right
    /// </summary>
    [SerializeField] private int foodState = 1;


    [SerializeField] private float sprinkleTime = 0f;
    [SerializeField] private float sprinkleDuration;
    [SerializeField] private ParticleSystem ps;

    private int zestTier = 0;

    [SerializeField] LevelReportCard lrc;

    // Start is called before the first frame update
    void Start()
    {
        sortedAnswers = new string[answers.Length / 3, 3];
        int counter = 0;
        foreach(string str in answers)
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
        if (playable)
        {
            if (Input.GetButtonDown("Left") && foodState == 1)
            {
                ToggleFoodPos();
            }
            else if (Input.GetButtonDown("Right") && foodState == 0)
            {
                ToggleFoodPos();
            }

            if (Input.GetButtonDown("Confirm"))
            {
                playable = false;
                StartCoroutine(LockInZest());
            }
        }

        if (sprinkleTime <= 0)
        {
            ps.Stop();
        } else
        {
            sprinkleTime -= Time.deltaTime;
        }
    }

    void IncreaseZest()
    {
        zestLevel++;
        zestText.text = zestLevel.ToString();
        if(zestLevel > secondLevelAnswer)
        {
            zestTier = 1;
            answerText.text = sortedAnswers[answerTable, 1];
        }
        if (zestLevel > thirdLevelAnswer)
        {
            zestTier = 2;
            answerText.text = sortedAnswers[answerTable, 2];
        }
    }

    void ToggleFoodPos()
    {
        IncreaseZest();
        if(foodState == 1)
        {
            foodObject.movingTo = leftPos;
            foodState = 0;
        }
        else
        {
            foodObject.movingTo = rightPos;
            foodState = 1;
        }
        sprinkleTime = sprinkleDuration;
        ps.Play();
    }

    IEnumerator LockInZest()
    {
        GameManager.GM.EvaluateZest(zestTier);

        int zestDiff = Mathf.Abs(zestTier - GameManager.GM.GetPreferredZest());
        int prefZest = GameManager.GM.GetPreferredZest();
        int score = 0;

        string headerText;
        string scoreText;

        if(zestTier == prefZest)
        {
            headerText = "Just the Right Zest!";
        }
        else if (zestTier > prefZest)
        {
            headerText = "Dial Back the Zest!";
        }
        else
        {
            headerText = "Too Bland!";
        }

        switch (zestDiff)
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
        yield return new WaitForSeconds(2.0f);
        GameManager.GM.NextLevel(score);
    }
}
