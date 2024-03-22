using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


        [RequireComponent(typeof(Rigidbody))] 
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _isPlayerOne = true;

    private Rigidbody _rigidbody;

    [SerializeField]
    private Vector3 _moveDirection;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _acceleration;

    [Space]

    [SerializeField]
    private bool _jumpInput = false;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField, Tooltip("How much vertical force is applied when jumping")]
    private float _jumpForce;

    [SerializeField]
    private Vector3 _groundCheck;

    [SerializeField]
    private float _groundCheckRadius;
    [SerializeField]

    private bool _isGrounded = false;

    [SerializeField]
    private Vector3 _wallCheck;
    //[SerializeField]

    //private Vector2 _wallchecc;
    //private Vector4 _wallChecd;

    [SerializeField]
    private float _wallCheckRadius;

    [SerializeField]
    private bool _isWalled = false;

    private bool _wallClinged = false;

    [SerializeField]
    private float _canWallJump = 0f;

    [SerializeField]
    private bool _canJump = true;

    [SerializeField]

    private bool _alreadyJumped = false;

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


        //_moveDirection = new Vector3(Input.GetAxisRaw("P" + (_isPlayerOne ? "1" : "2") + " Hori", 0, 0);

        if (_isPlayerOne)
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("P1 Hori"), 0, 0);
            _jumpInput = Input.GetAxisRaw("P1 Vert") != 0;
        }
        else
        {

            _moveDirection = new Vector3(Input.GetAxisRaw("P2 Hori"), 0, 0);
            _jumpInput = Input.GetAxisRaw("P2 Vert") != 0;
        }

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

        //wall check 
        _isWalled = Physics.OverlapSphere(transform.position + _wallCheck, _wallCheckRadius).Length > 1;




        if (_isGrounded)
            _canWallJump = 0;
        else if (_isWalled)
            _canWallJump = 1;
        else if (_isWalled && _isGrounded)
            _canWallJump = 2;
        else if (!_isGrounded && _isWalled)
            _canWallJump = 3;


        //0 means only grounded, 1 means walled, 2 means walled and grounded, 3 means off ground and walled but just jumped
        //_canWallJump = _isWalled && !_isGrounded;

        //maybe do a wall cling check or something?


        //clamp velocity to _maxspeed
        Vector3 velocity = _rigidbody.velocity;
        float newXSpeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        velocity.x = newXSpeed;
        _rigidbody.velocity = velocity;

        //add jump force
        if(_jumpInput/*&& _isGrounded /* || _jumpInput && _isWalled*/ /*|| _jumpInput && _offWall*/)
        {
            //bool for if a one condition for jumping is meant to prevent additional jumping
            //bool alreadyJumped = false;
            //check

            if (_isGrounded && _alreadyJumped == false)
            {
                //Calculate force needed to reach _jumpHeight
                float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
                _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
                _alreadyJumped = true;
            }

            if (_wallClinged&&_alreadyJumped == false)
            {
                //Calculate force needed to reach _jumpHeight
                float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
                _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
                _alreadyJumped = true;
            }

            

        ////wall cling
        //_wallClinged = _isWalled == true && _isGrounded == false;


        //    if (_isGrounded || _isWalled ||_wallClinged)
        //    {

        //        //Calculate force needed to reach _jumpHeight
        //        float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        //        _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        //        _wallClinged = false;
        //    }

            

            
        }
        
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //draw ground check 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _groundCheck, _groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _wallCheck, _wallCheckRadius);
        
    }
#endif
    //private void OnTriggerEnter(Collider other)
    //{
    //    _isGrounded = true;
    //}
}
