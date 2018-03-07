using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// Pas utilisé pour le moment
public class Dialogue {

    public string characterName;	// The name of the character saying this dialogue	
    [TextArea(3, 100)]
    public string[] sentences;		// 
	
}
