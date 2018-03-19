using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_Hub : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private int count;


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

<<<<<<< HEAD
    void OnTriggerEnter2D(Collider2D other)
=======
    void OnTriggerEnter2D (Collider2D other)
>>>>>>> f79887384a81c74e33fd84abbdc0ea368f6d2c68
    {
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


