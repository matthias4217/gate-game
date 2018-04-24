﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class collectableAnalytics : MonoBehaviour {


    [Tooltip("identifiant de l'objet")]
        public string id;
    [Tooltip("Niveau")]
        public string niveau;

    [Tooltip("TEM, TSP ou Autre")]
        private string school;

    // Use this for initialization
    void Start () {
        school = PlayerPrefs.GetString("School"); 

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter2D (Collider2D other) {
            sendCollectableAnalytics();
    }

    void OnTriggerEnter (Collider other) {
        // Pour Scholarvox
            sendCollectableAnalytics();
    }

    void sendCollectableAnalytics() {
        GameAnalytics.NewDesignEvent(school + ":collectable:" + niveau + ":" + id);
        //Debug.Log(school + ":collectable:" + niveau + ":" + id);
    }
}
