﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Hub");
    }
}
