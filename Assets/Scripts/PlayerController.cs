using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float baseSpeed = 5f;
    public float sprintSpeed = 10f;
    public float maxVelocityChange = 10f;
    
    [Space] public float jumpHeight = 10f;
    public float airController = 0.6f;
    

    private bool isSprinting;
    private Vector2 input;
    private Rigidbody rb;
    private bool isJumping;
    private bool isGrounded = false;

    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal") * baseSpeed, Input.GetAxis("Vertical") * baseSpeed);
        input.Normalize();
        isSprinting = Input.GetButton("Sprint");
        isJumping = Input.GetButton("Jump");
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            if (isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
            else if (input.magnitude > 0.5f)
            {
                rb.AddForce(Movement(isSprinting ? sprintSpeed : baseSpeed), ForceMode.VelocityChange);
            }
            else
            {
                var _velocity = rb.velocity;
                _velocity = new Vector3(_velocity.x * 0.05f * Time.fixedDeltaTime, _velocity.y,_velocity.z * 0.05f * Time.fixedDeltaTime);
                rb.velocity = _velocity;
            }
        }
        else
        {
            if (input.magnitude > 0.5f)
            {
                rb.AddForce(Movement(isSprinting ? sprintSpeed * airController: baseSpeed * airController), ForceMode.VelocityChange);
            }
            else
            {
                var _velocity = rb.velocity;
                _velocity = new Vector3(_velocity.x * 0.05f * Time.fixedDeltaTime, _velocity.y,
                    _velocity.z * 0.05f * Time.fixedDeltaTime);
                rb.velocity = _velocity;
            }
        }

        isGrounded = false;


    }
    

    private Vector3 Movement(float speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= speed;
        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Math.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Math.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            return velocityChange;
        }
        else
        {
            return new Vector3();
        }
    }
}
