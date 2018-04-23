using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tuto_ScolarVox : MonoBehaviour 
{
	public GameObject tuto1;
	public GameObject tuto2;

	public void WhenClickedTuto1_ScholarVox()
	{
		tuto1.SetActive(true);
	}

	public void WhenClickedTuto2_ScholarVox()
	{
		tuto2.SetActive(true);
	}
}
