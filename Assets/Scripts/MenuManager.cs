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
                    Tutorial();
                    break;
                case 2:
                    Credits();
                    break;
                case 3:
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

    void Tutorial()
    {

    }

    void Credits()
    {

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
