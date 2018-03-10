using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed;
    public float frotements;
    private Rigidbody2D rb;
    private int count;

	[SerializeField] private int decoylvl = 1;

	[SerializeField] private GameObject decoySpawnlv1;
	[SerializeField] private GameObject decoySpawnlv2;
	private GameObject ennemy;
	private Vector2 move;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		ennemy = GameObject.FindGameObjectWithTag ("Dummy");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moverVertical = Input.GetAxisRaw("Vertical");

		move = new Vector2(moveHorizontal,moverVertical);
		move.Normalize ();
		rb.velocity = move * speed;

		if (Input.GetMouseButton(0))
		{
			// On demande a notre shoottrigger de lancer un projectile.
			//shooter.ShootProjectile();
			GameObject decoy = GameObject.FindGameObjectWithTag ("Decoy");
			if (decoy == null)
				switch (decoylvl){
				case 2:
					Instantiate (decoySpawnlv2, transform.position, transform.rotation);
					break;
				default:
					Instantiate (decoySpawnlv1, transform.position, transform.rotation);
					break;
			}
		}


    }

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Target")) {
			ennemy.SetActive(false);
			ennemy = GameObject.FindGameObjectWithTag ("Roboto");
			ennemy.GetComponent<RobotController2D>().TargetAquire (this.gameObject);
			ennemy.GetComponent<RobotController2D>().ChaseActive (true);
		}
		if (other.gameObject.CompareTag ("Collectable")) {
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("Safe")) {
			
			if (ennemy.gameObject.CompareTag ("Dummy"))
				ennemy.GetComponent<DummyController>().ChaseActive (false);
			else
				ennemy.GetComponent<RobotController2D>().ChaseActive (false);
		}
		if (other.gameObject.CompareTag ("Dummy End")) {
			if (ennemy.gameObject.CompareTag ("Dummy"))
				ennemy.GetComponent<DummyController> ().ChaseActive (false);
		}

	}
    
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Safe")) {
			if (ennemy.gameObject.CompareTag ("Dummy"))
				ennemy.GetComponent<DummyController> ().ChaseActive (true);
			else
				ennemy.GetComponent<RobotController2D> ().ChaseActive (true);
		}
		if (other.gameObject.CompareTag ("Dummy End")) {
			if (ennemy.gameObject.CompareTag ("Dummy"))
				ennemy.GetComponent<DummyController> ().ChaseActive (true);
		}
	}
}
