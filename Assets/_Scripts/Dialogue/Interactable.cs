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
    [Tooltip("Bouton lançant le dialogue")]
    public GameObject dialogueLaunchButton;

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
            dialogueLaunchButton.SetActive(true); 
            //TriggerDialogue();
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        // On a bien les Debug.Log qui s'affichent,
        // Mais le bouton ne disparaît pas
        // Et le trigger ne se remet pas ?!?!
        Debug.Log("Hop, on quitte la zone d'interaction");
        if (col.gameObject.tag == "Player");
        {
            Debug.Log("On est dans le if !!!");
            dialogueLaunchButton.SetActive(false);
        }
    }


    /*
     * Appelle la fonction StartDialogue du DialogueManager avec l'attribut sentences
     */

    public void TriggerDialogue ()
    {
        /*
         * On lance un bouton, style bulle de texte avec "parlez-moi"
         */

        // Maintenant, c'est le bouton qui gère le lancement du dialogue ?
        // Mais comment lui donner les phrases en paramètres ?
//        FindObjectOfType<DialogueManager>().StartDialogue(sentences); 
    }
}
