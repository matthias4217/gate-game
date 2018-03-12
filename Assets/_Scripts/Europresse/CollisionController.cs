using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CollisionController : MonoBehaviour
{

    public Slider healthBarSlider;  //reference for slider


    /* Check if player enters/stays on the Enemy */
    void OnCollisionStay2D(Collision2D other)
    {
        //if player triggers an Enemy and health is greater than 0
        if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<EnemyController>().isDown && healthBarSlider.value > 0)
        {
            healthBarSlider.value -= 0.002f;  //reduce health
        }
    }

//    void OnTriggerEnter2D(Collider2D other) //Pour détruire l'ennemi si contact avec pieds du joueur
//    {
//        if (other.gameObject.tag == "Enemy")
//        {
//            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * 10, ForceMode2D.Impulse);
//            Destroy(other.gameObject);
//        }
//    }
}
