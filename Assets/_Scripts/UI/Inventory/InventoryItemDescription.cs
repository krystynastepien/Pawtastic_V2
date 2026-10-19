using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItemDescription : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Image _itemImg;
    [SerializeField] private TMP_Text _itemDescriptionTxt;
    [SerializeField] private TMP_Text _itemOnUseDescriptionTxt;
    [SerializeField] private TMP_Text _itemNameTxt;
    #endregion

    #region  Fields
    private bool _noItemSelected = true;
    #endregion



    
    #region Public Methods
    public void OnInit()
    {
        ClearDescription();
    }

    public void ClearDescription()
    {
        _noItemSelected = true;
        _itemImg.sprite = null;
        _itemImg.gameObject.SetActive(false);
        _itemNameTxt.text = "-";
        _itemDescriptionTxt.text = "-";
        _itemOnUseDescriptionTxt.text = "-";
    }

    public void SetDescription(ItemData item)
    {
        _noItemSelected = false;
        _itemImg.sprite = item.Icon;
        _itemImg.color = item.Color;
        _itemImg.gameObject.SetActive(true);
        _itemNameTxt.text = item.Name;
        _itemDescriptionTxt.text = item.Description;
        _itemOnUseDescriptionTxt.text = item.OnUseDescription;
    }


    #endregion
}
