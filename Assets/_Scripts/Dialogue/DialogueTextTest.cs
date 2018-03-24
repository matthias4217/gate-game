using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	[System.Serializable]

/*Cette classe permet de préciser le contenu textuel du dialogue d'un NPC et n'est à attacher à rien.
 * On définit juste une classe ici.
 * Cette classe est utilisée par le script DialogueManagerTest
 */

	public class DialogueTextTest {

		public string NPCName;	// Le nom du NPC qui parle
		[TextArea(0, 100)]      // Permet de dire que l'on peut créer jusqu'à 100 répliques pour le NPC
	public string[] sentences;  // Champ de répliques pour le NPC
}
