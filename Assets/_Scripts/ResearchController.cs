using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class ResearchController : MonoBehaviour {

	[Tooltip("La bonne réponse de référence")]
	public string reponse;

	[SerializeField] //Pour accéder à l'objet plus facilement qu'avec GetGameComponent etc
	private InputField inputField;

	[SerializeField]
	private Text text;

	[SerializeField]
	private GameObject player;

	private string guess; //Le choix du joueur



	 void Awake() {
		//inputField.gameObject.SetActive(false);
		//text.text = "Où sont allées les abeilles de la revue .... ?";
	}


	void OnTriggerEnter(Collider obj) {
		if (obj.tag == "Player") {
			inputField.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit(Collider obj){
		if (obj.tag == "Player") {
			inputField.gameObject.SetActive(false);
		}
	}

	public void Input (string guess) {
		inputField.text = "";
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