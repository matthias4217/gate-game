using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_clicked : MonoBehaviour 
{
	public int numeroChoix;

	public GameObject panelInfos;
	public GameObject livre1;
	public GameObject livre2;
	public GameObject livre3;
	public GameObject livre4;
	public GameObject recherche;
	public GameObject joueur;
	public Dropdown GUIBouton;
	// Use this for initialization
	public void whenClicked1()
	{
		panelInfos.SetActive (true);
		livre1.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClicked2()
	{
		panelInfos.SetActive (true);
		livre2.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClicked3()
	{
		panelInfos.SetActive (true);
		livre3.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClicked4()
	{
		panelInfos.SetActive (true);
		livre4.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().carresActives = false;
	}

	public void whenClickedClose()
	{
		livre1.SetActive (false);
		livre2.SetActive (false);
		livre3.SetActive (false);
		livre4.SetActive (false);
		recherche.SetActive (false);
		panelInfos.SetActive (false);
		joueur.GetComponent<scr_movement_player> ().carresActives = true;
	}

	public void whenClickedChoix()
	{
		if (GUIBouton.value == 0) numeroChoix = 1;
		if (GUIBouton.value == 1) numeroChoix = 2;
		if (GUIBouton.value == 2) numeroChoix = 3;
		if (GUIBouton.value == 3) numeroChoix = 4;
	}
}
