using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController _controller; 
    public Animator _animator;
    Vector3 _PlayerVelocity;
    

    [SerializeField] GameObject HeadPosition;
    [SerializeField] bool Crouch = false;
    [SerializeField] bool CanStand;

    [Header("Player Inputs")]
    [SerializeField] float _InputX, _InputZ;
    [SerializeField] bool _JumpButton;
    [SerializeField] bool _RunButton;
    [SerializeField] bool _Attack;

    [Header("Player Settings")]
    [SerializeField] float _PlayerSpeed = 2f;
    [SerializeField] float _JumpForce = 2f;
    [SerializeField] float _Gravity = -10f;
    [SerializeField] bool _Grounded;

    [SerializeField] Transform _Pivot;

    private void Start()
    {
     
    }

    private void Update()
    {
        Movements();
        Jumping();
        Running();
        Crouching();
        Attacking();
        Backwards();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, _Pivot.eulerAngles.y, 0f);
    }

    private void Movements()
    {
        _InputX = Input.GetAxis("Horizontal");
        _InputZ = Input.GetAxis("Vertical");

        _animator.SetFloat("SpeedX", _InputX, 0.07f, Time.deltaTime);
        _animator.SetFloat("SpeedZ", _InputZ, 0.07f, Time.deltaTime);

        Vector3 moveDir = transform.forward * _InputZ + transform.right * _InputX;
        _controller.Move(moveDir * _PlayerSpeed * Time.deltaTime);
    }

    private void Jumping()
    {
        // Input
        _JumpButton = Input.GetKeyDown(KeyCode.Space);

        // Ground Check
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0f), Vector3.down, 0.35f))
        {
            _animator.SetBool("fall", false);
            _Grounded = true;
        }
        else
        {
            _animator.SetBool("fall", true);
            _Grounded = false;
        }

        // Jump
        if (_Grounded && _PlayerVelocity.y < 0)
        {
            _PlayerVelocity.y = -2f;
        }

        if (_JumpButton && _Grounded)
        {
            _animator.Play("Jump_up");
            _PlayerVelocity.y += Mathf.Sqrt(_JumpForce * -3f * _Gravity);
        }

        _PlayerVelocity.y += _Gravity * Time.deltaTime;
        _controller.Move(_PlayerVelocity * Time.deltaTime);
    }

    private void Running()
    {

        //Input
         _RunButton = Input.GetKey(KeyCode.LeftShift);

        //Running

        if (_RunButton == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _animator.SetBool("run", true);
                _PlayerSpeed = 4f;  
            }
        }
        else
        {
            _animator.SetBool("run", false);
            _PlayerSpeed = 2f;
        }
    }
    
    private void Crouching()
    {

        //Crouching

        if (Physics.Raycast(HeadPosition.transform.position, Vector3.up, 0.8f))
        {
            CanStand = false;
            Debug.DrawRay(HeadPosition.transform.position, Vector3.up, Color.green);
        }
        else
        {
            CanStand = true;
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {


            if (Crouch == true && CanStand == true)
            {
                Crouch = false; // Standing
                _animator.SetBool("Crouch", false);
                _JumpForce = 2f;
                _controller.height = 1.39f;
                _controller.center = new Vector3(0f, 0.69f, 0);
            }
      
            else
            {
                Crouch = true; // Crouching
                _animator.SetBool("Crouch", true);
                _JumpForce = 0f;
                _PlayerSpeed = 0.9f;
                _controller.height = 0.7f;
                _controller.center = new Vector3(0f, 0.33f, 0);
            }
        }
    }

    private void Attacking()

    {
        //Running

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetBool("Attack", true);
            _PlayerSpeed = 0f;
        }
        else
        {
            _animator.SetBool("Attack", false);
        }
    }

    private void Backwards()
    {
        if (Input.GetKey(KeyCode.S))
        {
            _PlayerSpeed = 1f;
        }
    }
}
