using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _f_count;
    private int _t_count;
    private Vector3 _speed;
    private bool _speed_up;
    private Transform _camera;
    private Animator _animator;
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Fall = Animator.StringToHash("fall");
    private static readonly int Speed = Animator.StringToHash("speed");

    public Transform model;
    public float MoveSpeed = 1;
    public float MouseXSpeed = 1;
    public float MouseYSpeed = 1;

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
        if (Input.GetKeyDown(KeyCode.Space) && _f_count > 0 && _t_count > 0)
        {
            Debug.Log("GetKeyDown Space");
            _rigidbody.AddForce(transform.rotation * _speed * 40 + new Vector3(0, 300, 0));
        }


        _speed_up = Input.GetKey(KeyCode.LeftShift);

        if (Cursor.visible) return;
        _camera.parent.transform.Rotate(-Input.GetAxis("Mouse Y") * MouseYSpeed, 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * MouseXSpeed, 0);
        _camera.transform.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel");
    }

    private void FixedUpdate()
    {
        if (_t_count == 0 && _f_count == 0) return;
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

        f.Normalize();
        if (f.z < 0) f /= 2;
        _animator.SetFloat(Speed, f.magnitude);
        f = _speed = f * MoveSpeed * (_speed_up ? 2 : 1);
        f *= Time.fixedDeltaTime;
        if (f.magnitude <= 0) return;
        model.localRotation =
            Quaternion.Euler(0, (f.x > 0 ? 90 : f.x < 0 ? -90 : 0) * (f.z > 0 ? 0.5f : f.z < 0 ? -0.5f : 1), 0);
        var transform1 = transform;
        transform1.position += transform1.rotation * f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _t_count++;
        Debug.Log("OnCollisionEnter" + _t_count);
    }

    private void OnCollisionExit(Collision other)
    {
        _t_count--;
        Debug.Log("OnCollisionExit" + _t_count);
    }

    private void OnTriggerEnter(Collider other)
    {
        _f_count++;
        Debug.Log("OnTriggerEnter" + _f_count);
        _animator.SetTrigger(Fall);
    }

    private void OnTriggerExit(Collider other)
    {
        _f_count--;
        Debug.Log("OnTriggerExit" + _f_count);
        if (_f_count == 0)
            _animator.SetTrigger(Jump);
    }
}