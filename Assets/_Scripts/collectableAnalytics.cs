using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableAnalytics : MonoBehaviour {

    [Tooltip("identifiant de l'objet")]
        public int id;

    [Tooltip("TEM, TSP ou Autre")]
        private string school; 

    // Use this for initialization
    void Start () {
        GameAnalytics.NewDesignEvent(school + ":collectable", id);

    }

    // Update is called once per frame
    void Update () {

    }
}
