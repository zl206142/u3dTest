using UnityEngine;
using UnityEngine.EventSystems;

public class BagItem : MonoBehaviour, IPointerDownHandler
{
    

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.Instance.DispatchEvent("evt_open_item_info");
    }
}