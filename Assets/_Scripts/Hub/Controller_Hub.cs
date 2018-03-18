using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_Hub : MonoBehaviour
{
    public float speed;
    public float frotements;
    private Rigidbody2D rb;
    private int count;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moverVertical = Input.GetAxisRaw("Vertical");

		if (Mathf.Abs(moveHorizontal) + Mathf.Abs(moverVertical) == 2)
			rb.velocity = new Vector2(moveHorizontal * speed / Mathf.Sqrt(2),moverVertical * speed / Mathf.Sqrt(2));
		else rb.velocity = new Vector2(moveHorizontal * speed ,moverVertical * speed);


    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
            {
            case "scholavrox":
                SceneManager.LoadScene("Scholarvox");
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


