using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour, IPointerDownHandler
{
    public Text id;
    public Text count;

    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.Instance.DispatchEvent("evt_open_item_info");
    }

    public void SetInfo(int _id, int _count)
    {
        id.text = _id.ToString();
        count.text = _count.ToString();
        count.gameObject.SetActive(_count > 1);
    }
}