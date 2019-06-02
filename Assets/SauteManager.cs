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
    private float firstLevelSauteCutoff = 5.0f;
    private float secondLevelSauteCutoff = 10.0f;

    [SerializeField] private LevelReportCard lrc;

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
                Debug.Log("STOP");
                counting = false;
            }
            else
            {
                counting = true;
            }
        }

        if (counting)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString().Substring(0, 5);
        }
        
    }

    IEnumerator LockInSaute()
    {

        yield return new WaitForSeconds(2.0f);
    }
}
