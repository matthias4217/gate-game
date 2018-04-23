using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_avancerDansLeTexte : MonoBehaviour 
{
	public GameObject tuto;
	public GameObject texte1;
	public GameObject texte2;
	public GameObject texte3;
	public GameObject texte4;
	public GameObject image1;
	public GameObject image2;
	public int compteur = 0;
	// Update is called once per frame
	void LateUpdate ()
	{
		if (tag == "Safe") 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				if (compteur < 0) 
				{
					SceneManager.LoadScene ("Hub");
				}

				else if (compteur == 0) 
				{
					texte2.SetActive (true);
					texte1.SetActive (false);
					compteur++;
				} 

				else if (compteur == 1) 
				{
					texte3.SetActive (true);
					texte2.SetActive (false);
					compteur++;
					compteur *= -1;
				}
			}
		}


		if (tag == "porte_1") 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				if (compteur < 0) 
				{
					texte2.SetActive (false);
					texte3.SetActive (false);
					texte1.SetActive (true);
					compteur = 0;
					tuto.SetActive (false);
				}

				else if (compteur == 0) 
				{
					texte2.SetActive (true);
					texte1.SetActive (false);
					compteur++;
				} 

				else if (compteur == 1) 
				{
					texte3.SetActive (true);
					texte2.SetActive (false);
					compteur++;
					compteur *= -1;
				}
			}
		}

		else if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (compteur < 0) 
			{
				SceneManager.LoadScene ("Introduction_Combat");
			}

			if (compteur == 0) 
			{
				texte2.SetActive (true);
				texte1.SetActive (false);
				compteur++;
			} 
			else if (compteur == 1) 
			{
				image2.SetActive (true);
				texte3.SetActive (true);
				texte2.SetActive (false);
				image1.SetActive (false);
				compteur++;
			} 
			else if (compteur == 2) 
			{
				texte4.SetActive (true);
				texte3.SetActive (false);
				compteur++;
				compteur *= -1;
			}
		}
	}
}