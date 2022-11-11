using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    private static PlayerData _data;

    private int _health = 100;
    private int _max_health = 100;
    private readonly Dictionary<int, int> _items = new();

    public float HealthPer => _health / (float)_max_health;
    public int ItemCount => _items.Count;

    public static PlayerData Data
    {
        get
        {
            if (null != _data) return _data;
            var o = new GameObject("PlayerData");
            _data = o.AddComponent<PlayerData>();
            DontDestroyOnLoad(o);
            return _data;
        }
    }

    private void Awake()
    {
        _items.Add(1, 1);
        _items.Add(2, 1);
        _items.Add(3, 1);
        _items.Add(5, 2);
        _items.Add(6, 7);
    }

    public void ChangeHealth(int value)
    {
        _health = Math.Clamp(_health + value, 0, _max_health);
    }

    public int GetItemCount(int id)
    {
        return _items.TryGetValue(id, out var count) ? count : 0;
    }

    public void EachItem(UnityAction<int, int> action)
    {
        foreach (var (key, value) in _items)
        {
            action.Invoke(key, value);
        }
    }
}