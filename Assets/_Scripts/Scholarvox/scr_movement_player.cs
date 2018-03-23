using UnityEngine;
using System.Collections.Generic;


public class scr_movement_player : MonoBehaviour
{
    // SECTION DEPLACEMENT

    public bool on_mouse_over = false;
    public bool is_moving = false;
    public float deltaX;
	[SerializeField] public float JUMP_DURATION = 0.4f;
    [SerializeField] float A_hor = -1.3f;
    [SerializeField] float A_ver = -6f;
    public Vector3 destination;
	public bool carresActives = true;
    public float x0 = 0;
    public float y0 = 0;
    bool collisionsActives = false;
    //public LinkedList<Vector3> visited = new LinkedList<Vector3>();
    public GameObject panneauRewind;
    public GameObject bouton;
    public GameObject collectible;
    public bool collisionEnemy = false;
	public bool tutoActive = true;



    void Update ()
    {
		if (!tutoActive) 
		{
			if (!is_moving) 
			{
				x0 = this.transform.position.x;
				y0 = this.transform.position.y;
				/*if (Input.GetMouseButtonDown(1) || collisionEnemy)		// If clic droit ou boule de feu hit
	            {
	                if (visited.Count != 0)
	                {
	                    destination = visited.Last.Value;
	                    deltaX = destination.x - this.transform.position.x;
	                    visited.RemoveLast();
	                    carresActives = false;
	                    is_moving = true;

	                    panneauRewind.SetActive(true);
	                    if (bouton.GetComponent<scr_collision_with_player>().compteurActive >= 0) bouton.GetComponent<scr_collision_with_player>().compteurActive -= 1;
	                    if (collectible.GetComponent<scr_collision_with_player>().compteurActive >= 0) collectible.GetComponent<scr_collision_with_player>().compteurActive -= 1;
	                    collisionEnemy = false;
	                }
	            }
	            */
			}
			if (is_moving) 
			{
				if (collisionsActives)
					collisionsActives = false;
				seDeplacer (destination);
			}
			if (GetComponent<CharacterController> ().enabled != collisionsActives)
				GetComponent<CharacterController> ().enabled = collisionsActives;
		}
    }

    public void seDeplacer(Vector3 destination)
    {
        //Choix de la forme de la courbe
        float A_choisi;
        if (Mathf.Abs(deltaX) < 1.5)
        {
            A_choisi = A_ver;
        }
        else
        {
            A_choisi = A_hor;
        }

        //Calcul de la parabole à suivre
        float c = destination.y + destination.x * (A_choisi * x0 - ((y0 - destination.y) / (x0 - destination.x)));
        float b = ((y0 - destination.y) / (x0 - destination.x)) - A_choisi * (destination.x + x0);
        float dx = deltaX / (60.0f * JUMP_DURATION);
        float yy = (A_choisi * (this.transform.position.x + dx) + b) * (this.transform.position.x + dx) + c;
        this.transform.position = new Vector3(this.transform.position.x + dx, yy, this.transform.position.z);

        //Condition d'arret dégueulasse de Raphaël
        if (Mathf.Abs(destination.x - this.transform.position.x) < 0.05)
        {
            this.transform.position = new Vector3(destination.x, destination.y, this.transform.position.z);
            is_moving = false;
            collisionsActives = true;
            x0 = this.transform.position.x;
            y0 = this.transform.position.y;
            carresActives = true;
			/*if (panneauRewind.active) 
			{
				panneauRewind.SetActive (false);
			}*/
        }
    }
}


