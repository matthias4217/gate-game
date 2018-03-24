using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerTest : MonoBehaviour {
	public DialogueTextTest dialogue;
	public Text NameText;
	public Text DialogueText;
	public Queue<string> sentences;

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
}
