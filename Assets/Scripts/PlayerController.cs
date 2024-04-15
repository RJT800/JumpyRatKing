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



    void Update()
    {



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
        // prevents potential jump inputs 
        if (_jumpBeenInputted==true)
        {
            if (_jumpInput == false)
                _jumpBeenInputted = false;
        }
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
        if (_jumpInput == true && _jumpBeenInputted !=true &&_isGrounded==true)
        {

            //Calculate force needed to reach _jumpHeight
            float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);

            
            _jumpBeenInputted = true;
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

}
