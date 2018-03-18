using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlatformerController))]
public class PlatformerPlayer : MonoBehaviour {

	public int maxHealth = 3;			// The amount of hearts the player starts with

	public float moveSpeed = 10;
	public float maxJumpHeight = 6;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .35f;
	public float accelerationTimeAirborne = 0f;	// Amount of inertia while airborne (set to 0 for no inertia)
	public float accelerationTimeGrounded = 0f;	// Amount of inertia while grounded (set to 0 for no inertia)

	int currentHealth;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	PlatformerController controller;

	Vector2 directionalInput;

	void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
	}



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
		CalculateVelocity ();

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;		// To avoid "accumulating" gravity
		}
	}


	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
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

	public void Explode() {

	}



}
