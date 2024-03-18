using System.Collections;
using System.Collections.Generic;
using UnityEngine;


        [RequireComponent(typeof(Rigidbody))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private float _jumpHeight;

    [SerializeField]
    private Vector3 _groundCheck;

    [SerializeField]
    private float _groundCheckRadius;

    [SerializeField, Tooltip("How much vertical force is applied when jumping")]
    private float _jumpForce;

    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;
    private bool _jumpInput = false;
    private bool _isGrounded = false;

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
        _jumpInput = Input.GetAxisRaw("Jump") != 0;

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
        //gound check
        _isGrounded = Physics.OverlapSphere(transform.position + _groundCheck, _groundCheckRadius).Length > 1;
        //add movement force
        _rigidbody.AddForce(_moveDirection * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //clamp velocity to _maxspeed
        Vector3 velocity = _rigidbody.velocity;
        float newXSpeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        velocity.x = newXSpeed;
        _rigidbody.velocity = velocity;

        //add jump force
        if(_jumpInput&& _isGrounded)
        {
            //Calculate force needed to reach _jumpHeight
            float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
        
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //draw ground check 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _groundCheck, _groundCheckRadius);
    }
#endif
    //private void OnTriggerEnter(Collider other)
    //{
    //    _isGrounded = true;
    //}
}
