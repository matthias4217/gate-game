using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyController2D : MonoBehaviour {

	private Rigidbody2D rb;

	[SerializeField] private float launchSpeed;
	[SerializeField] private float slow;
	[SerializeField] private float lifeSpan;

	private float speed;

	private Vector2 direction;
	private Vector2 mouse_position = Vector2.zero;
	private GameObject player;
	private Vector2 move;

	// Use this for initialization
	void Start () {
		// Setting up references
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag ("Player");

		speed = launchSpeed;

		mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = mouse_position - (Vector2) player.transform.position;

		direction.Normalize ();
		move = direction * speed;

	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = move;

		if (lifeSpan < 0)
			this.gameObject.SetActive (false);
		if (speed > 0)
			speed -= launchSpeed / slow;
		else
			speed = 0f;
		move = direction * speed;

		lifeSpan -= Time.deltaTime;
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Wall")) {
			Obstacle ();
		}
		if (other.gameObject.CompareTag ("DestWall")) {
			Obstacle ();
		}
		if (other.gameObject.CompareTag ("TriggerWall")) {
			Obstacle ();
		}
	}

	public void Obstacle() {
		speed = 0f;
	}
}
