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

	private int count;
	private Vector2 directionalInput;

	private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
		if (Input.GetMouseButton (0)) {
			directionalInput = Input.mousePosition - Camera.main.WorldToScreenPoint (this.transform.position);
		} else {
			directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}
		directionalInput.Normalize ();

		rb.velocity = directionalInput * moveSpeed;

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


