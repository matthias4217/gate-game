using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float moveSpeed;
	Vector2 directionalInput;

	void Start () {
		
	}
	
	void Update () {
		transform.Translate (moveSpeed * directionalInput);
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}




}
