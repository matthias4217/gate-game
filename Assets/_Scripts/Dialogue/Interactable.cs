using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Ce script s'attache à tout objet avec lequel on peut interagir. 
 * 
 */
public class Interactable : MonoBehaviour {

    //public TextAsset fichier_dialogue;


	/* 
	 * La liste des phrases qui vont être dites.
	 * Une phrase = une boîte de dialogue.
	 */
	[TextArea(3, 100)]
	public Queue<string> sentences;



	/*
	 * Appelle la fonction StartDialogue du DialogueManager avec l'attribut sentences
	 */
    public void TriggerDialogue ()
    {
        // Dialogue dialogue = JsonUtility.FromJson<Dialogue>(fichier_dialogue.text);
        FindObjectOfType<DialogueManager>().StartDialogue(sentences); 
    }
}
