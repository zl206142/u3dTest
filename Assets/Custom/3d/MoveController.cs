using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _air;
    private Transform _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>().transform;
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
            _rigidbody.AddForce(0, 10, 0);
        }

        _camera.parent.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        _camera.parent.parent.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        _camera.transform.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel");
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        var f = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            f += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            f += Vector3.back;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            f += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            f += Vector3.right;
        }

        if (!(f.magnitude > 0)) return;
        f.Normalize();
        var transform1 = transform;
        transform1.position += transform1.rotation * f / 8f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _air++;
        Debug.Log("OnCollisionEnter" + _air);
    }

    private void OnCollisionExit(Collision other)
    {
        _air--;
        Debug.Log("OnCollisionExit" + _air);
    }
}