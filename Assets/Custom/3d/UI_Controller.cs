using System.Collections;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public Transform health;
    public ItemsController ItemsController;

    private void Awake()
    {
        health.localScale = new Vector3(PlayerData.Data.HealthPer, 1, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        ItemsController.gameObject.SetActive(false);
        StartCoroutine(ChangeHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ItemsController.gameObject.SetActive(!ItemsController.gameObject.activeSelf);
        }
    }

    private IEnumerator ChangeHealth()
    {
        while (PlayerData.Data.HealthPer > 0)
        {
            ChangeHealth(-1);
            yield return new WaitForSeconds(2);
        }
    }

    private void ChangeHealth(int value)
    {
        PlayerData.Data.ChangeHealth(value);
        health.localScale = new Vector3(PlayerData.Data.HealthPer, 1, 1);
    }
}