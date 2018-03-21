using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_Hub : MonoBehaviour
{
    public float moveSpeed;
    
	private int count;

	private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Vector3 moveAmount = new Vector3(moveHorizontal, moveVertical, 0);
		moveAmount.Normalize ();
		rb.velocity = moveAmount * moveSpeed;

    }

	void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("prout");
        string tag = other.gameObject.tag;
        Debug.Log(tag);
        switch (tag)
            {
            case "Scholarvox":
                SceneManager.LoadScene(0);
                break;
            case "ENI":
                SceneManager.LoadScene("ENI");
                break;
            case "Europresse":
                SceneManager.LoadScene("Europresse");
                break;
            case "Statista":
                SceneManager.LoadScene("Statista");
                break;
            default:
                break;
            }           
        }
    }


