using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_menu_control : MonoBehaviour 
{
	public GameObject texteMenu1;
	public GameObject logo;
	public GameObject panneauPause;
	public GameObject DropEcole;

	bool menuOuvert = false;
	int compteur = 0;



	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (compteur == 1) {
				SceneManager.LoadScene ("pré-Introduction (texte)");
		}
			if (compteur == 0) 
			{
				texteMenu1.SetActive (false);
				logo.SetActive (true);
				DropEcole.SetActive (true);
				compteur++;
			} 
		}

		else if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Time.timeScale = menuOuvert ? 1f : 0f;
			menuOuvert = !menuOuvert;
			panneauPause.SetActive(menuOuvert);
		}
	}
}
