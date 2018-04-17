using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class ProgressionStatus : MonoBehaviour {

    //[Tooltip("start, complete ou fail")]
    //    public GAProgressionStatus progressionStatus;
    [Tooltip("Le niveau")]
        public string niveau;
    [Tooltip("Le checkpoint dans le niveau")]
        public string checkpoint;
    private string school;
    private float timeStart;

    // Use this for initialization
    void Start () {
        school = PlayerPrefs.GetString("School");
        // get the time at the beginning of the level
        timeStart = PlayerPrefs.GetFloat("time" + niveau);
    }

    void sendProgression() {
        float deltaTime = Time.time - timeStart; // minus time at the beginning of the level...
        Debug.Log("Envoi de données à GameAnalytics...");
        Debug.Log(deltaTime);
        //GameAnalytics.NewProgressionEvent(progressionStatus, niveau, checkpoint);
        GameAnalytics.NewDesignEvent(school + ":progression:" + niveau + ":" + checkpoint, deltaTime);
    }
    
    void OnTriggerEnter2D (Collider2D other) {
        sendProgression();
    }

    void OnTriggerEnter (Collider other) {
        // Pour Scholarvox
        sendProgression();
    }

    // Update is called once per frame
    void Update () {

    }
}
