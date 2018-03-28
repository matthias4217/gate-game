using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_validerChoix : MonoBehaviour 
{
	public int val1 = 1;
	public int val2 = 2;
	public int val3 = 3;
	[SerializeField] float tempsAttente = 0.7f;

	Color couleurInitiale;
	public Color couleurWrong;
	private void Start()
	{
		couleurInitiale = this.GetComponent<Image>().color;
		if (tag == "porte_2") 
		{
			val1 = 3;
			val2 = 0;
			val3 = 1;
		}
	}
	public Dropdown choix1;
	public Dropdown choix2;
	public Dropdown choix3;
	public GameObject panneauRecherche;
	public GameObject livre0;
	public GameObject GUI_livre0;
	public GameObject joueur;
	public bool pointGiven = false;

	public void whenClickedValidate()
	{
		if (choix1.value == val1 && choix2.value == val2 && choix3.value == val3) {
			Debug.Log ("shoryuken");
			panneauRecherche.SetActive (false);
			livre0.SetActive (true);
			GUI_livre0.SetActive (true);
			if (!pointGiven) 
			{
				joueur.GetComponent<scr_movement_player> ().nbreRecherches += 1;
				pointGiven = true;
			}
		} 
		else 
		{
			this.GetComponent<Image> ().color = couleurWrong;
			StartCoroutine (WaitColor (tempsAttente));
		}

	}


	private IEnumerator WaitColor(float time) 
	{
		yield return new WaitForSeconds(time);
		this.GetComponent<Image> ().color = couleurInitiale;
	}

}
