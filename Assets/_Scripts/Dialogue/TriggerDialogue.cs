using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Ce script est à attacher au NPC qui doit parler. 
 * Il permet de faire apparaître la boîte de dialogue attachée au NPC lorsque
 * le joueur est à proximité du NPC.
 * Il permet aussi de faire disparaître la boîte de dialogue quand le joueur s'éloigne trop du NPC.
 **/

public class TriggerDialogue : MonoBehaviour {
        /*
         * Remettre ici la DialogueBox qui apparaîtra ou disparaîtra en fct de 
         * la proximité du joueur au NPC
         */
	public GameObject DialogueBox;
        /* 
         * Remettre l'animator de la DialogueBox. Pour cela, on peut glisser 
         * direct la DialogueBox dans le champ sur Unity. Unity comprend qu'il
         * faut prendre l'objet Animator de la DialogueBox
         */
	public Animator animator;
	
	void OnTriggerEnter2D (Collider2D col)
	// Il faut que l'objet interactable (le NPC qui parle) ait un Rigidbody et un Collider2D
	{
		if(col.gameObject.tag == "Player")
		{
			DialogueBox.SetActive(true); 
			animator.SetBool ("IsOpen", true);
		}
	}

	void OnTriggerExit2D (Collider2D col)
	/* Sous Linux, avant la compilation, le bouton ne disparaît pas quand on fait un SetActive(false) du bouton
         * C'est un bug de Unity 2017.3.0f1, qui n'impacte pas le jeu final.
         */
	{
		//Debug.Log("Hop, on quitte la zone d'interaction");
		if (col.gameObject.tag == "Player")
		{
			animator.SetBool ("IsOpen", false);
			DialogueBox.SetActive(false);
		}
	}
}
