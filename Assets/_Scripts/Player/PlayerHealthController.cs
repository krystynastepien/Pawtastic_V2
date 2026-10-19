using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthController : CreatureStats
{
    #region  Fields
    private int _maxHeartCount;
    private int _currentHeartCount;
    /// <summary>
    /// Tracks active damage coroutine to prevent multiple simultaneous damage effects
    /// </summary>
    private Coroutine _damageCoroutine;
    #endregion

    #region Properties
    /// <summary>
    /// Maximum number of lives the player can have
    /// </summary>
    public int MaxHeartCount { get => _maxHeartCount; private set => _maxHeartCount = value; }

    /// <summary>
    /// Current number of lives remaining. Character dies when it reaches 0.
    /// </summary>
    public int CurrentHeartCount { get => _currentHeartCount; private set => _currentHeartCount = value; }
    #endregion

    #region Events
        /// Action is a delegate that doesnt return value
        /// Action<int, int> means: "methods that take 2 ints and return nothing"
    /// <summary>
    /// 
    /// </summary>
    public event Action OnHealthChangedEvent;

    #endregion

    #region IManager Lifecycle Methods (GameFlow)
    public void OnStart()
    {
        InitPlayer();
    }

    #endregion

    #region Initialization
    private void InitPlayer()
    {
        int maxLifeValue;

        _maxHeartCount = PlayerStats.INIT_MAX_HEART_COUNT;
        _currentHeartCount = PlayerStats.INIT_MAX_HEART_COUNT;
        maxLifeValue = PlayerStats.BASE_HEART_LIFE * _currentHeartCount;
        BaseInit(
            maxLifeValue,
            PlayerStats.BASE_ATTACK,
            PlayerStats.BASE_DEFENSE,
            PlayerStats.BASE_SPEED
        );
    }
   
    #endregion

    #region Damage Methods
    /// <summary>
    /// Player has X hearts. each heart has init value = X * heartVal = max life.
    /// </summary>
    /// <param name="damageVal"></param>
    public override void TakeDamage(int damageVal)
    {
        CurrentHealth -= damageVal;
        if (CurrentHealth <= 0)
        {
            GameLogger.Log("PLAYER died", LoggerConfig.LogCategoryFlags.Player, this);
            gameObject.SetActive(false);         
        }

        // The ?. is null-check: "only invoke if someone is listening (there is a subscriber to this event)"
        OnHealthChangedEvent?.Invoke();
    }

    public override void TakeDamageOverGivenTime(float time, int damageVal)
    {
        if (_damageCoroutine == null)
            _damageCoroutine = StartCoroutine(DamageOverGivenTimeCoroutine(time, damageVal));
        else
        {
            StopActiveDamageCoroutine();
            _damageCoroutine = StartCoroutine(DamageOverGivenTimeCoroutine(time, damageVal));
        }
    }

    /// <summary>
    /// Subtracts given damage from object's current health over time
    /// </summary>
    public override void TakeDamageForSeconds(float seconds, int damageVal)
    {
        Debug.Log("_damageCoroutine: " + _damageCoroutine);
        if (_damageCoroutine == null)
            _damageCoroutine = StartCoroutine(DamageForSecondsCoroutine(seconds, damageVal));
        else
        {
            StopActiveDamageCoroutine();
            _damageCoroutine = StartCoroutine(DamageForSecondsCoroutine(seconds, damageVal));
        }
    }

    public override void StopActiveDamageCoroutine()
    {
        if (_damageCoroutine != null)
            StopCoroutine(_damageCoroutine);
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Applies damage over a given duration (total damageValMax spread across duration)
    /// </summary>
    public override IEnumerator DamageOverGivenTimeCoroutine(float duration, int damageValMax)
    {
        float amountDamaged = 0;
        int damagePerLoop = (int)Math.Round(damageValMax / duration);
        if (damagePerLoop == 0)
            damagePerLoop = 1;

        while (amountDamaged < damageValMax)
        {
            TakeDamage(damagePerLoop);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Applies damage repeatedly every X seconds until health reaches 0
    /// </summary>
    public override IEnumerator DamageForSecondsCoroutine(float seconds, int damageVal)
    {
        while (CurrentHealth > 0)
        {
            seconds = (int)Math.Round(seconds);
            TakeDamage(damageVal);
            GameLogger.Log($"Player is taking continous damage: {damageVal}dmg over {seconds}sec.", LoggerConfig.LogCategoryFlags.Player, this);
            GameLogger.Log($"Players current Life: {PlayerManager.Instance._playerHealth.CurrentHealth}", LoggerConfig.LogCategoryFlags.Player, this);
            yield return new WaitForSeconds(seconds);
        }
    }
    #endregion
}