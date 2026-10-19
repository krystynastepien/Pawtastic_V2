using UnityEngine;
using Pawtastic.Utils.Enums;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public Items ItemType;
    public ItemUse Action;
    public string Name;
    [TextArea] public string Description;
    [TextArea] public string OnUseDescription;
    public Sprite Icon;
    public Color Color;
    public int MaxStackSize = 1;
}
