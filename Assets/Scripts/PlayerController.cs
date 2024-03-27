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
    private bool _jumpBeenInputted = false;
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


    [SerializeField]
    private float _wallCheckRadius;

    [SerializeField]
    private bool _isWalled = false;

    [SerializeField]
    private bool _leftWall = false;


    [SerializeField]
    private float _jumpsLeft = 1f;

    [SerializeField]
    private bool _canJump = true;

    [SerializeField]
    private bool _canJumpAgain = true;



    
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

        
    }

    private void FixedUpdate()
    {

        if (_jumpBeenInputted==true)
        {
            if (_jumpInput == false)
                _jumpBeenInputted = false;
        }
        //gound check
        _isGrounded = Physics.OverlapSphere(transform.position + _groundCheck, _groundCheckRadius).Length > 1;
        //add movement force
        _rigidbody.AddForce(_moveDirection * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //wall check 
        _isWalled = Physics.OverlapSphere(transform.position + _wallCheck, _wallCheckRadius).Length > 1;


        //clamp velocity to _maxspeed
        Vector3 velocity = _rigidbody.velocity;
        float newXSpeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        velocity.x = newXSpeed;
        _rigidbody.velocity = velocity;


        if (_isGrounded == true)
        {
            _canJump = true;
            _canJumpAgain = false;
        }
        else
        {
            _canJump = false;
        }

        if (_isWalled == true)
        {
            _leftWall = false;
        }



        if (_isWalled == true && _isGrounded == false)
        {
            _canJumpAgain = true;

        }
        else
            _canJumpAgain = false;

        //if (_leftWall == true && _isGrounded == false)
        //{
        //    _canJumpAgain = true;
        //}
        //else
        //    _canJumpAgain = false;

        //Invoke("_jumpInput", 0.02f);










        //add jump force
        if (_jumpInput == true && _jumpBeenInputted !=true&& _canJump == true || _jumpInput == true && _canJumpAgain == true &&_jumpBeenInputted != true&& _jumpsLeft > 0)
        {

            //Calculate force needed to reach _jumpHeight
            float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);

            if (_isGrounded == false)
                _jumpsLeft--;


            _jumpBeenInputted = true;
        }

        if (_isGrounded ==true)
        {
            _jumpsLeft = 3;
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
