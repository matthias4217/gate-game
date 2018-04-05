using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour {

	public GameObject texteTuto;
	private Rigidbody2D rb;
	[SerializeField] private float chaseSpeed = 1f;
	[SerializeField] private float decoySpeed = 1f;
	public float speed;

	private GameObject target;
	private GameObject player;
	private Vector2 direction = Vector2.zero;
	public Vector2 move;
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
		speed = chaseSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isActive == false) {
			rb.velocity = Vector2.zero;
			return;
		}
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
		if (decoy != null) {
			target = decoy;
			speed = decoySpeed;
		}
		else{
			target = player;
			speed = chaseSpeed;}
		float x = target.transform.position.x - this.transform.position.x;
		float y = target.transform.position.y - this.transform.position.y;
		direction = new Vector2 (x, y);
		direction.Normalize ();
		move = direction * speed;

		rb.velocity = move;


	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Decoy")) {
			other.gameObject.SetActive (false);
			cooldown = stopTime;
		}
	}

	public void ChaseActive(bool boo) 
	{
		isActive = boo;
	}

}
