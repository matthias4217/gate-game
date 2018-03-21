using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_OnMouseOverNonClicable : MonoBehaviour
{
    public bool clicAutorise;
    public GameObject darkPanel;
    public GameObject sprite;
    public GameObject bouton;
    public GameObject joueur;

    private void Start()
    {
        if (tag == "collectible") clicAutorise = false;
        else clicAutorise = true;
    }
    //Trouver la version GUI
    private void OnMouseEnter()
    {
        if (tag == "collectible") clicAutorise = true;
        else clicAutorise = false;
    }

    private void OnMouseExit()
    {
        if (tag == "collectible") clicAutorise = false;
        else clicAutorise = true;
    }

    private void Update()
    {
        if (clicAutorise && tag == "collectible")
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Instantiate(darkPanel);
                Instantiate(sprite);
                Instantiate(bouton); 
            }
        }
    }
}
