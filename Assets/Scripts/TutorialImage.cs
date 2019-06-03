using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImage : MonoBehaviour
{
    [SerializeField] private string buttonName;
    [SerializeField] private float timeLength = 1.5f;

    private float timer = -1.0f;
    private Image img;
    private Color startColor, targetColor, newColor;
    private bool isFading = true;

    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
        startColor = img.color;
        startColor.a = 1;
        targetColor = startColor;
        targetColor.a = 0;
        newColor = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonName == "")
        {
            isFading = true;
        } else if  (Input.GetButtonDown(buttonName))
        {
            isFading = true;
        }

        if (isFading)
        {
            timer += Time.deltaTime;
            if (timer > 0)
            {
                newColor.a = Mathf.Lerp(startColor.a, targetColor.a, timer / timeLength);
                img.color = newColor;
            }
        }
    }
}
