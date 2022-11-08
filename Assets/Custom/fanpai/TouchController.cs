using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 _movement;
    private bool _working;

    [SerializeField] [Range(0.001f, 0.01f)]
    private float scale = 0.01f;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Graphic>().material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _working = true;
        Debug.Log("pointer down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _working = false;
        Debug.Log("pointer up");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_working) return;
        _movement += eventData.delta * scale;
        _movement.x = Math.Clamp(_movement.x, -3, 3);
        _movement.y = Math.Clamp(_movement.y, -3, 3);
        _material.SetFloat("_X", _movement.x);
        _material.SetFloat("_Y", _movement.y);
        _material.SetFloat("_MoveX", _movement.x switch
        {
            > 1 => _movement.x - 1,
            < -1 => _movement.x + 1,
            _ => 0
        });
        _material.SetFloat("_MoveY", _movement.y switch
        {
            > 1 => _movement.y - 1,
            < -1 => _movement.y + 1,
            _ => 0
        });
    }
}