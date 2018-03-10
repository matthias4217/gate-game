using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/*
 * This class represent a dialogue which a NPC can have.
 * Not used for the moment.
 */
public class Dialogue {

    public string characterName;	// The name of the character saying this dialogue	
    [TextArea(3, 100)]
    //public Queue<string> sentences;		// 
    public string[] sentences;
	
}
