using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyController : MonoBehaviour {

	private Rigidbody2D rb;

	[SerializeField] public float launchSpeed;
	[SerializeField] private float slow;
	[SerializeField] private float lifeSpan;

	private float velocity;

	private Vector2 direction;
	private GameObject player;
	private Vector2 move;

	// Use this for initialization
	void Start () {
		// Setting up references
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag ("Player");


		velocity = launchSpeed;
		Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = mouse_position - (Vector2)player.transform.position;

		direction.Normalize ();
		move = direction * velocity;

	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = move;

		if (lifeSpan < 0)
			this.gameObject.SetActive (false);
		if (velocity > 0)
			velocity -= launchSpeed / slow;
		else
			velocity = 0f;
		move = direction * velocity;

		lifeSpan -= Time.deltaTime;
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Wall")) {
			Stop ();
		}
		if (other.gameObject.CompareTag ("DestWall")) {
			Stop ();
		}
		if (other.gameObject.CompareTag ("TriggerWall")) {
			Stop ();
		}
	}

	public void Stop() {
		velocity = 0f;
	}
}
