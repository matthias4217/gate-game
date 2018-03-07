using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
	public GameObject target;
	public float speed;
	public float xOffset = 2.5f;
	public float yOffset = 1.0f;

    void Update()
    {
		Vector3 velocity = (this.transform.position + target.transform.position) * speed + yOffset*Vector3.up;
		velocity += Vector3.right * xOffset * Mathf.Sign (this.transform.position.x - target.transform.position.x);
		GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
