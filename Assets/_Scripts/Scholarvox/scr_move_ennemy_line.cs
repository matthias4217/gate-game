using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_move_ennemy_line : MonoBehaviour

{
    public LinkedList<Vector3> visited;
    public Vector3 destination;
    public float x0;
    public bool is_moving;
    private Vector3 initialVector3Pos;
    private Quaternion initialQuaternionRot;
    [SerializeField] int numberBeforeBirth = 10;
    [SerializeField] int numberBeforeDeath = 35;
    public GameObject instantiatedObject;
    private bool rightToBorn = true;

	private scr_movement_player joueur;

    // Use this for initialization
    void Start ()
    {
		joueur = GameObject.Find("obj_player").GetComponent<scr_movement_player>();
        initialVector3Pos = this.transform.position;
        initialQuaternionRot = this.transform.rotation;
        x0 = transform.position.x;
        visited = new LinkedList<Vector3>();
        is_moving = false;

    }
	

	// Update is called once per frame
	void LateUpdate ()
    {
        if (transform.position.x <= initialVector3Pos.x + numberBeforeBirth) rightToBorn = true;
        if (transform.position.x > initialVector3Pos.x + numberBeforeBirth && rightToBorn && !joueur.is_moving)
        {
            Instantiate(instantiatedObject, initialVector3Pos, initialQuaternionRot);
            rightToBorn = false;
        }
        if (transform.position.x > initialVector3Pos.x + numberBeforeDeath) Destroy(gameObject);
        
        else
            if ((Input.GetMouseButtonDown(1) && joueur.visited.Count !=0) || joueur.collisionEnemy)
        {
            if (visited.Count != 0)
            {
                destination = visited.Last.Value;
                visited.RemoveLast();
            }
            else Destroy(gameObject);
        }
        else if (Input.GetMouseButtonDown(0) && !is_moving && joueur.is_moving)
        {
            visited.AddLast(transform.position);
            destination = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
        }
        seDeplacer(destination);
	}


    void seDeplacer(Vector3 destination)
    {
        if (joueur.is_moving)
        {
            this.is_moving = true;
        }
        if(is_moving)
        {
            float dx = 1 * Mathf.Sign(destination.x - x0) / (60.0f * joueur.JUMP_DURATION);
            this.transform.position += new Vector3(dx, 0, 0);
            if (Mathf.Abs(destination.x - this.transform.position.x) < 0.1)
            {
                this.transform.position = new Vector3(destination.x, destination.y, this.transform.position.z);
                x0 = destination.x;
                is_moving = false;
            }
        }
    }
}