using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{

    [SerializeField] private Image rightGlow, leftGlow;
    [SerializeField] private DatingProfile profile1, profile2, profile3;
    [SerializeField] private DatingProfile[] dps;
    [SerializeField] private LerpInAndOut liao;

    [SerializeField] private Vector3 profile1TargetLoc, newPos;
    private float moveDist;
    private float smoothing = 0.1f;

    private int currentProfile = 0;

    [SerializeField] private float xMoveOffset;

    // Start is called before the first frame update
    void Awake()
    {
        profile1TargetLoc = profile1.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Right"))
        {
            if(!(currentProfile >= 2))
            {
                currentProfile += 1;
                profile1TargetLoc.x -= xMoveOffset;
            }
            rightGlow.GetComponent<AlwaysFadeOut>().MakeGlow();
        }
        else if (Input.GetButtonDown("Left"))
        {
            if (!(currentProfile <= 0))
            {
                currentProfile -= 1;
                profile1TargetLoc.x += xMoveOffset;
            }
            leftGlow.GetComponent<AlwaysFadeOut>().MakeGlow();
        }

        if(Vector3.Distance(profile1.GetComponent<RectTransform>().localPosition, profile1TargetLoc) > 0.5f)
        {
            newPos = Vector3.Lerp(profile1.GetComponent<RectTransform>().localPosition, profile1TargetLoc, smoothing);
            moveDist = newPos.x - profile1.GetComponent<RectTransform>().localPosition.x;

            profile1.GetComponent<RectTransform>().localPosition = newPos;

            newPos = profile2.GetComponent<RectTransform>().localPosition;
            newPos.x += moveDist;
            profile2.GetComponent<RectTransform>().localPosition = newPos;

            newPos = profile3.GetComponent<RectTransform>().localPosition;
            newPos.x += moveDist;
            profile3.GetComponent<RectTransform>().localPosition = newPos;
        }

        if (Input.GetButtonDown("Submit"))
        {
            StartCoroutine(DismissPhone());
        }

    }

    public void SetProfile(int profileIndex, Sprite pic, string name, string gender, string age, int prefZest, int prefSaute, int prefPinch)
    {
        dps[profileIndex].MakeProfile(pic, name, gender, age, prefZest, prefSaute, prefPinch);
    }

    private IEnumerator DismissPhone()
    {
        liao.LerpOut();
        yield return new WaitForSeconds(0.5f);
        GameManager.GM.SetDate(dps[currentProfile].name, dps[currentProfile].pic, dps[currentProfile].preferredZest, dps[currentProfile].preferredPinch, dps[currentProfile].preferredSaute);
    }

}
