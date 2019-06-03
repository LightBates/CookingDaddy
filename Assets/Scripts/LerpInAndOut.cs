using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpInAndOut : MonoBehaviour
{

    private bool lerpingIn, lerpingOut = false;
    private Vector3 startPos, newPos;
    private float timer;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float lerpDur;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpingIn)
        {
            timer += Time.deltaTime;
            newPos = Vector3.Lerp(this.GetComponent<RectTransform>().localPosition, endPos, timer / lerpDur);
            this.GetComponent<RectTransform>().localPosition = newPos;
            return;
        }else if (lerpingOut)
        {
            timer += Time.deltaTime;
            newPos = Vector3.Lerp(this.GetComponent<RectTransform>().localPosition, startPos, timer / lerpDur);
            this.GetComponent<RectTransform>().localPosition = newPos;
            return;
        }
        
    }

    public void LerpIn()
    {
        timer = 0;
        lerpingIn = true;

    }
    public void LerpOut()
    {
        lerpingIn = false;
        timer = 0;
        lerpingOut = true;
    }
}
