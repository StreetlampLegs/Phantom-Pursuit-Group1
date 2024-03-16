using System.Collections;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
   public CharacterController _controller;
    public Animator _animator;
    public CinemachineFreeLook PlayerFollowCamera;
     

    [SerializeField] float _PlayerSpeed = 2f;
    [SerializeField] float _RunSpeed = 4f;

    private void Start()
    {

           if (PlayerFollowCamera == null)
            {
                Debug.LogError("Cinemachine FreeLook Camera is not assigned to the PlayerScript!");
            }
        
    }

    private void Update()
    {
        Movements();
        Running();
        UpdateCameraInput(); // Call method to update camera input
    }

    private void Movements()
    {
        float _InputX = Input.GetAxis("Horizontal");
        float _InputZ = Input.GetAxis("Vertical");

        _animator.SetFloat("SpeedX", _InputX, 0.07f, Time.deltaTime);
        _animator.SetFloat("SpeedZ", _InputZ, 0.07f, Time.deltaTime);

        Vector3 moveDir = transform.forward * _InputZ + transform.right * _InputX; 
        _controller.Move(moveDir * _PlayerSpeed * Time.deltaTime);
    }

    private void Running()
    {
        bool _RunButton = Input.GetKey(KeyCode.LeftShift);

        if (_RunButton && Input.GetKey(KeyCode.W))
        {
            _animator.SetBool("Run", true);
            _PlayerSpeed = _RunSpeed;
        }
        else
        {
            _animator.SetBool("Run", false);
            _PlayerSpeed = 2f;
        }
    }
    private void UpdateCameraInput()
    {
        if (PlayerFollowCamera == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the virtual camera based on mouse input
        if (PlayerFollowCamera.Follow != null)
        {
            PlayerFollowCamera.transform.Rotate(Vector3.up, mouseX); // Rotate around the up axis
            PlayerFollowCamera.transform.Rotate(PlayerFollowCamera.transform.right, -mouseY); // Rotate around the camera's right axis
        }
    }
}

