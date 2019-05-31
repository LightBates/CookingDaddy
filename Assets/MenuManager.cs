using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private int menuCount;
    private int currentIndex;
    private int gameStartSceneIndex = 1;

    [SerializeField] GameObject[] menuOptions;
    [SerializeField] GameObject cursor;

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
        SceneManager.LoadScene(gameStartSceneIndex);
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
}
