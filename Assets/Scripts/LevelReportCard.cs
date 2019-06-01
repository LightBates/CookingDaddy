using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelReportCard : MonoBehaviour
{

    private Vector3 startPos, startRot;
    [SerializeField] private Vector3 endPos, endRot;

    [SerializeField] private TextMeshProUGUI scoreHeader, scoreText;

    [SerializeField] private float smoothing = 0.1f;
    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.GetComponent<RectTransform>().localPosition;
        startRot = this.GetComponent<RectTransform>().localRotation.eulerAngles;

        endPos = new Vector3(0, 0, 0);
        endRot = new Vector3(0, 0, Random.Range(0, 6));
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector3 newPos = Vector3.Lerp(this.GetComponent<RectTransform>().localPosition, endPos, smoothing);
            Vector3 newRot = Vector3.Lerp(this.GetComponent<RectTransform>().localRotation.eulerAngles, endRot, smoothing);

            this.GetComponent<RectTransform>().localPosition = newPos;
            this.GetComponent<RectTransform>().localRotation = (Quaternion.Euler(newRot));
        }
    }

    public void SetText(string header, string score)
    {
        scoreHeader.text = header;
        scoreText.text = score;
    }

    public void StartMoving()
    {
        moving = true;
    }
}
