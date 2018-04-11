using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class ProgressionStatus : MonoBehaviour {

    [Tooltip("start, complete ou fail")]
        public GAProgressionStatus progressionStatus;
    [Tooltip("Le niveau")]
        public string progression01;
    [Tooltip("Le checkpoint dans le niveau")]
        public string progression02;
    //public string progression03;

    // Use this for initialization
    void Start () {

    }

    
    void OnTriggerEnter2D (Collider2D other) {
        Debug.Log("Envoi de données à GameAnalytics...");
        GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
        GameAnalytics.NewDesignEvent("customProgression:" + progression01 + ":" + progression02);
    }

    void OnTriggerEnter (Collider other) {
        // Pour Scholarvox
        Debug.Log("Envoi de données à GameAnalytics via Collider (pas 2D)...");
        GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
        GameAnalytics.NewDesignEvent("customProgression:" + progression01 + ":" + progression02);
    }

    // Update is called once per frame
    void Update () {

    }
}
