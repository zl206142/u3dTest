using System;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _air;
    private Vector3 _speed;
    private Transform _camera;
    private Animator _animator;
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Fall = Animator.StringToHash("fall");
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>().transform;
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _air > 0)
        {
            _rigidbody.AddForce(transform.rotation * _speed * 5 + new Vector3(0, 10, 0));
            _animator.SetTrigger(Jump);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        _camera.parent.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        _camera.transform.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel");
    }

    private void FixedUpdate()
    {
        // if (_air == 0) return;
        // transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        var f = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            f += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            f += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            f += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            f += Vector3.right;
        }

        _animator.SetFloat(Speed, f.magnitude);
        _speed = f.normalized;
        if (!(f.magnitude > 0)) return;
        var transform1 = transform;
        transform1.position += transform1.rotation * _speed / 8f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // _air++;
        // Debug.Log("OnCollisionEnter" + _air);
    }

    private void OnCollisionExit(Collision other)
    {
        // _air--;
        // Debug.Log("OnCollisionExit" + _air);
    }

    private void OnTriggerEnter(Collider other)
    {
        _air++;
        Debug.Log("OnTriggerEnter" + _air);
    }

    private void OnTriggerExit(Collider other)
    {
        _air--;
        Debug.Log("OnTriggerExit" + _air);
        if (_air == 0)
        {
            _animator.SetTrigger(Fall);
        }
    }
}