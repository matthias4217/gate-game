using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text NameText;
    public Text NameTextPet;
    public Text DialogueText;
    public Text DialogueTextPet;
    public Animator animator;
    public Button Button;
    public Button ButtonContinue;
    

    private Queue<string> sentences; //pile FIIFO (first in first out) plus adaptée que String[]
    private Queue<string> sentencesPet; // pile pour discuter avec le pet



    void Start () {
        sentences = new Queue<string>();
        sentencesPet = new Queue<string>();
        
    }

    private void Update()
    {
       
        if (Vector2.Distance(GameObject.Find("Player").transform.position, ClosestPnj().transform.position) > 10f) // cas où y a pas de pnj à proximité pour discuter
        {
            Button.interactable = false;
            ButtonContinue.interactable = false;
        }
        else
        {
            Button.interactable = true;
            ButtonContinue.interactable = true;
        }
    }

    public GameObject ClosestPnj() // renvoie le pnj avec lequel discuter le plus proche
    {
        GameObject Closest = GameObject.FindGameObjectsWithTag("PNJ")[0];
        foreach (GameObject pnj in GameObject.FindGameObjectsWithTag("PNJ"))
        {
            if (Vector2.Distance(GameObject.Find("Player").transform.position, pnj.transform.position) < Vector2.Distance(GameObject.Find("Player").transform.position, Closest.transform.position))
            {
                Closest = pnj;
            }
        }
        return Closest;
    }

    public void StartDialogue (Queue sentences)
    {
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        NameText.text = "";
        NameTextPet.text = "Pet";
        // A partir de là faut mettre des if en fct du pnj à qui on parle puis ajouter les phrases
        // 1er PNJ == PNJ1
        if (ClosestPnj().name == "PNJ(1)")
        {
            NameText.text = "Revue effrayée";
            sentences.Enqueue("Ne me mangez pas ><");
            sentences.Enqueue("...");
            sentences.Enqueue("Vous n'êtes pas un sbire de Big Journal?");
            sentences.Enqueue("Hmm... C'est vrai que vous ne ressemblez pas à un journal");
            sentences.Enqueue("Vous venez d'une dimension parallèle ?!?");
            sentences.Enqueue("Peu importe. Je vais vous raconter une histoire");
            sentences.Enqueue("Nous vivions tous heureux en harmonie ici");
            sentences.Enqueue("Un jour, une malédiction a été lancé dans notre monde");
            sentences.Enqueue("Je vous en supplie sauvez nous!");

        }
        // 2ème PNJ == PNJ2
        if (ClosestPnj().name == "PNJ(2)")
        {
            NameText.text = "Revue terrifiée";
            sentences.Enqueue("AHHHHHHH!");
            sentences.Enqueue("Ah vous n'avez pas l'air méchant vous");
            sentences.Enqueue("Big Journal est une revue devenue maléfique à cause d'une malédiction");
            sentences.Enqueue("Ses sbires nous chassent et nous emprisonnent");
            sentences.Enqueue("Il faut que vous nous débarassiez de Big Journal!");
        }
        // 3ème PNJ == PNJ3
        if (ClosestPnj().name == "PNJ(3)")
        {
            NameText.text = "Revue appeurée";
            sentences.Enqueue("Au secours!");
            sentences.Enqueue("Mais vous êtes qui vous?");
            sentences.Enqueue("...");
            sentences.Enqueue("Bah, ça n'a plus d'importance");
            sentences.Enqueue("Big Journal me tuera bientôt comme mes amis");
            sentences.Enqueue("Nous ne savons pas pourquoi il est devenu maléfique et a décidé de prendre le pouvoir pour faire régner une tyrannie");
        }
        // 4ème PNJ == PNJ4
        if (ClosestPnj().name == "PNJ(4)")
        {
            NameText.text = "Revue désespérée";
            sentences.Enqueue("");
            sentences.Enqueue("");
            sentences.Enqueue("");
        }
        // 5ème PNJ == PNJ5
        if (ClosestPnj().name == "PNJ(5)")
        {
            NameText.text = "Revue prête à tout";
            sentences.Enqueue("Hm Hm");
            sentences.Enqueue("What?");
            sentences.Enqueue("...");
        }
        // 6ème PNJ == BigJournal
        if (ClosestPnj().name == "BigJournal")
        {
            NameText.text = "Big Journal";
            sentences.Enqueue("Tu es où?");
            sentences.Enqueue("What?");
            sentences.Enqueue("...");
        }
        // PNJ == Pet
            
            sentencesPet.Enqueue("C'est moi le Pet");
            sentencesPet.Enqueue("What?");
            sentencesPet.Enqueue("...");
        
       // DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); //Pour arrêter la diffusion des lettres si le joueur appuye sur continue pendant que le texte s'affiche
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentencePet()
    {
        NameText.text = "Pet";
        if (sentencesPet.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentencePet = sentencesPet.Dequeue();
        StopAllCoroutines(); //Pour arrêter la diffusion des lettres si le joueur appuye sur continue pendant que le texte s'affiche
        StartCoroutine(TypeSentence(sentencePet));
    }

    IEnumerator TypeSentence (string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) //pour afficher les lettres des textes une par une
        {
            DialogueText.text += letter;
            yield return null; // pour attendre un peu entre chaque lettre affichée
        }
    }
      

    public void EndDialogue ()
    {
        animator.SetBool("IsOpen", false);
    }
	
}
