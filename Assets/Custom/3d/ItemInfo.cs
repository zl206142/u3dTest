using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Instance.AddEventListener("evt_open_item_info", a => { gameObject.SetActive(true); });
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}