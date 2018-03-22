using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_menu_control : MonoBehaviour 
{
	bool menuOuvert = false;
	int compteur = 0;
	public GameObject texteMenu1;
	public GameObject logo;
	public GameObject panneauPause;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (compteur == 1)
				SceneManager.LoadScene ("Introduction_Combat");
			
			if (compteur == 0) 
			{
				texteMenu1.SetActive (false);
				logo.SetActive (true);
				compteur += 1;
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (!menuOuvert) 
			{
				panneauPause.SetActive(true);
				Time.timeScale = 0.0f;
				menuOuvert = true;
			} 
			else 
			{
				panneauPause.SetActive(false);
				Time.timeScale = 1.0f;
				menuOuvert = false;
			}
		}
	}
}
