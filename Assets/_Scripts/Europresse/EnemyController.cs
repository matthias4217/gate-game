using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : RaycastController {

	public Vector3[] localWaypoints;
	Vector3[] globalWaypoints;		// Waypoints but with global coordinates

	public float speed;			// The speed of the platform
	public bool cyclic;			// Is the movement cyclic or does the platform goes back and forth
	public float waitTime;		// Time waited at a waypoint
	[Range(0,3)]
	public float easeAmount;

	int fromWaypointIndex;
	float percentBetweenWaypoints;
	float nextMoveTime;
	int platformDirection = 1;		// Indicates if the platform is moving in forward or backward in its path (in a non cyclical movement)

	public override void Start () {
		base.Start ();

		// Initializing the globalWaypoints array
		globalWaypoints = new Vector3[localWaypoints.Length];
		for (int i = 0; i < localWaypoints.Length; i++) {
			globalWaypoints[i] = transform.position + localWaypoints[i];
		}
	}

	void Update () {
		UpdateRaycastOrigins ();
		Vector3 moveAmount = CalculatePlatformMovement();
		transform.Translate (moveAmount);
	}

	float Ease(float x) {
		float a = easeAmount + 1;
		return Mathf.Pow(x,a) / (Mathf.Pow(x,a) + Mathf.Pow(1-x, a));
	}

	Vector3 CalculatePlatformMovement() {

		if (Time.time < nextMoveTime) {
			return Vector3.zero;
		}

		fromWaypointIndex %= globalWaypoints.Length;
		int toWaypointIndex = (fromWaypointIndex + platformDirection) % globalWaypoints.Length;
		float distanceBetweenWaypoints = Vector3.Distance (globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
		percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;
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
