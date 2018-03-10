using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float moveSpeed;
    private int count;

	[SerializeField] private int decoylvl = 1;

	[SerializeField] private GameObject decoy;
	private GameObject enemy;
	private Vector2 moveAmount;
	private Rigidbody2D rb;

    void Start()
    {
        // Settings up references
		rb = GetComponent<Rigidbody2D>();
		enemy = GameObject.FindGameObjectWithTag ("Dummy");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		moveAmount = new Vector2(moveHorizontal, moveVertical);
		moveAmount.Normalize ();
		rb.velocity = moveAmount * moveSpeed;

		if (Input.GetMouseButton(0))
		{
			if (!GameObject.FindGameObjectWithTag ("Decoy")) {		// If there is not already a decoy
				Instantiate (decoy, transform.position, transform.rotation);
				decoy.GetComponent<DecoyController> ().launchSpeed = 0;
			}
		}

    }

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Target")) {
			enemy.SetActive(false);
			enemy = GameObject.FindGameObjectWithTag ("Roboto");
			enemy.GetComponent<RobotController2D>().TargetAquire (this.gameObject);
			enemy.GetComponent<RobotController2D>().ChaseActive (true);
		}
		if (other.gameObject.CompareTag ("Collectable")) {
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("Safe")) {
			
			if (enemy.gameObject.CompareTag ("Dummy"))
				enemy.GetComponent<DummyController>().ChaseActive (false);
			else
				enemy.GetComponent<RobotController2D>().ChaseActive (false);
		}
		if (other.gameObject.CompareTag ("Dummy End")) {
			if (enemy.gameObject.CompareTag ("Dummy"))
				enemy.GetComponent<DummyController> ().ChaseActive (false);
		}

	}
    
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Safe")) {
			if (enemy.gameObject.CompareTag ("Dummy"))
				enemy.GetComponent<DummyController> ().ChaseActive (true);
			else
				enemy.GetComponent<RobotController2D> ().ChaseActive (true);
		}
		if (other.gameObject.CompareTag ("Dummy End")) {
			if (enemy.gameObject.CompareTag ("Dummy"))
				enemy.GetComponent<DummyController> ().ChaseActive (true);
		}
	}
}
