using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalReportCard : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score1, score2, score3, totalScore;

    // This is text for now, and should be replaced with sprites
    [SerializeField] TextMeshProUGUI letterGrade;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.MakeFinalReport(this);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Confirm"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SetScores(int s1, int s2, int s3)
    {
        score1.text = s1.ToString();
        score2.text = s2.ToString();
        score3.text = s3.ToString();

        totalScore.text = (s1 + s2 + s3).ToString();

        // TODO: Replace text functions here with sprites

        float scorePercentage = (float)(s1+ s2 +s3) / 150f;
        letterGrade.text = "F";
        if (scorePercentage > 0.6f)
        {
            letterGrade.text = "D";
        }
        if(scorePercentage > 0.7f)
        {
            letterGrade.text = "C";
        }
        if(scorePercentage > 0.8f)
        {
            letterGrade.text = "B";
        }
        if (scorePercentage > 0.9f)
        {
            letterGrade.text = "A";
        }
        if(scorePercentage >= 1.0f)
        {
            letterGrade.text = "A+";
        }
    }
}
