using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Pawtastic.Utils;
using System.Linq;
using System;

public class HealthBarController : MonoBehaviour
{
    #region Consts

    #endregion

    #region Serialized Fields
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private GameObject _heartsPanel;
    #endregion Serialized Fields

    #region Fields
    private List<GameObject> _existingHeartsList = new List<GameObject>();
    private PlayerHealthController _playerCtx;
    private Sprite _halfHeartSprite;
    private Sprite _fulHeartSprite;
    #endregion Fields

    #region Lifecycle Methods (GameFlow)
    public void OnStart()
    {
        AssignSprites();
        InitializeHealthBarUI(PlayerStats.INIT_MAX_HEART_COUNT);
        GetPlayerCtx();
    }
    #endregion Lifecycle Methods (GameFlow)

    #region public methods
    /// <summary>
    /// update HeartPanel
    /// </summary>
    public void OnUpdateHeartPanel()
    {
        if (_playerCtx == null)
        {
            GameLogger.LogError("_playerCtx is null", LoggerConfig.LogCategoryFlags.Player | LoggerConfig.LogCategoryFlags.UI, _playerCtx);
            return;
        }

        int currentHealth = _playerCtx.CurrentHealth;
        if (currentHealth > 0)
        {
            int newHeartCount = GetCurrentHeartCount(currentHealth);

            ClearHeartsPanel();

            //update heart ui panel - rerender hearts
            InitializeHealthBarUI(newHeartCount);

            //update Last heart object to correct sprite
            UpdateLastHeartInPanel();
        }
        else
        {
            ClearHeartsPanel();
        }
    }
    #endregion public methods


    #region private methods
    private void GetPlayerCtx()
    {
        _playerCtx = PlayerManager.Instance._playerHealth;
        if (_playerCtx == null)
        {
            GameLogger.LogError("Failed to get player context", LoggerConfig.LogCategoryFlags.Player | LoggerConfig.LogCategoryFlags.UI, _playerCtx);
        }
    }

    /// <summary>
    ///  Initialize heart objects in panel with set amount of full hearts
    /// </summary>
    /// <param name="heartCount">amount of hearts to create</param>
    private void InitializeHealthBarUI(int heartCount)
    {
        //GameLogger.Log($"Configuring UI, hearts to render: {heartCount} ", LoggerConfig.LogCategoryFlags.UI);
        for (int i = 0; i < heartCount; i++)
        {
            CreateFullHeartObj(_fulHeartSprite);
        }
    }

    /// <summary>
    /// Create copy of object with given sprite
    /// </summary>
    /// <param name="fullHeartSprite"></param>
    /// <returns></returns>
    private GameObject CreateFullHeartObj(Sprite fullHeartSprite)
    {
        // create prefab object
        GameObject newHeart = Instantiate(_heartPrefab, _heartsPanel.transform);
        if (newHeart == null)
        {
            GameLogger.LogError("Cannot create new heart object for HUD lifebar.", LoggerConfig.LogCategoryFlags.UI, newHeart);
            return null;
        }

        // Get existing Image component
        Image heartImage = newHeart.GetComponent<Image>();
        heartImage.sprite = fullHeartSprite;

        _existingHeartsList.Add(newHeart);
        return newHeart;
    }

    /// <summary>
    /// updates last heart in heart list
    /// </summary>
    private void UpdateLastHeartInPanel()
    {
        GameObject lastHeart = _existingHeartsList.LastOrDefault();
        if (lastHeart != null)
        {
            int healthRest = _playerCtx.CurrentHealth % PlayerStats.BASE_HEART_LIFE; //modulo   7 mod 2 = 1 (the rest is 1)

            switch (healthRest)
            {
                case 0: //0 -> full heart
                    lastHeart.GetComponent<Image>().sprite = _fulHeartSprite; // technically it's not necessary 
                    break;
                case 1: //1 -> half heart
                    lastHeart.GetComponent<Image>().sprite = _halfHeartSprite;
                    break;
            }
        }
    }
    
    #endregion private methods


    #region helpers?

    /// <summary>
    /// Assign all health Bar sprites
    /// </summary>
    private void AssignSprites()
    {
        _fulHeartSprite = SpriteGrabber.LoadHeartSprite(SpriteGrabber.HUD_HeartState.Full);
        _halfHeartSprite = SpriteGrabber.LoadHeartSprite(SpriteGrabber.HUD_HeartState.Broken);
    }

    /// <summary>
    /// returns current heart count to render
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <returns></returns>
    private int GetCurrentHeartCount(float currentHealth)
    {
        float dirtyHeartCount = currentHealth / PlayerStats.BASE_HEART_LIFE;
        return (int)Math.Ceiling(dirtyHeartCount);
    }

    /// <summary>
    /// Remove all hearts from the panel
    /// </summary>
    private void ClearHeartsPanel()
    {
        foreach (Transform child in _heartsPanel.transform)
        {
            Destroy(child.gameObject);
        }
        _existingHeartsList.Clear();
    }
    #endregion


}
