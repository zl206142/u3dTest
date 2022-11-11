using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Text Name;
    public Text Info;

    private void Awake()
    {
        EventManager.Instance.AddEventListener("evt_open_item_info", SetInfo);
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void SetInfo(params object[] param)
    {
        gameObject.SetActive(true);
        Name.text = param[0].ToString();
        Info.text = param[0].ToString() + "info";
    }
}