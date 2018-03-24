using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Ce script permet de gérer l'animation des boîtes de dialogue,
 * l'affichage des répliques et du nom du NPC
 * le défilement des répliques lettre par lettre.
 * Il faut attacher ce script à l'objet DialogueBox qui est la boîte de dialogue d'un NPC (un objet DialogueBox par NPC à prévoir)
 * 
 * PS: Voir le script TriggerDialogue pour le déclenchement des dialogues à proximité du NPC
 */


public class DialogueManagerTest : MonoBehaviour {
	public DialogueTextTest dialogue; 
	public Text NameText;              // Champ de texte pour afficher le nom du NPC
	public Text DialogueText;          // Champ de texte pour afficher les répliques
	public Queue<string> sentences;
	public Animator animator;          // Objet pour gérer les animations 

	void Start () {
		sentences = new Queue<string> ();
	}

	public void TriggerDialogue() {
		StartDialogue (dialogue);
	}

	public void StartDialogue (DialogueTextTest dialogue) {
		NameText.text = dialogue.NPCName;
		sentences.Clear ();
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue (sentence);
		}
		DisplayNextSentence ();
	}

	public void DisplayNextSentence() {
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}
		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	IEnumerator TypeSentence (string sentence) {
		DialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) 
		{
			DialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue() {
		animator.SetBool ("IsOpen", false);
	}
}
