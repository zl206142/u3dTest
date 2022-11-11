using UnityEngine;

public class BagSlot : MonoBehaviour
{
    private BagItem _item;

    public BagItem Item => _item;

    public bool PutItem(BagItem item)
    {
        if (_item || !item) return false;
        _item = item;
        _item.slot = this;
        Transform transform1;
        (transform1 = _item.transform).SetParent(transform);
        transform1.localPosition = Vector3.zero;
        return true;
    }

    public void ChangeItem(BagSlot slot)
    {
        var temp = slot._item;
        slot._item = null;
        slot.PutItem(_item);
        _item = null;
        PutItem(temp);
    }
}