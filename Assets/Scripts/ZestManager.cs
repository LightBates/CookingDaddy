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
    private string[,] sortedAnswers;
    private int answerTable;

    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] int secondLevelAnswer, thirdLevelAnswer;

    /// <summary>
    /// Current position of the food item.
    /// 0 means left, 1 means right
    /// </summary>
    [SerializeField] private int foodState = 1;


    [SerializeField] private float sprinkleTime = 0f;
    [SerializeField] private float sprinkleDuration;
    [SerializeField] private ParticleSystem ps;

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
            answerText.text = sortedAnswers[answerTable, 1];
        }
        if (zestLevel > thirdLevelAnswer)
        {
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
}
