using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public float moveSpeed;
    private int count;
	public GameObject tuto;
	[SerializeField] private int decoylvl = 1;

	[SerializeField] private GameObject decoylvl1;
	[SerializeField] private GameObject decoylvl2;
	private GameObject enemy;
	private Vector2 moveAmount;
	private Rigidbody2D rb;

	[SerializeField] private int health;

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
		{// If there is not already a decoy
			if (!GameObject.FindGameObjectWithTag ("Decoy")) {
				if (decoylvl == 1)
					Instantiate (decoylvl1, transform.position, transform.rotation);
				if (decoylvl == 2)
					Instantiate (decoylvl2, transform.position, transform.rotation);
							}
		}

    }

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Target")) {
			enemy.SetActive(false);
			enemy = GameObject.FindGameObjectWithTag ("Roboto");
			enemy.GetComponent<RobotController2D>().TargetAquire (this.gameObject);
			enemy.GetComponent<RobotController2D>().ChaseActive (true);
			other.gameObject.SetActive (false);
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
			if (enemy.gameObject.CompareTag ("Dummy")) {
				enemy.GetComponent<DummyController> ().ChaseActive (false);
				tuto.SetActive (false);
				}

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

	public void Damage (int damage)
	{
		health -= damage;
		if (health <= 0) {
			SceneManager.LoadScene ("ENI");
		}
	}

}
