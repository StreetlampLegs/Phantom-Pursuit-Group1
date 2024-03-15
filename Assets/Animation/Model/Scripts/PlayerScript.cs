using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController _controller; 
    public Animator _animator;
    

    [SerializeField] GameObject HeadPosition;
    [SerializeField] bool CanStand;

    [Header("Player Inputs")]
    [SerializeField] float _InputX, _InputZ;
    [SerializeField] bool _RunButton;

    [Header("Player Settings")]
    [SerializeField] float _PlayerSpeed = 2f;
    [SerializeField] bool _Grounded;

    [SerializeField] Transform _Pivot;

    private void Start()
    {
     
    }

    private void Update()
    {
        Movements();
        Running();
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

   

    private void Running()
    {

        //Input
         _RunButton = Input.GetKeyDown(KeyCode.LeftShift);

        //Running

        if (_RunButton == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _animator.SetBool("Run", true);
                _PlayerSpeed = 4f;
                Debug.Log("running");
            }
        }
        else
        {
            _animator.SetBool("Run", false);
            _PlayerSpeed = 2f;
 
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
