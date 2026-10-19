using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    #region Consts

    private const string UseLabel = "Use";
    private const string DropLabel = "Drop";
    private const string EatLabel = "Eat";
    private Color EmptyColor = new Color(249,235,210);
    #endregion


    #region  Serialized Fields
    [SerializeField] private GameObject _counter;
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private Button _useBtn;
    [SerializeField] private Button _dropBtn;
    [SerializeField] private Image _itemImg;
    [SerializeField] private Image _borderImg;
    [SerializeField] private Image _selectedBorderImg;
    #endregion

    #region Delegates
    public event Action<InventoryItemSlot> OnLeftMouseBtnClick;    
    public event Action<InventoryItemSlot> OnItemDroppedOn;
    public event Action<InventoryItemSlot> OnItemBeginDrag;
    public event Action<InventoryItemSlot> OnItemDrag;
    public event Action<InventoryItemSlot> OnItemEndDrag;
    public event Action<InventoryItemSlot> OnRightMouseBtnClick;
    #endregion

    #region  Fields
    private bool _isEmpty = true;
    private ItemData _itemData;
    #endregion


    #region Properties
    public Button UseButton { get => _useBtn; private set => _useBtn = value; }
    public Button DropButton { get => _dropBtn; private set => _dropBtn = value; }
    #endregion



    #region Initialization
    public void OnInit()
    {
        ClearSlot();
        Deselect();
    }
    #endregion


    #region Public Methods

    public void ClearSlot()
    {
        _isEmpty = true;
        _counterText.text = "1";
        _counter.SetActive(false);
        _buttonPanel.SetActive(false);
        _itemImg.sprite = null;
        _itemImg.color = EmptyColor;
        _itemData = null;
        _itemImg.enabled = false;
    }

    public void AssignItem(ItemData item, int quantity)
    {
        _isEmpty = false;
        _itemImg.enabled = true;
        _itemImg.sprite = item.Icon;
        _itemImg.color = item.Color;
        SetCounter(item, quantity);
        _itemData = item;
    }

    /// <summary>
    /// set slot as selected
    /// </summary>
    public void Select()
    {
        if(!_isEmpty){
            _selectedBorderImg.enabled = true;
        }
    }

    /// <summary>
    /// Deselect item slot
    /// </summary>
    public void Deselect()
    {
        _selectedBorderImg.enabled = false;
    }

    public ItemData GetItemData() => _itemData;
    public GameObject GetItemImage() => _itemImg.gameObject;    
    public void SetItemImagePosition(Vector3 input)
    {
        _itemImg.gameObject.transform.position = input;
    }
    



    #endregion


    #region Events and Actions

    public void OnBeginDrag()
    {
        if (_isEmpty) return;     
        
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrag()
    {        
        OnItemDrag?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData">data on position of the item and which button has been clicked</param>
    public void OnPointerClick(BaseEventData eventData)
    {
        //if (_isEmpty) return;     
        
        PointerEventData data = (PointerEventData)eventData;
        if (data.button == PointerEventData.InputButton.Left)
        {
            OnLeftMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// sets text for counter and shows if it's greater than 1
    /// </summary>
    /// <param name="quantity"></param>
    private void SetCounter(ItemData item, int quantity)
    {
        if(quantity > item.MaxStackSize)
        {
            _counterText.text = item.MaxStackSize.ToString();
            quantity = item.MaxStackSize;
        }
        else
        {
            _counterText.text = quantity.ToString();
        }

        if (quantity > 1)
        {
            _counter.SetActive(true);
        }
        else{
            _counter.SetActive(false);
        }
    }
 
    #endregion


    #region helpers



    /// <summary>
    /// Assign all health Bar sprites
    /// </summary>
    private void AssignSprites()
    {
        // _fulHeartSprite = SpriteGrabber.LoadHeartSprite(SpriteGrabber.HUD_HeartState.Full);
        // _halfHeartSprite = SpriteGrabber.LoadHeartSprite(SpriteGrabber.HUD_HeartState.Broken);
    }
    #endregion
}