using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_collision_with_player : MonoBehaviour
{
	public GameObject joueur;
	public GameObject triggeredObject;
    public GameObject triggeredColor;
    public GameObject triggeredUi;
	public GameObject boutonCaseDeFin;
    public int compteurActive = -1;
    public bool AlreadyTriggered = false;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && compteurActive >= 0) compteurActive += 1;
        if (AlreadyTriggered && compteurActive < 0)
        {
            triggeredObject.SetActive(true);
            AlreadyTriggered = false;
        }
        if (AlreadyTriggered && tag == "trigger") triggeredColor.GetComponent<Renderer>().material.color = Color.green;
        else if (tag == "trigger") triggeredColor.GetComponent<Renderer>().material.color = Color.white;
    }



    private void OnTriggerEnter(Collider collision)
    {
		if (this.tag == "enemy" && !GetComponent<scr_move_ennemy_line> ().is_moving) {
			if (collision.tag == "Player") 
			{
				collision.transform.position = collision.GetComponent<scr_movement_player> ().initialPosition;
				//Application.LoadLevel (Application.loadedLevel);
				//collision.GetComponent<scr_movement_player> ().collisionEnemy = true;

			}
		} else if (this.tag == "trigger") 
		{
			if (AlreadyTriggered && compteurActive < 0) 
			{
				triggeredObject.SetActive (true);
				AlreadyTriggered = false;
			}
			if (!AlreadyTriggered) 
			{
				if (compteurActive < 0) 
				{
					compteurActive = 0;
					//Destroy(triggeredObject, 0.15f);
					triggeredObject.SetActive (false);
					AlreadyTriggered = true;
				}
			}
		} 
		else if (this.tag == "collectible") 
		{
			if (compteurActive < 0) 
			{
				compteurActive = 0;
				triggeredObject.SetActive (false);
				//score +=1
				//afficher un trivia sur le livre 1
				AlreadyTriggered = true;
				triggeredUi.SetActive (true);
                                // send here to GA
			}
		}
		else if (tag == "triggerFin" && !joueur.GetComponent<scr_movement_player> ().tutoActive)
		{
			boutonCaseDeFin.SetActive (true);
		} 
    }
}
