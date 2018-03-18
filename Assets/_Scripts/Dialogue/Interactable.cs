using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Ce script s'attache à tout objet avec lequel on peut interagir. 
 * L'objet doit avoir un Collider de type Collider2D, ainsi qu'un rigidbody
 */
public class Interactable : MonoBehaviour {

    public TextAsset fichier_dialogue;
    [TextArea(3, 100)]
    private Queue<string> sentences;

    void Start()
    {
        Dialogue dialogue = JsonUtility.FromJson<Dialogue>(fichier_dialogue.text);
        sentences = new Queue<string>(dialogue.sentences);
    }

    void OnTriggerEnter2D (Collider2D col)
    // Il faut que l'objet interactable ait un rigidbody et un Collision2D
    {
        Debug.Log("Wsh wsh les individus");
        if(col.gameObject.tag == "Player")
        {
            TriggerDialogue();
        }
    }


	/*
	 * Appelle la fonction StartDialogue du DialogueManager avec l'attribut sentences
	 */

    public void TriggerDialogue ()
    {
        //Debug.Log("Hadouken !");
        /*
         * On lance un bouton, style bulle de texte avec "parlez-moi"
         *
         *
         *
         *
         *
         */
        FindObjectOfType<DialogueManager>().StartDialogue(sentences); 
    }
}
