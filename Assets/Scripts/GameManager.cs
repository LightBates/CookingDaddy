using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private int score;
    private int level1Score, level2score, level3score;

    // Tables for random date assemblage
    [SerializeField] private string[] potentialNames;
    [SerializeField] private Sprite[] potentialPics;

    // Variables used for the "date"
    [SerializeField] private string dateName;
    [SerializeField] private Sprite datePic;
    [SerializeField] private int preferredZest;
    [SerializeField] private int preferredPinch;

    [SerializeField] private int[] minigameSceneIndices;
    private int firstMinigame, secondMinigame, thirdMinigame;
    private int currentScore;
    private int currentGameIndex = 0;

    [SerializeField] private int reportCardSceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        if(GM == null)
        {
            GM = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        // Set default values
        firstMinigame = minigameSceneIndices[(int)(Random.value * (minigameSceneIndices.Length))];
        secondMinigame = minigameSceneIndices[(int)(Random.value * (minigameSceneIndices.Length ))];
        thirdMinigame = minigameSceneIndices[(int)(Random.value * (minigameSceneIndices.Length ))];

        currentScore = 0;
        level1Score = 0;
        level2score = 0;
        level3score = 0;

        Debug.Log(firstMinigame);

        //
        if(potentialPics.Length != 0)
        {
            datePic = potentialPics[(int)(Random.value * (potentialPics.Length - 1))];
        }
        dateName = potentialNames[(int)(Random.value * (potentialNames.Length - 1))];
        preferredZest = (int)(Random.value * 3f);

        Debug.Log("Date's name is " + dateName);
        Debug.Log("Preferred zest level: " + preferredZest);

        currentGameIndex = 1;
        SceneManager.LoadScene(firstMinigame);

    }

    public void NextLevel(int score)
    {
        if(currentGameIndex == 1)
        {
            level1Score = score;
            SceneManager.LoadScene(secondMinigame);
        }else if (currentGameIndex == 2)
        {
            level2score = score;
            SceneManager.LoadScene(thirdMinigame);
        }
        else
        {
            level3score = score;
            SceneManager.LoadScene(reportCardSceneIndex);
        }
        currentScore += score;
        currentGameIndex++;
    }

    // Expected values: 0 for no zest, 1 for medium zest, 2 for high zest
    public void EvaluateZest(int zestiness)
    {
        if (zestiness == preferredZest)
        {
            currentScore += 50;
        }
        else if (Mathf.Abs(zestiness - preferredZest) > 1)
        {
            currentScore += 0;
        }
        else
        {
            currentScore += 25;
        }
    }

    public void EvaluatePinch(int pinch)
    {
        if(pinch == preferredPinch)
        {
            currentScore += 50;
        }else if (Mathf.Abs(pinch - preferredPinch) > 1)
        {
            currentScore += 0;
        }
        else
        {
            currentScore += 25;
        }
    }

    public int GetPreferredZest()
    {
        return preferredZest;
    }

    public int GetPreferredPinch()
    {
        return preferredPinch;
    }

    public void MakeFinalReport(FinalReportCard frc)
    {
        frc.SetScores(level1Score, level2score, level3score);
    }
}
