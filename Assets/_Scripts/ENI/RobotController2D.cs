using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController2D : MonoBehaviour {

	private Rigidbody2D rb;
	[SerializeField] private float robotSpeed;
	//[SerializeField] private float chargeSpeed;

	//[SerializeField] private float chaseTime;

	private GameObject target;
	private GameObject player;
	private Vector2 direction = Vector2.zero;
	private Vector2 move;

	[SerializeField] private float stopTime;
	//private bool decoyActive;

	private float cooldown = 0;
	private float chase = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag ("Player");
		target = null;
		//decoyActive = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target == null)
			return;

		if (cooldown > stopTime /2) {
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
		move = direction * robotSpeed;

		rb.velocity = move;

		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Decoy")) {
			other.gameObject.SetActive (false);
			cooldown = stopTime;
		}
		if (other.gameObject.CompareTag ("Clear")) {
			this.gameObject.SetActive (false);
	}
	}

	void OnCollisionEnter2D (Collision2D other) {

		if (other.gameObject.CompareTag ("Clear")) {
			this.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("DestWall")) {
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("TriggerWall")) {
			other.gameObject.GetComponent<TriggerController>().DestroyWall();
		}
		}

	public void TargetAquire (GameObject newTarget) {
		this.target = newTarget;
	}

	void DecoyChase() {
		if (chase > 0)
			chase -= Time.deltaTime;
		else
			cooldown = stopTime;
	}

}
