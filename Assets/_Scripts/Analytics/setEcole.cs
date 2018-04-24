using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class setEcole : MonoBehaviour {

    public Dropdown dropdown;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        string school = dropdown.captionText.text;
        //Debug.Log("getschool : " + school);
        PlayerPrefs.SetString("School", school);
    }
}
