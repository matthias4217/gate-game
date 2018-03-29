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

    
    void onTriggerEnter2D (Collision2D other) {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
    }

    // Update is called once per frame
    void Update () {

    }
}
