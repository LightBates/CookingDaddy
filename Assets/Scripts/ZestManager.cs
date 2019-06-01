using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZestManager : MonoBehaviour
{

    private bool playable = true;
    [SerializeField] private ZestableFood foodObject;
    [SerializeField] private Vector3 leftPos, rightPos;

    /// <summary>
    /// Current position of the food item.
    /// 0 means left, 1 means right
    /// </summary>
    [SerializeField]private int foodState = 1;


    [SerializeField] private float sprinkleTime = 0f;
    [SerializeField] private float sprinkleDuration;
    [SerializeField] private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playable)
        {
            if(Input.GetButtonDown("Left") && foodState == 1)
            {
                ToggleFoodPos();
            }
            else if(Input.GetButtonDown("Right") && foodState == 0)
            {
                ToggleFoodPos();
            }
        }

        if(sprinkleTime <= 0 )
        {
            ps.Stop();
        } else
        {
            sprinkleTime -= Time.deltaTime;
        }
    }

    void ToggleFoodPos()
    {
        if(foodState == 1)
        {
            foodObject.movingTo = leftPos;
            foodState = 0;
        }
        else
        {
            foodObject.movingTo = rightPos;
            foodState = 1;
        }
        sprinkleTime = sprinkleDuration;
        ps.Play();
    }
}
