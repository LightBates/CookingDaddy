using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalReportCard : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score1, score2, score3, totalScore;

    // This is text for now, and should be replaced with sprites
    [SerializeField] TextMeshProUGUI letterGrade, header;

    [SerializeField] private ParticleSystem ps;

    [SerializeField] private Image stamp;
    [SerializeField] private Sprite aStamp, bStamp, cStamp, dStamp, fStamp;

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
        

        float scorePercentage = (float)(s1+ s2 +s3) / 150f;
        stamp.sprite = fStamp;
        header.text = "C’est Bullshit!";

        ParticleSystem.EmissionModule em = ps.emission;

        em.rateOverTime = 1f;
        ps.Play();

        if (scorePercentage >= 0.5f)
        {
            stamp.sprite = dStamp;
            header.text = "Laisse tomber…";
            em.rateOverTime = 5f;
        }
        if(scorePercentage > 0.6f)
        {
            stamp.sprite = cStamp;
            header.text = "Allez!";
            em.rateOverTime = 10f;
        }
        if(scorePercentage > 0.8f)
        {
            stamp.sprite = bStamp;
            header.text = "Génial!";
            em.rateOverTime = 20f;
        }
        
        if(scorePercentage >= 1.0f)
        {
            stamp.sprite = aStamp;
            header.text = "C'est Magnifique!";
            em.rateOverTime = 50f;
        }
    }
}
