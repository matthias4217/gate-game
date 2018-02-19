using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerPlatformController : RaycastController {

	public LayerMask passengerMask;

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

	List<PassengerMovement> passengerMovement;
	Dictionary<Transform, PlatformerController> passengerDictionary = new Dictionary<Transform, PlatformerController>();
	
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

		CalculatePassengerMovement(moveAmount);

		MovePassengers (true);
		transform.Translate (moveAmount);
		MovePassengers (false);
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

	void MovePassengers(bool beforeMovePlatform) {
		foreach (PassengerMovement passenger in passengerMovement) {
			if (!passengerDictionary.ContainsKey(passenger.transform)) {
				passengerDictionary.Add(passenger.transform,passenger.transform.GetComponent<PlatformerController>());
			}

			if (passenger.moveBeforePlatform == beforeMovePlatform) {
				passengerDictionary[passenger.transform].Move(passenger.moveAmount, passenger.standingOnPlatform);
			}
		}
	}

	void CalculatePassengerMovement(Vector3 moveAmount) {
		HashSet<Transform> movedPassengers = new HashSet<Transform> ();
		passengerMovement = new List<PassengerMovement> ();

		float directionX = Mathf.Sign(moveAmount.x);
		float directionY = Mathf.Sign(moveAmount.y);

		// Vertically moving platform
		if (moveAmount.y != 0) {
			float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i++) {
				Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

				if (hit && hit.distance != 0) {		// If there is a passenger (hit.distance != 0 to ignore objects through)
					if (!movedPassengers.Contains(hit.transform)) {		// In order to move each passenger only once
						movedPassengers.Add(hit.transform);
						float pushX = (directionY == 1) ? moveAmount.x : 0;
						float pushY = moveAmount.y - (hit.distance - skinWidth) * directionY;

						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), directionY==1, true));
					}
				}
			}
		}

		// Horizontally moving platform
		if (moveAmount.x != 0) {
			float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
			
			for (int i = 0; i < horizontalRayCount; i ++) {
				Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

				if (hit && hit.distance != 0) {		// If there is a passenger (hit.distance != 0 to ignore objects through)
					if (!movedPassengers.Contains(hit.transform)) {		// In order to move each passenger only once
						movedPassengers.Add(hit.transform);
						float pushX = moveAmount.x - (hit.distance - skinWidth) * directionX;
						float pushY = -skinWidth;		// Trick to be able to check collision below, thus enabling jump
						
						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), false, true));
					}
				}
			}
		}

		// Passenger on top of a horizontally or downward moving platform
		if (directionY == -1 || moveAmount.y == 0 && moveAmount.x != 0) {
			float rayLength = skinWidth * 2;
			
			for (int i = 0; i < verticalRayCount; i++) {
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				
				if (hit && hit.distance != 0) {		// If there is a passenger (hit.distance != 0 to ignore objects through)
					if (!movedPassengers.Contains(hit.transform)) {		// In order to move each passenger only once
						movedPassengers.Add(hit.transform);
						float pushX = moveAmount.x;
						float pushY = moveAmount.y;
						
						passengerMovement.Add(new PassengerMovement(hit.transform, new Vector3(pushX,pushY), true, false));
					}
				}
			}
		}
	}


	struct PassengerMovement {
		public Transform transform;
		public Vector3 moveAmount;
		public bool standingOnPlatform;
		public bool moveBeforePlatform;

		public PassengerMovement(Transform _transform, Vector3 _moveAmount, bool _standingOnPlatform, bool _moveBeforePlatform) {
			transform = _transform;
			moveAmount = _moveAmount;
			standingOnPlatform = _standingOnPlatform;
			moveBeforePlatform = _moveBeforePlatform;
		}
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
