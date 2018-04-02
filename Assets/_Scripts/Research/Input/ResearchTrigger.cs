using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Ce script est à attacher au PNJ qui demandera au joueur d'effectuer la recherche
 **/

public class ResearchTrigger : MonoBehaviour {


	[SerializeField] //Pour accéder à l'objet plus facilement qu'avec GetGameComponent etc
	private InputField inputField;

	void OnTriggerEnter2D (Collider2D col)
	// Il faut que l'objet interactable (le NPC qui parle) ait un Rigidbody et un Collider2D
	{
		if(col.gameObject.tag == "Player")
		{
			//inputField.gameObject.SetActive(true); //Pour une raison inconnue, cette méthode ne marche pas alors qu'elle le devrait 
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			//inputField.gameObject.SetActive(false); //Pour une raison inconnue, cette méthode ne marche pas alors qu'elle le devrait 
		}
	}
}
