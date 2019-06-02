﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFinger : MonoBehaviour
{

    [SerializeField] private Vector3 leftPos, rightPos;
    [SerializeField] private GameObject thumb;
    [SerializeField] private float dunkAmount;

    [SerializeField] private float timer;
    [SerializeField] private float asLongAsItShouldTake;

    private bool moving = true;
    private bool movingLeft = false;
    private bool dunking = false;
    private bool undunking = false;
    private float dunkTime = 0.1f;
    private float amount;

    private Vector3 dunkStartPos, dunkEndPos;
    private Vector3 thumbStartPos;

    [SerializeField] private PinchingManager pm;

    // Start is called before the first frame update
    void Start()
    {
        thumbStartPos = thumb.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {

            Vector3 newPos = new Vector3();
            Vector3 newThumbPos = new Vector3();
            timer += Time.deltaTime;

            if (Input.GetButtonDown("Confirm"))
            {
                amount = Vector3.Distance(this.transform.localPosition, leftPos);
                dunkStartPos = this.transform.localPosition;
                dunkEndPos = this.transform.localPosition;
                dunkEndPos.y -= dunkAmount;
                timer = 0;
                dunking = true;
            }

            if (timer > asLongAsItShouldTake && !dunking)
            {
                timer = 0;
                movingLeft = !movingLeft;
            }


            if (dunking)
            {
                timer += Time.deltaTime;
                newPos = Vector3.Lerp(dunkStartPos, dunkEndPos, timer / dunkTime);
                newThumbPos = thumb.transform.localPosition;
                newThumbPos.y = Mathf.Lerp( thumbStartPos.y, thumbStartPos.y - dunkAmount, (timer / dunkTime));

                thumb.transform.localPosition = newThumbPos;

                if (timer > dunkTime)
                {
                    timer = 0;
                    undunking = true;
                    dunking = false;
                    StartCoroutine(pm.LockInPinch(amount));
                }

            } else if (undunking)
            {
                timer += Time.deltaTime;
                newPos = Vector3.Lerp(dunkEndPos, dunkStartPos, timer / dunkTime);
                newThumbPos = thumb.transform.localPosition;
                newThumbPos.y = Mathf.Lerp(thumbStartPos.y - dunkAmount, thumbStartPos.y, (timer / dunkTime));

                thumb.transform.localPosition = newThumbPos;
                if (timer > dunkTime)
                {
                    moving = false;
                    undunking = false;
                }

            }
            else if (movingLeft)
            {
                newPos = Vector3.Lerp(rightPos, leftPos, timer / asLongAsItShouldTake);
            }
            else if (!movingLeft)
            {
                newPos = Vector3.Lerp(leftPos, rightPos, timer / asLongAsItShouldTake);
            }

            this.transform.localPosition = newPos;
        }

    }
}
