using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatePictureAutoUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = GameManager.GM.GetDatePic();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
