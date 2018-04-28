using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTimeInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    PlayerPrefs.SetFloat("timeInit", Time.time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
