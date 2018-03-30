using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be improved:
// ease with an exponential

[RequireComponent(typeof(Rigidbody2D))]
public class PetController : MonoBehaviour
{
	public float speed;
	public Vector2 offset;

	Rigidbody2D rb;



	void Start() {
		rb = GetComponent<Rigidbody2D> ();

	}

	void Update() {

		if (this.transform.localPosition.x <= 0f) {		// if the pet is on the left of the target
			rb.velocity = new Vector2(-offset.x - this.transform.localPosition.x, offset.y - this.transform.localPosition.y) * speed;;
		} else {
			rb.velocity = new Vector2(offset.x - this.transform.localPosition.x, offset.y - this.transform.localPosition.y) * speed;
		}

	}


}


