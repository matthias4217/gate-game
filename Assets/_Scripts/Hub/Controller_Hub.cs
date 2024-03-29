﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Controller_Hub : MonoBehaviour
{
    public GameObject texteENI;
    public GameObject texteSCHOL;
    public GameObject texteEURO;
    public GameObject prefab;
    public GameObject texteSpawn;
    public float moveSpeed;
    public float timeBeforeMove;
    public GameObject Scholwarning;
    public GameObject Eurowarning;
    public GameObject ENIwarning;
    public GameObject player_Camera;
    public bool allTheWay;
    public float minMove;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float waitBeforeText;

    private int count;
    private Vector2 directionalInput;
    private bool mouseMoving;

    private Rigidbody2D rb;
    private Camera cm;
    private Vector2 mousePos;
    private float timer;
    private GameObject Obj;



    void Start()
    {
        Obj = null;
        timer = timeBeforeMove * 2 + 1;
        mouseMoving = false;
        rb = GetComponent<Rigidbody2D>();
        cm = player_Camera.GetComponent<Camera>();
        StartCoroutine(StopText(waitBeforeText));
    }

    void FixedUpdate()
    {
        StartCoroutine(StopText(waitBeforeText));
        Vector2 pos = this.transform.position;
        if (Input.GetMouseButtonDown(0) && timer > timeBeforeMove)
        {
            if ((Mathf.Abs(cm.ScreenToWorldPoint(Input.mousePosition).x - pos.x) > minMove || Mathf.Abs(cm.ScreenToWorldPoint(Input.mousePosition).y - pos.y) > minMove) && (!allTheWay || !mouseMoving) || (pos.x > 5.9 || pos.x < -7.4 || Mathf.Abs(pos.y) > 4.6))
            {

                mousePos = cm.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(mousePos);
                Vector3 objPos = Tronquer(mousePos, maxX, minX, maxY, minY);
                Destroy(Obj);
                Obj = Instantiate(prefab, objPos, new Quaternion(0, 0, 0, 0));
                mouseMoving = true;
                if (allTheWay) { timer = 2 * timeBeforeMove + 1; } else { timer = 0f; }
                directionalInput = cm.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
                directionalInput.Normalize();
                rb.velocity = directionalInput * moveSpeed;
            }
        }
        else
        {
            float test = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
            if (test != 0 || mouseMoving == false || ((Mathf.Abs(mousePos.x - pos.x) < 0.05 && Mathf.Abs(mousePos.y - pos.y) < 0.05)))
            {
                Destroy(Obj);

                mouseMoving = false;
                timer = timeBeforeMove * 2 + 1;
                directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                directionalInput.Normalize();
                rb.velocity = directionalInput * moveSpeed;
            }
        }
        if (mouseMoving)
            timer += Time.deltaTime;


    }

    void OnTriggerExit2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Scholarvox":
                Scholwarning.SetActive(false);
                texteSCHOL.SetActive(false);
                break;
            case "ENI":
                ENIwarning.SetActive(false);
                texteENI.SetActive(false);
                break;
            case "Europresse":
                Eurowarning.SetActive(false);
                texteEURO.SetActive(false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        string tag = other.gameObject.tag;;
        Debug.Log("name : " + other.name +"  tag : "+ other.tag +"  gameObject : "+ other.gameObject);
        switch (tag)
        {
            case "Target":
                Destroy(other.gameObject);
                break;
            case "Scholarvox":
                Scholwarning.SetActive(true);
                texteSCHOL.SetActive(true);
                break;
            case "ENI":
                ENIwarning.SetActive(true);
                texteENI.SetActive(true);
                break;
            case "Europresse":
                Eurowarning.SetActive(true);
                texteEURO.SetActive(true);
                break;

        }
    }

    void OnTriggerStay2D(Collider2D other) {

        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Hadoken");
            SceneManager.LoadScene(other.gameObject.tag);
        }
    }

    Vector3 Tronquer(Vector2 pos, float maxX, float minX, float maxY, float minY)
    {
        float tempx = pos.x;
        float tempy = pos.y;

        if (tempx > maxX) { tempx = maxX; }
        else if (tempx < minX) { tempx = minX; }

        if (tempy > maxY) { tempy = maxY; }
        else if (tempy < minY) { tempy = minY; }

        return new Vector3(tempx, tempy, 0f);
    }

    IEnumerator StopText(float wait) {
        yield return new WaitForSeconds(wait);
        texteSpawn.SetActive(false);
    }

    /*IEnumerator LoadScene(string tag) {
        Debug.Log("hadoken");
        while (true)
        {
            Debug.Log("hadoken");
            if (Input.GetKeyDown(KeyCode.Space))
            {

                SceneManager.LoadScene(tag);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}



