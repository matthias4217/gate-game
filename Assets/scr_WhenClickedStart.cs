using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_WhenClickedStart : MonoBehaviour {

	public GameObject texte;
	public void WhenClickedStart()
	{
		texte.SetActive (false);
	}
}
