using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlwaysFadeOut : MonoBehaviour
{
    [SerializeField] private float lengthOfGlow = 0.5f;
    [SerializeField] private float timer = 0;
    private Image img;
    [SerializeField] private Color startColor, targetColor, newColor;

    // Start is called before the first frame update
    void Start()
    {

        img = this.GetComponent<Image>();
        targetColor = img.color;
        targetColor.a = 0;
        startColor = img.color;
        startColor.a = 1;
        newColor = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < lengthOfGlow)
        {
            timer += Time.deltaTime;
            newColor.a = Mathf.Lerp(startColor.a, targetColor.a, timer / lengthOfGlow);
            img.color = newColor;
        }
        
    }

    public void MakeGlow()
    {
        timer = 0;
        img.color = startColor;
    }
}
