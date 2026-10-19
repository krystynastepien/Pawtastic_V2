using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    void OnTriggerStay(Collider characterCollider)
    {
        if(characterCollider.gameObject.tag == Tags.Player){
            
        }

    }

}
