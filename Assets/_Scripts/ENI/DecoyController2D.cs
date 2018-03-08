using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyController2D : MonoBehaviour {

	private Rigidbody2D rb;

	[SerializeField] private float launchSpeed;
	[SerializeField] private float slow;
	[SerializeField] private float lifeSpan;

	private float speed;
	private float inv_slow;

	private Vector2 direction;
	private Vector2 mouse_position = Vector2.zero;
	private GameObject player;
	private Vector2 move;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		speed = launchSpeed;
		inv_slow = 1f / slow;

		player = GameObject.FindGameObjectWithTag ("Player");
		mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = mouse_position - new Vector2(player.transform.position.x, player.transform.position.y);

		direction.Normalize ();
		move = direction * speed;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = move;

		if (lifeSpan < 0)
			this.gameObject.SetActive (false);
		if (speed > 0)
			speed -= launchSpeed * inv_slow;
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
