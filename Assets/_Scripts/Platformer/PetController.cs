using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{

	public GameObject target;
	public float speed;

    void Update()
    {
        if (this.transform.position.x <= target.transform.position.x)
        {
            Vector2 velocity = new Vector2(target.transform.position.x - this.transform.position.x - 2.5f, target.transform.position.y - this.transform.position.y + 1.0f) * speed;
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
        else
        {
            Vector2 velocity = new Vector2(target.transform.position.x - this.transform.position.x + 2.5f, target.transform.position.y - this.transform.position.y + 1.0f) * speed;
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}
