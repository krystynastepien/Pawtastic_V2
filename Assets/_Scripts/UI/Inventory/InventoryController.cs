using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    #region Consts
    #endregion consts


    #region Serialized Fields
    [SerializeField] private InventoryItemSlot _itemPrefab;
    [SerializeField] private RectTransform _contentPanel;
    [SerializeField] private InventoryItemDescription _descriptionPanel;

    [SerializeField] private List<ItemData> _TEST_inventoryItems = new List<ItemData>(4);

    #endregion SerializedFields

    #region Fields

    private GameObject _inventoryCanvas;
    private List<InventoryItemSlot> _inventoryItems = new List<InventoryItemSlot>();
    private bool _isVisible;
    private Transform _parentAfterDrag;
    #endregion Fields

    #region Properties
    public bool IsVisible {get => _isVisible; private set => _isVisible = value; }
    #endregion

    #region Lifecycle Methods (GameFlow)
    public void OnStart()
    {
        // get inventory parent - Inventory Canvas HUD
        _inventoryCanvas = this.transform.parent.gameObject;

        ///create empty inventory with 6 slots
        InitializeInventoryUI(6);
        // TEMP: put a test item in first slot
        _inventoryItems[0].AssignItem(_TEST_inventoryItems[0], 1);
        //_inventoryItems[1].AssignItem(_TEST_inventoryItems[1], 99);
        //_inventoryItems[2].AssignItem(_TEST_inventoryItems[2], 3);
        OnHide();
    }
    #endregion Lifecycle Methods (GameFlow)



    #region public methods

    /// <summary>
    /// hide the inventory window
    /// </summary>
    public void OnHide()
    {
        _inventoryCanvas.SetActive(false);
        _descriptionPanel.ClearDescription();
        _isVisible = false;
    }

    /// <summary>
    /// show the inventory window
    /// </summary>
    public void OnShow()
    {
        _isVisible = true;
        
        _inventoryCanvas.SetActive(true);
    }
    
    #endregion public methods

    #region private methods

    /// <summary>
    ///  Initialize item slot objects in panel with set amount of inventory size
    /// </summary>
    /// <param name="heartCount">amount of hearts to create</param>
    private void InitializeInventoryUI(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
           InventoryItemSlot newSlot = CreateItemSlot();
           _inventoryItems.Add(newSlot);
        }
        _descriptionPanel.OnInit();
    
    }

        /// <summary>
    /// Create copy of item slot object 
    /// </summary>
    /// <param name="fullHeartSprite"></param>
    /// <returns></returns>
    private InventoryItemSlot CreateItemSlot()
    {
        // create prefab object
        InventoryItemSlot newSlot = Instantiate(_itemPrefab, _contentPanel.transform);
        if (newSlot == null)
        {
            GameLogger.LogError("Cannot create new item slot object for HUD Inventory.", LoggerConfig.LogCategoryFlags.UI, newSlot);
            return null;
        }
        newSlot.OnInit();
        SubscribeToEvents(newSlot);

        return newSlot;
    }

    /// <summary>
    /// assign delegates
    /// </summary>
    private void SubscribeToEvents(InventoryItemSlot itemSlot)
    {
        //PlayerManager.Instance._playerHealth.OnHealthChangedEvent += UpdateHUD_OnHealthChange;
        itemSlot.OnLeftMouseBtnClick += HandleItemSelection;
        itemSlot.OnRightMouseBtnClick += HandleShowItemActions;
        itemSlot.OnItemBeginDrag += HandleBeginDrag;
        itemSlot.OnItemDrag += HandleDrag;
        itemSlot.OnItemEndDrag += HandleEndDrag;
        itemSlot.OnItemDroppedOn += HandleItemsSwap;
        
    }

        #region drag and drop handlers
    private void HandleItemSelection(InventoryItemSlot slot)
    {
        slot.Select();
        var data = slot.GetItemData();
        if (data != null)
            _descriptionPanel.SetDescription(data);
        GameLogger.Log("Selected: " + slot.name, LoggerConfig.LogCategoryFlags.UI, slot);
    }

    private void HandleBeginDrag(InventoryItemSlot slot)
    {
        GameLogger.Log("Begin drag", LoggerConfig.LogCategoryFlags.UI);   
        _parentAfterDrag = slot.transform.parent; 

    }
    private void HandleDrag(InventoryItemSlot slot)
    {
        GameLogger.Log("Drag", LoggerConfig.LogCategoryFlags.UI);
        slot.SetItemImagePosition(Input.mousePosition);
    }

    private void HandleEndDrag(InventoryItemSlot slot)
    {
        GameLogger.Log("End drag", LoggerConfig.LogCategoryFlags.UI);  
        slot.transform.SetParent(_parentAfterDrag);
    }

    private void HandleShowItemActions(InventoryItemSlot slot)
    {
        throw new NotImplementedException();
    }

    private void HandleItemsSwap(InventoryItemSlot slot)
    {
        throw new NotImplementedException();
    }


        #endregion drag and drop handlers
    #endregion private methods


    #region helpers
    #endregion helpers

}
