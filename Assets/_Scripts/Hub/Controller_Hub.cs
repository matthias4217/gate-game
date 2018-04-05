using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_Hub : MonoBehaviour
{
    public float moveSpeed;

    public GameObject Scholwarning;
	public GameObject Eurowarning;
	public GameObject ENIwarning;
	public GameObject spawn;

    public GameObject Camera;
    private Camera cam;

	private int count;
	private Vector2 directionalInput;
    private Vector2 mousePos;
    private Vector2 camPos;
    private Rigidbody2D rb;

    private bool mouseactive;
    private Vector2 lm ;

    void Start()
    {
        mousePos = new Vector2(0, 0);
        mouseactive = false;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.GetComponent<Camera>();
        Vector2 camPos = this.transform.position;
        Debug.Log(camPos);
    }

    void FixedUpdate()
    {
        Vector2 camPos = this.transform.position;
        float a1 = Input.GetAxisRaw("Horizontal");
        float a2 = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0)) {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            //mousePos = Input.mousePosition;
            directionalInput = mousePos - camPos;
            Debug.Log(mousePos);
            mouseactive = true;

        }
        else if (Mathf.Abs(a1) + Mathf.Abs(a2) != 0)
        {
            Debug.Log("Keyboard");
			directionalInput = new Vector2 (a1, a2);
            directionalInput.Normalize();
            Debug.Log(directionalInput);
            rb.velocity = directionalInput * moveSpeed;
        }
        else if(!mouseactive)
        {
            rb.velocity = new Vector2(0, 0);
        }

         

        /*directionalInput = mousePos - camPos;
        if (directionalInput.magnitude < 0.01)
            {
            rb.velocity = new Vector2(0, 0);
            }
            */

    }
	void OnTriggerExit2D(Collider2D other)
	{
		string tag = other.gameObject.tag;
		switch (tag) {
		case "Respawn":
			spawn.SetActive (false);
			break;
		case "Scholwarning":
			Scholwarning.SetActive (false);
			break;
		case "Eurowarning":
			Eurowarning.SetActive (false);
			break;
		case "ENIwarning":
			ENIwarning.SetActive (false);
			break;
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
        string tag = other.gameObject.tag;
        switch (tag)
            {
		case "Scholwarning":
			Scholwarning.SetActive (true);
			break;
		case "Eurowarning":
			Eurowarning.SetActive (true);
			break;
		case "ENIwarning":
			ENIwarning.SetActive (true);
			break;
			
			
            case "Scholarvox":
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


