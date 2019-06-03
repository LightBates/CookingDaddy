using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeInFadeOut : MonoBehaviour
{

    [SerializeField] private float waitToStart, fadeInTime, waitTime, fadeOutTime;
    public float timer;

    private Color startColor, endColor, newColor;
    private Image img;

    private bool fadingIn, fadingOut;

    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();

        startColor = img.color;
        startColor.a = 0;
        endColor = img.color;
        endColor.a = 1;
        newColor = img.color;

        StartCoroutine(kickoff());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(fadingOut)
        {
            newColor.a = Mathf.Lerp(endColor.a, startColor.a, timer / fadeOutTime);

        }

        if (fadingIn)
        {
            
            newColor.a = Mathf.Lerp(startColor.a, endColor.a, timer / fadeInTime);
        }

        img.color = newColor;
        
    }

    private IEnumerator kickoff()
    {
        yield return new WaitForSeconds(waitToStart);
        timer = 0;
        fadingIn = true;
        yield return new WaitForSeconds(fadeInTime);
        fadingIn = false;
        timer = 0;
        yield return new WaitForSeconds(waitTime);
        fadingOut = true;
        timer = 0;
        yield return new WaitForSeconds(fadeOutTime + 0.5f);

        SceneManager.LoadScene(1);
    }
}
