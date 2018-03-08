using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {

	[SerializeField] private GameObject trigger;
	[SerializeField] private Sprite destSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//change la couleur du mur si le trigger est détruit, pas optimal
		if (trigger.activeSelf == false)
			this.GetComponent<SpriteRenderer> ().sprite = destSprite;
		}

	public void DestroyWall() {
		//permet d'être détruit uniquement si le trigger est détruit
		if (trigger.activeSelf == false)
			this.gameObject.SetActive (false);
	}
}
