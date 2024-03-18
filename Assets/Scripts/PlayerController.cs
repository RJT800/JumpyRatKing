using System.Collections;
using System.Collections.Generic;
using UnityEngine;


        [RequireComponent(typeof(Rigidbody))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxSpeed;

    [SerializeField, Tooltip("How much vertical force is applied when jumping")]
    private float _jumpForce;

    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;
    private bool _isGrounded;

    public float Speed
    {
        get => _maxSpeed;
        set => _maxSpeed = Mathf.Max(0, value);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            Debug.LogError("RigidBody is null!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);


        //float moveInput = Input.GetAxisRaw("Horizontal");
        //float jumpInput = 0;
        //if (Input.GetKeyDown(KeyCode.Space)&& _isGrounded)
        //{
        //    jumpInput = 1;
        //    _isGrounded = false;
        //}

        //_rigidbody.AddForce(Vector3.right * moveInput * _maxSpeed * Time.deltaTime);
        //_rigidbody.AddForce(Vector3.up * jumpInput * _jumpForce * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        //add movement force
        _rigidbody.AddForce(_moveDirection * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //clamp velocity to _maxspeed
        if (_rigidbody.velocity.magnitude > _maxSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }
}
