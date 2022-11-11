using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BagItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler,
    ICanvasRaycastFilter
{
    private int _id;
    private int _count;
    private bool _drag;
    private bool _isRaycastValid = true;
    [FormerlySerializedAs("_slot")] public BagSlot slot;
    public Text id;
    public Text count;


    public void SetInfo(int itemId, int itemCount)
    {
        _id = itemId;
        _count = itemCount;
        id.text = itemId.ToString();
        count.text = itemCount.ToString();
        count.gameObject.SetActive(itemCount > 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _drag = true;
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _drag = false;
        EventManager.Instance.DispatchEvent("evt_item_end_drag", transform);
        transform.localPosition = Vector3.zero;

        var g = eventData.pointerCurrentRaycast.gameObject;
        if (g)
        {
            Debug.Log(g);
            var slot = g.GetComponent<BagSlot>();
            if (slot)
            {
                Debug.Log(slot);
                slot.ChangeItem(this.slot);
            }

            var item = g.GetComponent<BagItem>();
            if (item)
            {
                Debug.Log(item);
                item.slot.ChangeItem(this.slot);
            }
        }

        _isRaycastValid = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_drag) return;
        EventManager.Instance.DispatchEvent("evt_open_item_info", _id);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
        EventManager.Instance.DispatchEvent("evt_item_begin_drag", transform);
        _isRaycastValid = false;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return _isRaycastValid;
    }
}