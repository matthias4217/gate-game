using UnityEngine;
using System.Collections;

public class PlatformerController : RaycastController {
	
	public float maxSlopeAngle = 80f;		// degree

	public CollisionInfo collisions;
	[HideInInspector]
	public Vector2 playerInput;

	public override void Start() {
		base.Start ();
		collisions.faceDir = 1;

	}

	public void Move(Vector2 moveAmount, bool standingOnPlatform) {		// Called when not related to some inputs
		Move (moveAmount, Vector2.zero, standingOnPlatform);
	}

	public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform=false) {
		UpdateRaycastOrigins ();

		collisions.Reset ();
		collisions.moveAmountOld = moveAmount;
		playerInput = input;

		if (moveAmount.y < 0) {
			DescendSlope(ref moveAmount);
		}

		if (moveAmount.x != 0) {
			collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
		}

		HorizontalCollisions (ref moveAmount);
		if (moveAmount.y != 0) {
			VerticalCollisions (ref moveAmount);
		}

		transform.Translate (moveAmount);

		if (standingOnPlatform) {
			collisions.below = true;
		}
	}

	void HorizontalCollisions(ref Vector2 moveAmount) {
		float directionX = collisions.faceDir;
		float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;		// The more we are moving, the longer the rays are

		if (Mathf.Abs(moveAmount.x) < skinWidth) {
			rayLength = 2*skinWidth;
		}

		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

			if (hit) {

				if (hit.distance == 0) {	// If we're actually IN an obstacle; @@@ BTW has to be changed in order to crush the character.
					continue;
				}

				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

				if (i == 0 && slopeAngle <= maxSlopeAngle) {
					if (collisions.descendingSlope) {
						collisions.descendingSlope = false;
						moveAmount = collisions.moveAmountOld;
					}
					float distanceToSlopeStart = 0;
					if (slopeAngle != collisions.slopeAngleOld) {
						distanceToSlopeStart = hit.distance - skinWidth;
						moveAmount.x -= distanceToSlopeStart * directionX;
					}
					ClimbSlope(ref moveAmount, slopeAngle, hit.normal);
					moveAmount.x += distanceToSlopeStart * directionX;
				}

				if (!collisions.climbingSlope || slopeAngle > maxSlopeAngle) {
					moveAmount.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;		// Reducing the lenght of the next rays casted to avoid collisions further than this one

					if (collisions.climbingSlope) {
						moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
					}

					collisions.left = (directionX == -1);
					collisions.right = (directionX == 1);
				}
			}
		}
	}

	void VerticalCollisions(ref Vector2 moveAmount) {
		float directionY = Mathf.Sign(moveAmount.y);
		float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;		// The more we are moving, the longer the rays are

		for (int i = 0; i < verticalRayCount; i++) {

			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

			if (hit) {
				// IMPORTANT NOTE: Do not make slopes traversable because it is not well handled and it's useless anyway
				if (hit.collider.tag == "Traversable") {
					if (directionY == 1 || hit.distance == 0) {		// If we're jumping through it
						continue;
					}
					if (collisions.fallingThroughPlatform) {
						continue;
					}
					if (playerInput.y == -1) {
						collisions.fallingThroughPlatform = true;
						Invoke("ResetFallingThroughPlatform", .1f);		// Set here the amount of time we go through platforms
						continue;
					}
				} else if (hit.collider.tag == "Friable") {
					print (hit.point);
					StartCoroutine (hit.collider.gameObject.GetComponent<FriableTile> ().effrite ((int)hit.point.x, (int) hit.point.y - 1));
				}

				moveAmount.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;	// Reducing the lenght of the next rays casted to avoid collisions further than this one

				if (collisions.climbingSlope) {
					moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
				}

				collisions.below = (directionY == -1);
				collisions.above = (directionY == 1);
			}
		}

		if (collisions.climbingSlope) {
			float directionX = Mathf.Sign(moveAmount.x);
			rayLength = Mathf.Abs(moveAmount.x) + skinWidth;		// The more we are moving, the longer the rays are

			Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			if (hit) {
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if (slopeAngle != collisions.slopeAngle) {
					moveAmount.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
					collisions.slopeNormal = hit.normal;
				}
			}
		}
	}

	void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal) {

		/* Actually just simple trigonometry */
		float moveDistance = Mathf.Abs(moveAmount.x);
		float climbMoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (moveAmount.y <= climbMoveAmountY) {		// If not jumping on slope
			moveAmount.y = climbMoveAmountY;
			moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
			collisions.below = true;
			collisions.climbingSlope = true;
			collisions.slopeAngle = slopeAngle;
			collisions.slopeNormal = slopeNormal;
		}
	}

	void DescendSlope(ref Vector2 moveAmount) {
		RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast (raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);
		RaycastHit2D maxSlopeHitRight = Physics2D.Raycast (raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask);


		float directionX = Mathf.Sign(moveAmount.x);
		Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

		if (hit) {
			float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxSlopeAngle) {
				if (Mathf.Sign(hit.normal.x) == directionX) {
					if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x)) {
						float moveDistance = Mathf.Abs(moveAmount.x);
						float descendMoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
						moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
						moveAmount.y -= descendMoveAmountY;

						collisions.slopeAngle = slopeAngle;
						collisions.descendingSlope = true;
						collisions.below = true;
						collisions.slopeNormal = hit.normal;
					}
				}
			}
		}

	}

	void ResetFallingThroughPlatform() {
		collisions.fallingThroughPlatform = false;
	}


	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

		public bool climbingSlope;
		public bool descendingSlope;

		public float slopeAngle, slopeAngleOld;
		public Vector2 slopeNormal;
		public Vector2 moveAmountOld;
		public int faceDir;		// -1->left; 1->right
		public bool fallingThroughPlatform;

		public void Reset() {
			above = below = false;
			left = right = false;
			climbingSlope = false;
			descendingSlope = false;
			slopeNormal = Vector2.zero;

			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}

}
