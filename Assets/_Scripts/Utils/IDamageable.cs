using System.Collections;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// Substracts given damage from object's current health
    /// </summary>
    /// <param name="damageVal"></param>
    public void TakeDamage(int damageVal){} 

    /// <summary>
    /// Substracts given damage from object's current health over time
    /// </summary>
    /// <param name="damageVal"></param>
    public void TakeDamageOverGivenTime(float time, int damageVal){} 
    
    /// <summary>
    /// Substracts given damage from object's current health every x seconds
    /// </summary>
    /// <param name="damageVal"></param>
    public void TakeDamageForSeconds(float seconds, int damageVal){} 
}
