using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_clicked_menu : MonoBehaviour 
{
	public void whenClickedBoutonExit()
	{
		Application.Quit ();
	}

	public void whenClickedBoutonHub()
	{
		SceneManager.LoadScene ("Hub");
		Time.timeScale = 1.0f;
	}
}