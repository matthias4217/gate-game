using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlatformerController))]
public class PlatformerPlayer : MonoBehaviour {

	public int maxHealth = 300;			// The amount of hearts the player starts with

	public float moveSpeed = 10;
	public float maxJumpHeight = 6;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .35f;
	public float accelerationTimeAirborne = 0f;	// Amount of inertia while airborne (set to 0 for no inertia)
	public float accelerationTimeGrounded = 0f;	// Amount of inertia while grounded (set to 0 for no inertia)

	public float knockbackX;
	public float knockbackY;

	int currentHealth;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	bool hitThisFrame;
	bool isHit;			// true while the player is in a knockback state: the player is unable to move until the ground is reached

	PlatformerController controller;

	Vector2 directionalInput;

	void Start() {
		controller = GetComponent<PlatformerController> ();

		currentHealth = maxHealth;

		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex,2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}

	void Update() {
		if (currentHealth <= 0) {	
			Explode ();
			gameObject.SetActive (false);
		}

		if (hitThisFrame) {
			velocity.x = -controller.collisions.faceDir * knockbackX;
			velocity.y = knockbackY;
			hitThisFrame = false;
		}

		CalculateVelocity ();
	
		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.below) {
			isHit = false;
		}
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;		// To avoid "accumulating" gravity
		}
	}


	void CalculateVelocity() {
		if (!isHit) {
			float targetVelocityX = directionalInput.x * moveSpeed;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		}
		velocity.y += gravity * Time.deltaTime;
	}
		
	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (controller.collisions.below) {
			velocity.y = maxJumpVelocity;
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("hadoken 6");
		currentHealth--;
		hitThisFrame = true;
		isHit = true;
	}

	public void Explode() {

	}



}
