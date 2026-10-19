using UnityEngine;

public class ElevationExit : MonoBehaviour
{
    [SerializeField] Collider2D[] mountainColliders;
    [SerializeField] Collider2D[] boundryColliders;


    private void OnTriggerEnter2D(Collider2D collision) // this collision will store players collider when they trigger the collision
    {
        if(collision.gameObject.tag == Tags.Player){
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true; // when we enter the mountain we turn off tilemap colliders
            }

            foreach (Collider2D boundry in boundryColliders)
            {
                boundry.enabled = false; // when we enter the mountain we turn on tilemap boundry colliders
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        
        
    }
}
