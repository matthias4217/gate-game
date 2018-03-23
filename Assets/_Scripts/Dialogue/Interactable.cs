using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("Boite de dialogue")]
    public GameObject dialogueBox;
    [Tooltip("Endroit où s'affiche le texte du NPC")]
    public Text NPCName;
    [Tooltip("Endroit où s'affiche le texte du dialogue")]
    public Text dialogueCanvas;

    private Text DialogueText;
    private Dialogue dialogue;

    void Start()
    /*
     * On initialise les objets
     */
    {
        dialogue = JsonUtility.FromJson<Dialogue>(fichier_dialogue.text);
    }

    void OnTriggerEnter2D (Collider2D col)
        // Il faut que l'objet interactable ait un rigidbody et un Collision2D
    {
        //Debug.Log("Wsh wsh les individus");
        if(col.gameObject.tag == "Player")
        {
            dialogueLaunchButton.SetActive(true); 
        }
    }

    void OnTriggerExit2D (Collider2D col)
        /* Sous Linux, avant la compilation, le bouton ne disparaît pas
         * C'est un bug de Unity 2017.3.0f1, qui n'impacte pas le jeu final.
         */
    {
        //Debug.Log("Hop, on quitte la zone d'interaction");
        if (col.gameObject.tag == "Player");
        {
            dialogueLaunchButton.SetActive(false);
        }
    }


    public void TriggerDialogue ()
    {
        //Debug.Log("Triggering dialogue...");
        NPCName.text = dialogue.characterName;
        // on créé la queue à chaque fois, elle est dépilée par la suite
        // donc il est nécessaire de la recréer à chaque lancement du dialogue
        sentences = new Queue<string>(dialogue.sentences);
        DisplayNextSentence(sentences);
    }

    public void DisplayNextSentence (Queue<string> sentences)
    // fonction récursive
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        //StopAllCoroutines();            // Pour arrêter la diffusion des lettres si le joueur appuye sur continue pendant que le texte s'affiche
        //on essaie d'abord d'afficher toute la phrase d'un coup
        dialogueCanvas.text = sentence;
        //StartCoroutine(TypeSentence(sentence));
        DisplayNextSentence(sentences);
    }

    IEnumerator TypeSentence (string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())         // Pour afficher les lettres des textes une par une
        {
            // @matthias :
            // où bien un dialogueCanvas.text += letter; ?
            DialogueText.text += letter;
            yield return null;          // pour attendre un peu entre chaque lettre affichée
        }
    }

    public void EndDialogue ()
    {
        Debug.Log("Ending dialogue");
        //animator.SetBool("IsOpen", false);
    }
}
