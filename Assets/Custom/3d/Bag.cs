using UnityEngine;
using UnityEngine.Serialization;

public class Bag : MonoBehaviour
{
    [FormerlySerializedAs("count")] public int BagSlotCount;
    public BagItem itemDefault;

    private void Awake()
    {
        if (transform.childCount >= BagSlotCount || transform.childCount <= 0) return;
        var t = transform.GetChild(0);
        var c = BagSlotCount - transform.childCount;
        for (var i = 0; i < c; i++)
        {
            Instantiate(t, transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var i = 0;
        PlayerData.Data.EachItem((id, count) =>
        {
            var slot = transform.GetChild(i++).GetComponent<BagSlot>();
            var item = Instantiate(itemDefault);
            item.SetInfo(id, count);
            slot.PutItem(item);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}