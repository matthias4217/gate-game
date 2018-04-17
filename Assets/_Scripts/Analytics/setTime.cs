using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTime : MonoBehaviour {

    public string niveau;

	// Use this for initialization
	void Start () {
            PlayerPrefs.SetFloat("time" + niveau, Time.time);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
