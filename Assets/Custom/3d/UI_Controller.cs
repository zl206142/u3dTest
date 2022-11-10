using System.Collections;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public Transform health;

    private void Awake()
    {
        health.localScale = new Vector3(PlayerData.Data.health_per, 1, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeHealth());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator ChangeHealth()
    {
        while (PlayerData.Data.health_per > 0)
        {
            ChangeHealth(-1);
            yield return new WaitForSeconds(2);
        }
    }

    private void ChangeHealth(int value)
    {
        PlayerData.Data.ChangeHealth(value);
        health.localScale = new Vector3(PlayerData.Data.health_per, 1, 1);
    }
}