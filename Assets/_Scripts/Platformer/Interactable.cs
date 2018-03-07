using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public Queue sentences;

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(sentences); 
    }
}
