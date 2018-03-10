using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_changercoleurOnMouseOver : MonoBehaviour
{
    public Color startColor;
    private void Start()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        GetComponent<Renderer>().material.SetColor("_EmissionColor",startColor);
    }
    private void OnMouseEnter()
    {
        this.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnMouseExit()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
    }
}
