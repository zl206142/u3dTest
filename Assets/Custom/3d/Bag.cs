using UnityEngine;

public class Bag : MonoBehaviour
{
    public int count;
    private void Awake()
    {
        if (transform.childCount >= count || transform.childCount <= 0) return;
        var t = transform.GetChild(0);
        var c = count - transform.childCount;
        for (var i = 0; i < c; i++)
        {
            Instantiate(t, transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
