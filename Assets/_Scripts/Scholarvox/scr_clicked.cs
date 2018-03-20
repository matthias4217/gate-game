using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_clicked : MonoBehaviour 
{
	public GameObject panelInfos;
	public GameObject livre1;
	public GameObject livre2;
	public GameObject livre3;
	public GameObject livre4;
	public GameObject recherche;
	public GameObject joueur;
	// Use this for initialization
	public void whenClicked1()
	{
		Debug.Log ("hadouken1");
		panelInfos.SetActive (true);
		livre1.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClicked2()
	{
		Debug.Log ("hadouken2");
		panelInfos.SetActive (true);
		livre2.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClicked3()
	{
		Debug.Log ("hadouken3");
		panelInfos.SetActive (true);
		livre3.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClicked4()
	{
		Debug.Log ("hadouken4");
		panelInfos.SetActive (true);
		livre3.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClickedClose()
	{
		Debug.Log ("hadoukenClose");
		livre1.SetActive (false);
		livre2.SetActive (false);
		livre3.SetActive (false);
		livre4.SetActive (false);
		recherche.SetActive (false);
		panelInfos.SetActive (false);
		joueur.GetComponent<scr_movement_player> ().carresActives = true;
	}

	public void whenClickedValidate()
	{
		Debug.Log ("hadoukenValidate");
	}
}
