using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_sourisdessus : MonoBehaviour
{
    GameObject joueur;
    GameObject non_sol;
    public Color startColor;
    public Color OnMouseOverColor;
    //public bool dessus;
    public bool clicAutorise;

    private void Start()
    {
        clicAutorise = true;
        joueur = GameObject.Find("obj_player");
        non_sol = GameObject.Find("tile_non_sol");
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        GetComponent<Renderer>().material.SetColor("_EmissionColor", startColor);
    }

    private void Update()
    {
        if (!joueur.GetComponent<scr_movement_player>().is_moving) clicAutorise = true;
        bool etat_carres = joueur.GetComponent<scr_movement_player>().carresActives;
        this.GetComponent<MeshRenderer>().enabled = etat_carres;
        this.GetComponent<BoxCollider>().enabled = etat_carres;
    }

    void OnMouseEnter()
    {
        //dessus = true;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", OnMouseOverColor);
    }

    void OnMouseExit()
    {
        //dessus = false;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", startColor);

    }

    private void OnMouseOver()
    {
        joueur.GetComponent<scr_movement_player>().on_mouse_over = true;
        if (Input.GetMouseButtonDown(0) && clicAutorise && non_sol.GetComponent<scr_OnMouseOverNonClicable>().clicAutorise)
            {
                //clicAutorise = false;
                joueur.GetComponent<scr_movement_player>().deltaX = this.transform.position.x - joueur.transform.position.x;
                joueur.GetComponent<scr_movement_player>().destination = this.transform.position;
                joueur.GetComponent<scr_movement_player>().carresActives = false;
//                joueur.GetComponent<scr_movement_player>().visited.AddLast(joueur.GetComponent<scr_movement_player>().transform.position);
                joueur.GetComponent<scr_movement_player>().is_moving = true;
            }
        joueur.GetComponent<scr_movement_player>().on_mouse_over = false;
    }
}
