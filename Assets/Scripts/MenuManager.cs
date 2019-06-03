using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private int menuCount;
    private int currentIndex;
    private int gameStartSceneIndex = 2;

    [SerializeField] GameObject[] menuOptions;
    [SerializeField] GameObject cursor;

    [SerializeField] PhoneManager pm;
    [SerializeField] GameObject phone, fader;

    [SerializeField] private LerpInAndOut liao;

    private bool playable = true;

    [SerializeField] private GameObject creditsDisplay, menuDisplay;
    private bool menu = true;
    private bool credits = false;

    // Start is called before the first frame update
    void Start()
    {
        menuCount = menuOptions.Length - 1;
        currentIndex = 0;
        UpdateCursor();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playable)
        {
            return;
        }

        if (credits)
        {
            if (Input.GetButtonDown("Confirm"))
            {
                ShowMenu();
                return;
            }
            return;
        }

        if (Input.GetButtonDown("Down"))
        {
            currentIndex += 1;
            if(currentIndex > menuCount)
            {
                currentIndex = 0;
            }
            UpdateCursor();
        }
        else if (Input.GetButtonDown("Up"))
        {
            currentIndex -= 1;
            if(currentIndex < 0)
            {
                currentIndex = menuCount;
            }
            UpdateCursor();
        }

        if (Input.GetButtonDown("Confirm"))
        {
            switch (currentIndex)
            {
                default:
                    StartGame();
                    break;
                case 0:
                    StartGame();
                    break;
                case 1:
                    Credits();
                    break;
                case 2:
                    Options();
                    break;
            }
        }
    }

    void UpdateCursor()
    {
        Vector3 newPos = cursor.GetComponent<RectTransform>().localPosition;
        newPos.y = menuOptions[currentIndex].GetComponent<RectTransform>().localPosition.y;
        cursor.GetComponent<RectTransform>().localPosition = newPos;
    }

    void StartGame()
    {
        liao.LerpIn();
        playable = false;
        GameManager.GM.StartGame();
    }

    void ShowMenu()
    {
        credits = false;
        creditsDisplay.SetActive(false);
        menuDisplay.SetActive(true);
        cursor.SetActive(true);

    }

    void Tutorial()
    {

    }

    void Credits()
    {
        credits = true;
        creditsDisplay.SetActive(true);
        menuDisplay.SetActive(false);
        cursor.SetActive(false);
    }

    void Options()
    {

    }
    private void Awake()
    {
        GameManager.GM.pm = pm;
        GameManager.GM.phone = phone;
        GameManager.GM.fader = fader;

        phone.SetActive(false);
        fader.SetActive(false);
    }
}
