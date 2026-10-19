using UnityEngine;

public class HUDManager : Singleton<HUDManager>, IManager
{
    #region Serialized Fields
        [SerializeField] private HealthBarController _healthBarController;
        [SerializeField] private InventoryController _inventoryController;

    #endregion

    #region Properties

    #endregion

    #region IManager Lifecycle Methods (GameFlow)
    public void OnAwake()
    {
        base.Initialize(); // Call the singleton logic
    }

    public void OnStart()
    {
        _healthBarController.OnStart();
        _inventoryController.OnStart();
        SubscribeToEvents();
    }

    public void OnFixedUpdate()
    {
    }

    public void OnFrameUpdate()
    {
    }

    public void Update()
    {
         if (Input.GetKeyDown(KeyCode.I)) {
            GameLogger.Log("pressed I", LoggerConfig.LogCategoryFlags.UI, _inventoryController);
            ShowOrHide(UIElement.InventoryUI, !_inventoryController.IsVisible);
        }
    }
    #endregion



    #region Public Methods
    /// <summary>
    /// rerender HUD healthbar on health change
    /// </summary>
    public void UpdateHUD_OnHealthChange()
    {
        _healthBarController.OnUpdateHeartPanel();
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// assign delegates
    /// </summary>
    private void SubscribeToEvents()
    {
        // subscribe to OnHealthChangedEvent to update HUD on health change
        PlayerManager.Instance._playerHealth.OnHealthChangedEvent += UpdateHUD_OnHealthChange;
    }
    
    private void OnDestroy()
    {
        //Unsubscribe to prevent memory leaks
        if (PlayerManager.Instance._playerHealth != null)
        {
            PlayerManager.Instance._playerHealth.OnHealthChangedEvent -= UpdateHUD_OnHealthChange;
        }
    }

    /// <summary>
    /// show or hide given HUD element
    /// </summary>
    /// <param name="element"></param>
    private void ShowOrHide(UIElement element, bool enable)
    {
        switch (element)
        {
            case UIElement.InventoryUI:
                if (enable)
                    _inventoryController.OnShow();
                else
                    _inventoryController.OnHide();              
                break;
            default:
                break;
        }
    }

    #endregion
}

/// <summary>
/// ui elements to control
/// </summary>
enum UIElement
{
    LifePanelUI,
    InventoryUI,
    PauseUI,
    MainMenuUI,
}