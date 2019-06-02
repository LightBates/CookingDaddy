using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinchingManager : MonoBehaviour
{
    private bool playable = true;

    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI answer;

    // 0 for pinch, 1 for smidge, 2 for dash
    [SerializeField] private int pinchLevel;

    [SerializeField] LevelReportCard lrc;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LockInPinch(float dist)
    {
        pinchLevel = 0;
        if(dist > 3.5)
        {
            pinchLevel = 1;
        }
        if(dist > 7)
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
            headerText = "Just right!";
        }else if(pinchLevel > prefPinch)
        {
            headerText = "Too clingy!";
        }
        else
        {
            headerText = "Too stingy!";
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
        yield return new WaitForSeconds(1.0f);
        GameManager.GM.NextLevel(score);
    }
}
