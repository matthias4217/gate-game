using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Ce script s'attache à tout objet avec lequel on peut interagir. 
 * L'objet doit avoir un Collider
 */
public class Interactable : MonoBehaviour {

    //public float distanceTrigger;
    //public TextAsset fichier_dialogue;
    //static Dialogue dialogue = JsonUtility.FromJson<Dialogue>(fichier_dialogue.text);
    /*
     * La liste des phrases qui vont être dites.
     * Une phrase = une boîte de dialogue.
     */
    [TextArea(3, 100)]
    public Queue<string> sentences;
    //static Queue<string> sentences = dialogue.sentences;

        

    void OnCollisionEnter2D (Collision2D col)
    {
        //Debug.Log("Yaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah");
        if(col.gameObject.tag == "Player")
        {
            TriggerDialogue();
        }
    }


	/*
	 * Appelle la fonction StartDialogue du DialogueManager avec l'attribut sentences
	 */

    void Update()
    {
       // Nous obtenons la position de l'objet
       // Puis de Player
       // Si < x, alors Trigger
       /* GameObject player = GameObject.Find ("Player");
        * Transform playerTransform = player.transform;
        * // get player position
        * Vector3 playerPosition = playerTransform.position;
        * 
        * Vector3 pnjPosition = transform.position;
        * 
        * Vector3 distanceVector = playerPosition - pnjPosition;
        * float distance = distanceVector.magnitude;
        * 
        * if (distance < distanceTrigger) {
        *    TriggerDialogue();
        * }
        */
       /*
        * Ou avec un collider ?
        */

       /*
        * Ou une touche à activer
        */
    }

    public void TriggerDialogue ()
    {
        //Debug.Log("Hadouken !");
        /*
         *
         *
         *
         *
         *
         */
        FindObjectOfType<DialogueManager>().StartDialogue(sentences); 
    }
}
