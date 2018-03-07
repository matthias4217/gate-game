using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject Player;
    public float range;
    public float speed;
    private float Vitesse = 0.10f; // on choisit la vitesse de déplacement
    public float X;
    private int direction = 1; // nous sert a changer de direction
    public bool isDown = false;

void Start()
    {
        Enemy = GameObject.FindWithTag("Enemy");
        Player = GameObject.FindWithTag("Player");
        
    }

    void Update()
    {
        RigidbodyConstraints2D constraints = this.GetComponent<Rigidbody2D>().constraints;
        float distance = Mathf.Abs(Player.transform.position.x - X);
        if (distance <= range && constraints != RigidbodyConstraints2D.FreezePositionY)
        {
            Vector2 velocity = new Vector2((transform.position.x - Player.transform.position.x) * speed, (transform.position.y - Player.transform.position.y) * speed);
            GetComponent<Rigidbody2D>().velocity = -velocity;
        }
        if (distance > range && constraints != RigidbodyConstraints2D.FreezePositionY)
        {
            if (transform.position.x > X + 2.5f) // première condition "Si ..."
                direction = -1;
            else if (transform.position.x < X - 2.5f) // seconde si la première n'est pas vrai "Sinon si..."
                direction = 1;
            transform.position = new Vector2(transform.position.x + Vitesse * direction, transform.position.y);     
        }
    }

    void OnCollisionEnter2D(Collision2D other) //Pour que l'ennemi reste au sol une fois piétiné par le joueur
    {
        isDown = true;
        if (other.gameObject.tag == "Floor")
        {
            //Destroy(this.gameObject); Si on veut détruire l'ennemi
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY; // Si on veut que l'ennemi reste au sol qd il touche le sol
        }
    }
}
