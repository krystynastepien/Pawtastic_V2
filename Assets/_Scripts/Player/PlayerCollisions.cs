using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
      Debug.Log("OnTriggerEnter2D");
      ProcessCollisionOverTime(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
        StopCollisionOverTime(collision.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      //Debug.Log("OnCollisionEnter2D");
      ProcessCollision(collision.gameObject);
    }

    #region Helpers
    void ProcessCollision(GameObject collider){
        
        //switch for different tags
        if (collider.CompareTag(Tags.Water))
        {
            PlayerManager.Instance._playerHealth.TakeDamage(damageVal: 2);
        }
    }

    void ProcessCollisionOverTime(GameObject collider){ 
        //switch for different tags
        if (collider.CompareTag(Tags.Water))
        {
            PlayerManager.Instance._playerHealth.TakeDamageForSeconds(seconds: 1, damageVal: 1);
        }
    }

    void StopCollisionOverTime(GameObject collider){
        
        //switch for different tags
        if (collider.CompareTag(Tags.Water))
        {
            PlayerManager.Instance._playerHealth.StopActiveDamageCoroutine();
        }
    }
    #endregion
}
