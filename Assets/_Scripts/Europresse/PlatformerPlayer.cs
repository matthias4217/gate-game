using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (PlatformerController))]
public class PlatformerPlayer : MonoBehaviour {

	[Tooltip("The amount of hearts the player starts with")]
	public int maxHealth = 3;
	[Space(10)]
	public float moveSpeed = 10;
	public float maxJumpHeight = 6;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .35f;
	[Tooltip("Amount of inertia while airborne (set to 0 for no inertia)")]
	public float accelerationTimeAirborne = 0f;
	[Tooltip("Amount of inertia while grounded (set to 0 for no inertia)")]
	public float accelerationTimeGrounded = 0f;
	[Space(10)]
	[Tooltip("The force applied to the player when they hit an enemy")]
	public Vector2 knockback;
	[Tooltip("The amount of time the player is invincible after taking a hit")]
	public float invicibilityTimeAfterHit;
	[Tooltip("The frequency at which the player flashes when they are invicible")]
	public float flashFrequency;

	int currentHealth;
	bool canMove;
	bool hitThisFrame;
	bool hit;
	bool invicible;
	bool diedThisFrame;
	Vector3 lastCheckpoint;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	Vector2 directionalInput;

	PlatformerController controller;
	GameObject healthBar;



	void Start() {
		controller = GetComponent<PlatformerController> ();
		healthBar = GameObject.FindWithTag ("Health Bar");

		currentHealth = maxHealth;
		canMove = true;

		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex,2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}

	void Update() {
		
		if (diedThisFrame) {
			StartCoroutine(PlayerDeath ());
			diedThisFrame = false;
		}

		CalculateVelocity ();
	
		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.below) {
			hit = true;
		}
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;		// To avoid "accumulating" gravity
		}
	}


	void CalculateVelocity() {
		if (hitThisFrame) {
			velocity.x = -controller.collisions.faceDir * knockback.x;
			velocity.y = knockback.y;
			hitThisFrame = false;
		}
		if (hit) {
			float targetVelocityX = canMove ? directionalInput.x * moveSpeed : 0f;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		}

		velocity.y += gravity * Time.deltaTime;
	}

	public void OnJumpInputDown() {
		if (controller.collisions.below && canMove) {
			velocity.y = maxJumpVelocity;
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void SetCheckpoint (Vector3 checkpointPosition) {
		lastCheckpoint = checkpointPosition;
	}


	void OnTriggerStay2D(Collider2D other) {

		if (other.CompareTag("Enemy") && !invicible) {
			Debug.Log ("Hit");
			currentHealth--;
			if (currentHealth <= 0) {
				diedThisFrame = true;
			}
			hitThisFrame = true;
			hit = false;
			invicible = true;
			StartCoroutine (InvicibilityAfterHit (invicibilityTimeAfterHit));
			StartCoroutine (PlayerFlash(flashFrequency));
			UpdateHealthBar ();

		} else if (other.CompareTag("Fall Trigger")) {
			StartCoroutine(PlayerDeath ());
		}
	}


	public IEnumerator PlayerDeath() {
		hit = false;
		velocity = Vector3.zero;
		Explode ();

		// Fade out
		float transitionTime = 1f;		// The amount of time required to reach full opacity

		RawImage screenFilter = FindObjectOfType<RawImage>();
		while (screenFilter.color.a < 1f) {		// while the image is not opaque
			Color tmpColor = screenFilter.color;
			tmpColor.a += Time.deltaTime / transitionTime;
			screenFilter.color = tmpColor;
			yield return null;
		}

		// Respawn
		gameObject.transform.position = lastCheckpoint;
		currentHealth = maxHealth;
		GetComponent<SpriteRenderer> ().enabled = true;		// Reenabling the rendering of the sprite
		UpdateHealthBar ();
		velocity = Vector3.zero;
		hit = false;
		yield return new WaitForSeconds (3f);

		while (screenFilter.color.a > 0f) {		// while the image is there
			Color tmpColor = screenFilter.color;
			tmpColor.a -= Time.deltaTime / transitionTime;
			screenFilter.color = tmpColor;
			yield return null;
		}


	}

	public void Explode() {
		GetComponent<SpriteRenderer> ().enabled = false;		// Disabling the rendering of the player
		//
		print ("Boom");
	}

	public void UpdateHealthBar () {
		for (int i = 1; i <= Mathf.Min(3, currentHealth); i++) {
			SpriteRenderer heartI = healthBar.transform.Find ("Heart " + i).GetComponent<SpriteRenderer> ();
			heartI.color = Color.red;
		}
		for (int i = currentHealth + 1; i <= Mathf.Min(3, maxHealth); i++) {
			SpriteRenderer heartI = healthBar.transform.Find ("Heart " + i).GetComponent<SpriteRenderer> ();
			heartI.color = Color.white;
		}
	}



	private IEnumerator InvicibilityAfterHit(float invicibilityTime) {
		yield return new WaitForSeconds (invicibilityTime);
		Debug.Log ("Invincibility finished");
		invicible = false;
	}

	private IEnumerator PlayerFlash (float flashFrequency) {
		float delta = 1 / (2 * flashFrequency);
		Color playerColor = GetComponent<SpriteRenderer> ().color;
		while (invicible) {
			yield return new WaitForSeconds (delta);
			playerColor.a = 1 - playerColor.a;		// Switching the visibility of the sprite
			GetComponent<SpriteRenderer> ().color = playerColor;
		}
		// Resetting the sprite visible
		playerColor.a = 1;
		GetComponent<SpriteRenderer> ().color = playerColor;
	}
		



	/* // not enough time to use this elegent implementation
	public struct HitInfo {
		public bool hitThisFrame;
		public bool hit;				// true while the player is in a knockback state: the player is unable to move until the ground is reached
		public Vector2 knockback;		// The knockback applied to the player when they hit an enemy
		public bool invincible;			// The player can't get hit while invicible

		public void Reset() {
			hitThisFrame = false;
			hit = false;
			invincible = false;
		}
	}
	*/
}
