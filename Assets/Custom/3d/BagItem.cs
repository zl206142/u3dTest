using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    private int _id;
    private int _count;
    public Text id;
    public Text count;

    public void ShowInfo()
    {
        EventManager.Instance.DispatchEvent("evt_open_item_info", _id);
        Debug.Log(id.text);
        Debug.Log(count.text);
        Debug.Log(_id);
        Debug.Log(_count);
    }

    public void SetInfo(int itemId, int itemCount)
    {
        _id = itemId;
        _count = itemCount;
        id.text = itemId.ToString();
        count.text = itemCount.ToString();
        count.gameObject.SetActive(itemCount > 1);
    }
}