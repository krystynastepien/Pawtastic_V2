using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameLogger.Log("Begin drag", LoggerConfig.LogCategoryFlags.UI);    
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameLogger.Log("Drag", LoggerConfig.LogCategoryFlags.UI);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameLogger.Log("End drag", LoggerConfig.LogCategoryFlags.UI);    
    }
}
