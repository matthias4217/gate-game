using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Can be improved:
// ease with an exponential
// pet as child of player (thus no need for target)
public class PetController : MonoBehaviour
{
	public GameObject target;
	public float speed;
	public Vector2 offset = new Vector2 (2.5f, 1.0f);
	public float xOffset = 2.5f;
	public float yOffset = 1.0f;

    void Update()
    {
		if (this.transform.position.x <= target.transform.position.x)		// if the pet is on the left of the target
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
