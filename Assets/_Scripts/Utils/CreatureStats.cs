using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public abstract class CreatureStats : MonoBehaviour, IDamageable
{
    #region Fields
    private int _baseAttack;
    private int _baseDefense;
    private float _baseSpeed;
    private int _maxHealth;
    private int _currentHealth;
    #endregion

    #region Properties

    /// <summary>
    /// returns character's max health value
    /// </summary>
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    /// <summary>
    /// returns character's current health value
    /// </summary>
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    
    /// <summary>
    /// returns character's attack value
    /// </summary>
    public int Attack { get => _baseAttack; set => _baseAttack = value; }

    /// <summary>
    /// returns character's defense value
    /// </summary>
    public int Defense { get => _baseDefense; set => _baseDefense = value; }

    /// <summary>
    /// returns character's speed value
    /// </summary>
    public float Speed { get => _baseSpeed; set => _baseSpeed = value; }

    #endregion


    #region Fake Ctor
    public void BaseInit(int maxHealth, int attack, int defense, float speed)
    {
        _baseAttack = attack;
        _baseDefense = defense;
        _baseSpeed = speed;
        _maxHealth = maxHealth;

        _currentHealth = maxHealth;
    }

    #endregion

    #region Modifiers

    public abstract void TakeDamage(int damageVal);
    public abstract void TakeDamageOverGivenTime(float time, int damageVal);
    public abstract void TakeDamageForSeconds(float seconds, int damageVal);
    public abstract void StopActiveDamageCoroutine();


    /// <summary>
    /// Substracts given damage from object's current health over time
    /// </summary>
    /// <param name="damageVal"></param>
    public abstract IEnumerator DamageOverGivenTimeCoroutine(float duration, int damageValMax);

    /// <summary>
    /// Substracts given damage from object's current health every x seconds
    /// </summary>
    /// <param name="damageVal"></param>
    public abstract IEnumerator DamageForSecondsCoroutine(float duration, int damageValMax);


   
    /// <summary>
    /// returns characters health after taking damage
    /// </summary>
    /// <param name="damage"></param>
    public void Heal(int healVal){
        _maxHealth = _maxHealth + healVal;
        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
    
    #endregion
}
