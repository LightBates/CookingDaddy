using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SauteManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;

    private float timer;
    private bool counting;
    private bool playable = true;

    private int sauteLevel;
    private float firstLevelSauteCutoff = 3.0f;
    private float secondLevelSauteCutoff = 6.0f;

    [SerializeField] private LevelReportCard lrc;
    [SerializeField] private ParticleSystem ps;

    [SerializeField] private GameObject crumblies;
    [SerializeField] private Gradient rawToCooked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playable)
        {
            return;
        }

        if (Input.GetButtonDown("Confirm"))
        {
            if (counting)
            {
                counting = false;
                ps.Stop();

                sauteLevel = 0;
                if (timer >= firstLevelSauteCutoff)
                {
                    sauteLevel = 1;
                }
                if (timer >= secondLevelSauteCutoff)
                {
                    sauteLevel = 2;
                }

                Debug.Log("Saute level is " + sauteLevel);
                StartCoroutine(LockInSaute());
            }
            else
            {
                ps.Play();
                counting = true;
            }
        }

        if (counting)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString().Substring(0, 5);
            if(timer <= secondLevelSauteCutoff)
            {
                crumblies.GetComponent<MeshRenderer>().material.color = rawToCooked.Evaluate(timer / secondLevelSauteCutoff);
            }
            else
            {
                crumblies.GetComponent<MeshRenderer>().material.color = rawToCooked.Evaluate(1);
            }
        }
        
    }

    IEnumerator LockInSaute()
    {
        playable = false;
        GameManager.GM.EvaluateSaute(sauteLevel);

        int sauteDiff = Mathf.Abs(sauteLevel - GameManager.GM.GetPreferredSaute());
        int prefSaute = GameManager.GM.GetPreferredSaute();
        int score = 0;

        string headerText;
        string scoreText;

        if(sauteLevel == prefSaute)
        {
            headerText = "Just right!";
        }else if (sauteLevel > prefSaute)
        {
            headerText = "Kinda awkward...";
        }
        else
        {
            headerText = "Are you always 'on'?";
        }

        switch (sauteDiff)
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
        yield return new WaitForSeconds(1.0f);
        GameManager.GM.NextLevel(score);
    }
}
