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
    public GameObject player_Camera;

	private int count;
	private Vector2 directionalInput;
    private bool mouseMoving;

	private Rigidbody2D rb;
    private Camera cm;
    



    void Start()
    {
        mouseMoving = false;
        rb = GetComponent<Rigidbody2D>();
        cm = player_Camera.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
		if (Input.GetMouseButton (0)) { 
            mouseMoving = true;
			directionalInput = Input.mousePosition - cm.WorldToScreenPoint (this.transform.position);
            directionalInput.Normalize();
            rb.velocity = directionalInput * moveSpeed;
        } else {
            float test = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
            if (test != 0 || mouseMoving == false)
            {
                mouseMoving = false;
                directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                directionalInput.Normalize();
                rb.velocity = directionalInput * moveSpeed;
            }
        }

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


