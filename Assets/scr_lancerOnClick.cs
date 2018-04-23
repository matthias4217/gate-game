using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_lancerOnClick : MonoBehaviour 
{
	public GameObject bouton;
	public void lancerLeTemps()
	{
		Time.timeScale = 1.0f;
		bouton.SetActive (false);
	}
}
