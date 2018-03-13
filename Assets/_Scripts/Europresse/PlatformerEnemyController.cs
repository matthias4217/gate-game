using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerEnemyController : MonoBehaviour {

	public Vector3[] localWaypoints;
	Vector3[] globalWaypoints;	// Waypoints but with global coordinates

	public float moveSpeed = 3;		// The speed of the enemy
	public bool cyclic;			// Is the movement cyclic or does the platform goes back and forth
	public float waitTime;		// Time waited at a waypoint
	[Range(0,3)]
	public float easeAmount;

	int fromWaypointIndex;
	float percentBetweenWaypoints;
	float nextMoveTime;
	int platformDirection = 1;		// Indicates if the platform is moving in forward or backward in its path (in a non cyclical movement)

	public void Start () {
		//base.Start ();

		// Initializing the globalWaypoints array
		globalWaypoints = new Vector3[localWaypoints.Length];
		for (int i = 0; i < localWaypoints.Length; i++) {
			globalWaypoints[i] = transform.position + localWaypoints[i];
		}
	}

	void Update () {
		// UpdateRaycastOrigins ();


	}

	float Ease(float x) {
		float a = easeAmount + 1;
		return Mathf.Pow(x,a) / (Mathf.Pow(x,a) + Mathf.Pow(1-x, a));
	}
		
	public Vector3 CalculateMovement() {
		if (Time.time < nextMoveTime) {
			return Vector3.zero;
		}

		fromWaypointIndex %= globalWaypoints.Length;
		int toWaypointIndex = (fromWaypointIndex + platformDirection) % globalWaypoints.Length;
		float distanceBetweenWaypoints = Vector3.Distance (globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
		percentBetweenWaypoints += Time.deltaTime * moveSpeed / distanceBetweenWaypoints;
		percentBetweenWaypoints = Mathf.Clamp01 (percentBetweenWaypoints);
		float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);

		Vector3 newPos = Vector3.Lerp (globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

		if (percentBetweenWaypoints >= 1) {
			percentBetweenWaypoints = 0;
			fromWaypointIndex += platformDirection;

			if (!cyclic) {
				if (fromWaypointIndex >= globalWaypoints.Length-1 || fromWaypointIndex <= 0) {
					platformDirection *= -1;		// About-turn
				}
			}
			nextMoveTime = Time.time + waitTime;
		}
		return newPos - transform.position;
	}

	public void Move(Vector3 moveAmount) {
		transform.Translate (moveAmount);
	}


	void OnDrawGizmos() {
		if (localWaypoints != null) {
			Gizmos.color = Color.red;
			float size = .3f;

			for (int i =0; i < localWaypoints.Length; i ++) {
				Vector3 globalWaypointPos = (Application.isPlaying)?globalWaypoints[i] : localWaypoints[i] + transform.position;
				Gizmos.DrawLine(globalWaypointPos + Vector3.down * size, globalWaypointPos + Vector3.up * size);
				Gizmos.DrawLine(globalWaypointPos + Vector3.right * size, globalWaypointPos + Vector3.left * size);
			}
		}
	}

}
