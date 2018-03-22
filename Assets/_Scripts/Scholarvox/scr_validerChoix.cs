using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_validerChoix : MonoBehaviour 
{
	[SerializeField] int val1 = 2;
	[SerializeField] int val2 = 1;
	[SerializeField] int val3 = 4;

	public Dropdown choix1;
	public Dropdown choix2;
	public Dropdown choix3;

	public void whenClickedValidate()
	{
		Debug.Log (choix1.value + " " + choix2.value + " " + choix3.value);
		if (choix1.value == val1 && choix2.value == val2 && choix3.value == val3)
		{
			Debug.Log ("shoryuken");
		}
	}
}
