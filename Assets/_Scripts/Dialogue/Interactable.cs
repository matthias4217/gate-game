using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Ce script s'attache à tout objet avec lequel on peut interagir. 
 * L'objet doit avoir un Collider
 */
public class Interactable : MonoBehaviour {

    public TextAsset fichier_dialogue;
    /*
     * Une phrase = une boîte de dialogue.
     */
    //[Tooltip("La liste des phrases qui vont être dites.")]
    //[TextArea(3, 100)]
    //public Queue<string> sentences;
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
