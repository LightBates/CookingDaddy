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
    [SerializeField] private string[] potentialAges;
    [SerializeField] private string[] potentialGenders;

    [SerializeField] public string[] zestInterests;
    [SerializeField] public string[] pinchInterests;
    [SerializeField] public string[] sauteInterests;
    

    // Variables used for the "date"
    [SerializeField] private string dateName;
    private List<int> nameList = new List<int>();
    [SerializeField] private Sprite datePic;
    private List<int> picList = new List<int>();
    [SerializeField] private int preferredZest;
    [SerializeField] private int preferredPinch;
    [SerializeField] private int preferredSaute;

    [SerializeField] private int[] minigameSceneIndices;
    private List<int> minigameList = new List<int>();
    private int firstMinigame, secondMinigame, thirdMinigame;
    private int currentScore;
    private int currentGameIndex = 0;

    [SerializeField] private int reportCardSceneIndex;

    [SerializeField] public GameObject fader, phone;
    [SerializeField] public PhoneManager pm;

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
            SceneManager.LoadScene(1);
        }
        else if (Input.GetButtonDown("Exit"))
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
        minigameList.Clear();
        for (int i = 0; i < minigameSceneIndices.Length; i++)
        {
            minigameList.Add(minigameSceneIndices[i]);
        }

        picList.Clear();
        for (int i = 0; i < potentialPics.Length; i++)
        {
            picList.Add(i);
        }
        nameList.Clear();
        for (int i = 0; i < potentialNames.Length; i++)
        {
            nameList.Add(i);
        }

        int randoValue;

        randoValue = (int)(Random.value * (minigameList.Count));
        firstMinigame = minigameList[randoValue];
        minigameList.RemoveAt(randoValue);

        randoValue = (int)(Random.value * (minigameList.Count));
        secondMinigame = minigameList[randoValue];
        minigameList.RemoveAt(randoValue);

        randoValue = (int)(Random.value * (minigameList.Count));
        thirdMinigame = minigameList[randoValue];
        minigameList.RemoveAt(randoValue);

        currentScore = 0;
        level1Score = 0;
        level2score = 0;
        level3score = 0;
        

        for (int i = 0; i <= 2; i++)
        {
            if (potentialPics.Length != 0)
            {
                int rand = (int)(Random.value * (picList.Count));
                datePic = potentialPics[picList[rand]];
                picList.RemoveAt(rand);
            }
            

            int randoVal = (int)(Random.value * (nameList.Count));
            dateName = potentialNames[nameList[randoVal]];
            nameList.RemoveAt(randoVal);


            preferredZest = (int)(Random.value * 3f);
            preferredPinch = (int)(Random.value * 3f);
            preferredSaute = (int)(Random.value * 3f);

            pm.SetProfile
                (i, 
                datePic, 
                dateName, 
                potentialGenders[(int)(Random.value * (potentialGenders.Length-1))],
                potentialAges[(int)(Random.value * (potentialAges.Length - 1))],
                preferredZest, 
                preferredSaute, 
                preferredPinch);
        }

        fader.SetActive(true);
        phone.SetActive(true);

        currentGameIndex = 1;
    }

    public void SetDate(string name, Sprite pic, int zest, int pinch, int saute)
    {
        dateName = name;
        if(pic != null)
        {
            datePic = pic;
        }
        preferredZest = zest;
        preferredPinch = pinch;
        preferredSaute = saute;

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

    public void EvaluateSaute(int saute)
    {
        if(saute == preferredSaute)
        {
            currentScore += 50;
        }else if(Mathf.Abs(saute - preferredPinch) > 1)
        {
            currentScore += 0;
        }
        else
        {
            currentScore += 25;
        }
    }

    public void LockInDate(string name, Sprite pic, int zest, int pinch, int saute)
    {
        dateName = name;
        datePic = pic;
        preferredZest = zest;
        preferredPinch = pinch;
        preferredSaute = saute;
}

    public int GetPreferredZest()
    {
        return preferredZest;
    }

    public int GetPreferredPinch()
    {
        return preferredPinch;
    }

    public int GetPreferredSaute()
    {
        return preferredSaute;
    }

    public Sprite GetDatePic()
    {
        return datePic;
    }

    public void MakeFinalReport(FinalReportCard frc)
    {
        frc.SetScores(level1Score, level2score, level3score);
    }
}
