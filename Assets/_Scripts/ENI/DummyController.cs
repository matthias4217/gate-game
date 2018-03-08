using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour {

	private Rigidbody2D rb;
	[SerializeField] private float target_speed = 1f;

	private GameObject target;
	private GameObject player;
	private Vector2 direction = Vector2.zero;
	private Vector2 move;
	private bool isActive;

	[SerializeField] private float stopTime;
	private float halfStopTime;
	private float cooldown = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag ("Player");
		target = player;
		isActive = true;
		halfStopTime = stopTime / 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isActive == false)
			return;

		if (cooldown > halfStopTime) {
			cooldown-= Time.deltaTime ;
			rb.velocity = move;
			return;
		}

		if (cooldown > 0) {
			rb.velocity = Vector2.zero;
			cooldown-= Time.deltaTime ;
			return;
		}


		GameObject decoy = GameObject.FindGameObjectWithTag ("Decoy");
		if (decoy != null)
			target = decoy;
		else
			target = player;

		float x = target.transform.position.x - this.transform.position.x;
		float y = target.transform.position.y - this.transform.position.y;
		direction = new Vector2 (x, y);
		direction.Normalize ();
		move = direction * target_speed;

		rb.velocity = move;


	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Decoy")) {
			other.gameObject.SetActive (false);
			cooldown = stopTime;
		}
	}

	void SetActive(bool boo) {
		isActive = boo;
	}

}
