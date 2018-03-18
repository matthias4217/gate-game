using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 13/03: Currently useless separation with PlatformerEnemyController, but I still think it's clean
[RequireComponent (typeof (PlatformerEnemyController))]
public class PlatformerEnemy : MonoBehaviour {
	
	PlatformerEnemyController controller;

	void Start() {
		controller = GetComponent<PlatformerEnemyController> ();
	}

	void Update() {
		Vector3 moveAmount = controller.CalculateMovement ();
		controller.Move (moveAmount);
	}



}
