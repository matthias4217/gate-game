﻿using System.Collections;
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
    private Vector2 mousePos;
    



    void Start()
    {
        mouseMoving = false;
        rb = GetComponent<Rigidbody2D>();
        cm = player_Camera.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector2 pos = this.transform.position;
        if (Input.GetMouseButton (0)) {
            mousePos = cm.ScreenToWorldPoint(Input.mousePosition);
            mouseMoving = true;
			directionalInput = cm.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            directionalInput.Normalize();
            rb.velocity = directionalInput * moveSpeed;
        } else {
            float test = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
            if (test != 0 || mouseMoving == false || ((Mathf.Abs(mousePos.x - pos.x) < 0.05 & Mathf.Abs(mousePos.y - pos.y) < 0.05)))
            {
                mouseMoving = false;
                directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                directionalInput.Normalize();
                rb.velocity = directionalInput * moveSpeed; 
            }
        }
    }

	
	void OnTriggerStay2D(Collider2D other) {
        string tag = other.gameObject.tag;
    if (Input.GetKeyDown("space")) {
            SceneManager.LoadScene(tag);
        }        
    }
}


