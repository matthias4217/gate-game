using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour {

    public GameObject player;
    private Transform trans;
	// Use this for initialization
	void Start () {
        trans = player.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = trans.position + new Vector3(0,0,-5);

	}
}
