using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DatingProfile : MonoBehaviour
{

    [SerializeField] private Image displayPicture;
    [SerializeField] private string dateName;
    [SerializeField] private string gender;
    [SerializeField] private string age;

    [SerializeField] private string interest1, interest2, interest3;

    public int preferredZest, preferredSaute, preferredPinch;

    public Sprite pic;
    private TextMeshProUGUI nameText, genderText, ageText, interestsText;

    // Start is called before the first frame update
    void Start()
    {
        nameText = this.transform.Find("name").GetComponent<TextMeshProUGUI>();
        genderText = this.transform.Find("gender").GetComponent<TextMeshProUGUI>();
        ageText = this.transform.Find("age").GetComponent<TextMeshProUGUI>();
        interestsText = this.transform.Find("interests").GetComponent<TextMeshProUGUI>();
        displayPicture = this.transform.Find("displayPic").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeProfile(Sprite pic, string name, string gender, string age, int prefZest, int prefSaute, int prefPinch)
    {

        nameText = this.transform.Find("name").GetComponent<TextMeshProUGUI>();
        genderText = this.transform.Find("gender").GetComponent<TextMeshProUGUI>();
        ageText = this.transform.Find("age").GetComponent<TextMeshProUGUI>();
        interestsText = this.transform.Find("interests").GetComponent<TextMeshProUGUI>();
        displayPicture = this.transform.Find("displayPic").GetComponent<Image>();

        if (pic != null)
        {
            displayPicture.sprite = pic;
            this.pic = pic;
        }
        this.dateName = name;
        this.nameText.text = name;
        this.gender = gender;
        this.genderText.text = gender;
        this.age = age;
        this.ageText.text = age;

        //TODO: Make randomized string table
        string interestsParagraph = "";
        interestsParagraph += GameManager.GM.zestInterests[prefZest] + "\n\n";
        interestsParagraph += GameManager.GM.pinchInterests[prefPinch] + "\n\n";
        interestsParagraph += GameManager.GM.sauteInterests[prefSaute] + "\n\n";

        preferredSaute = prefSaute;
        preferredZest = prefZest;
        preferredPinch = prefPinch;

        interestsText.text = interestsParagraph;
    }


}
