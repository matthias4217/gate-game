using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[System.Serializable]

//Cette classe permet de préciser le contenu textuel du dialogue d'un NPC
	public class DialogueTextTest {

		public string NPCName;	// The name of the character saying this dialogue	
		[TextArea(3, 100)]
	public string[] sentences;
}
