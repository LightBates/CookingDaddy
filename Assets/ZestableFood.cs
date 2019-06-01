using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZestableFood : MonoBehaviour
{
    public Vector3 movingTo;
    [SerializeField] private float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(Vector3.Distance(this.transform.localPosition, movingTo) <= 0.1)
        {
            return;
        }
        else
        {
            Vector3 newPos = Vector3.Lerp(this.transform.localPosition, movingTo, smoothing);
            this.transform.localPosition = newPos;
        }
    }
}
