using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public TextAsset fichier_dialogue;


    public void TriggerDialogue ()
    {
        Dialogue dialogue = JsonUtility.FromJson<Dialogue>(fichier_dialogue.text);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); 
    }
}
