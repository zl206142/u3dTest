using UnityEditor.UI;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public Transform TopContent;
    private Transform _tempParent;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventManager.Instance.AddEventListener("evt_item_begin_drag", args =>
        {
            if (!gameObject.activeSelf) return;
            var a = (Transform)args[0];
            _tempParent = a.parent;
            a.SetParent(TopContent);
        });
        EventManager.Instance.AddEventListener("evt_item_end_drag", args =>
        {
            if (!_tempParent) return;
            ((Transform)args[0]).SetParent(_tempParent);
            _tempParent = null;
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}