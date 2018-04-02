using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Ce script est à attacher à l'inputField où le joueur rentrera sa recherche
 * Faudra bien vérifier que dans OnEndEdit pour l'inputfield, la fonction Input est bien sélectionnée
 * Il faut aussi l'attacher au GameObject ResearchController (déjà fait sur le prefab).
 **/

public class ResearchController : MonoBehaviour {

	[Tooltip("La bonne réponse de référence")]
	public string reponse;

	[Tooltip("La question posée pour effectuer la recherche")]
	public string question;

	[SerializeField] //Pour accéder à l'objet plus facilement qu'avec GetGameComponent etc
	private InputField inputField;

	[SerializeField]
	private Text text;

	private string guess; //Le choix du joueur


	 void Awake() {
		text.text = question;
	}
		
	public void Input (string guess) {
		inputField.text = "";
		CompareReponse (guess);
	}

	void CompareReponse(string guess) {
		if (guess.Equals(reponse)) {
			text.text = "Vous avez répondu juste. Vous pouvez passer maintenant pauvre fou!";
			//inputField.SetActive(false); //Pour une raison inconnue, cette méthode ne marche pas alors qu'elle le devrait

		} else {
			text.text = "Mauvaise réponse: Vous ne passerez pas!";
		}
	}
}