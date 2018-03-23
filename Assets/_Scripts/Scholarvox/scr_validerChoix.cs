using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_validerChoix : MonoBehaviour 
{
	[SerializeField] int val1 = 2;
	[SerializeField] int val2 = 1;
	[SerializeField] int val3 = 4;
	[SerializeField] float tempsAttente = 0.7f;

	Color couleurInitiale;
	public Color couleurWrong;
	private void Start()
	{
		couleurInitiale = this.GetComponent<Image>().color;
	}
	public Dropdown choix1;
	public Dropdown choix2;
	public Dropdown choix3;
	public GameObject panneauRecherche;
	public GameObject livre0;
	public GameObject GUI_livre0;

	public void whenClickedValidate()
	{
		if (choix1.value == val1 && choix2.value == val2 && choix3.value == val3) {
			Debug.Log ("shoryuken");
			panneauRecherche.SetActive (false);
			livre0.SetActive (true);
			GUI_livre0.SetActive (true);
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
