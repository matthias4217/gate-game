using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_clicked : MonoBehaviour 
{
	public GameObject livre0;
	public GameObject panelInfos;
	public GameObject livre1;
	public GameObject livre2;
	public GameObject livre3;
	public GameObject livre4;
	public GameObject livre5;
	public GameObject recherche;
	public GameObject joueur;
	public Dropdown GUIBouton;
	public GameObject paquetRecherche2;
	public bool pointGiven = false;
	// Use this for initialization

	//nbreCollectibles ne s'active que si l'ouvrage est consulté
	public void whenClicked0()
	{
		panelInfos.SetActive (true);
		livre0.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().tutoActive = true;
	}


	public void whenClicked1()
	{
		panelInfos.SetActive (true);
		livre1.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().tutoActive = true;
		if (!pointGiven) 
		{
			joueur.GetComponent<scr_movement_player> ().nbreCollectibles += 1;
			pointGiven = true;
		}
	}

	public void whenClicked2()
	{
		panelInfos.SetActive (true);
		livre2.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().tutoActive = true;
		if (!pointGiven) 
		{
			joueur.GetComponent<scr_movement_player> ().nbreCollectibles += 1;
			pointGiven = true;
		}
	}

	public void whenClicked3()
	{
		panelInfos.SetActive (true);
		livre3.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().tutoActive = true;
		if (!pointGiven) 
		{
			joueur.GetComponent<scr_movement_player> ().nbreCollectibles += 1;
			pointGiven = true;
		}
	}

	public void whenClicked4()
	{
		panelInfos.SetActive (true);
		livre4.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().tutoActive = true;
		if (!pointGiven) 
		{
			joueur.GetComponent<scr_movement_player> ().nbreCollectibles += 1;
			pointGiven = true;
		}
	}

	public void whenClickedClose()
	{
		livre0.SetActive (false);
		livre1.SetActive (false);
		livre2.SetActive (false);
		livre3.SetActive (false);
		livre4.SetActive (false);
		recherche.SetActive (false);
		panelInfos.SetActive (false);
		paquetRecherche2.SetActive (false);
		joueur.GetComponent<scr_movement_player> ().tutoActive = false;
	}

	public void whenClickedRechercheFinale()
	{
		livre0.SetActive (false);
		livre1.SetActive (false);
		livre2.SetActive (false);
		livre3.SetActive (false);
		livre4.SetActive (false);
		panelInfos.SetActive (true);
		paquetRecherche2.SetActive (true);
		joueur.GetComponent<scr_movement_player> ().tutoActive = true;
	}
}
