using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text NameText;
    public Text DialogueText;
    public Animator animator;	

    public void StartDialogue (Queue<string> sentences)
    {
        animator.SetBool("IsOpen", true);
        //NameText.text = dialogue.characterName;
        sentences.Clear();
        DisplayNextSentence(sentences);
    }

    public void DisplayNextSentence (Queue<string> sentences)
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); 		// Pour arrêter la diffusion des lettres si le joueur appuye sur continue pendant que le texte s'affiche
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())		// Pour afficher les lettres des textes une par une
        {
            DialogueText.text += letter;
            yield return null; 		// pour attendre un peu entre chaque lettre affichée
        }
    }
      

    public void EndDialogue ()
    {
        animator.SetBool("IsOpen", false);
    }
	
}
