using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData _data;

    public float health_per => _health / (float)_max_health;


    private int _health = 100;
    private int _max_health = 100;

    public static PlayerData Data
    {
        get
        {
            if (null == _data)
            {
                var o = new GameObject("PlayerData");
                _data = o.AddComponent<PlayerData>();
                DontDestroyOnLoad(o);
            }

            return _data;
        }
    }

    public void ChangeHealth(int value)
    {
        _health = Math.Clamp(_health + value, 0, _max_health);
    }
}