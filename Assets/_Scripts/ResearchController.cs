using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class ResearchController : MonoBehaviour {
	private string reponse; //La bonne réponse de référence
	private string guess; //Le choix du joueur

	[SerializeField] //Pour accéder à l'objet plus facilement qu'avec GetGameComponent etc
	private InputField input;

	[SerializeField]
	private Text text;

	[SerializeField]
	private GameObject player;

	 void Awake() {
	//	input.SetActive(false);
		reponse = "kebab";
		text.text = "Où sont allées les abeilles de la revue .... ?";
	}


	void OnTriggerEnter(Collider obj) {
		if (obj.tag == "Player") {
	//		input.SetActive(true);
		}
	}

	void OnTriggerExit(Collider obj){
		if (obj.tag == "Player") {
	//		input.SetActive(false);
		}
	}

	public void Input (string guess) {
		input.text = "";
		CompareReponse (guess);
	}
	void CompareReponse(string guess) {
		if (guess.Equals(reponse)) {
			text.text = "Vous avez répondu juste. Vous pouvez passer maintenant pauvre fou!";
	//		input.SetActive(false);

		} else {
			text.text = "Mauvaise réponse: Vous ne passerez pas!";
		}
	}
}