using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _isOpen = false;
    public bool IsOpen { get => _isOpen; set => _isOpen = value; }
    [SerializeField]
    private bool _allowDirectInteraction = true;
    public bool AllowDirectInteraction { get => _allowDirectInteraction; set => _allowDirectInteraction = value; }
    [SerializeField]
    private bool _isRotatingDoor = true;
    [SerializeField]
    private float _speed = 1f;

    [Header("Rotation Config")]
    [SerializeField]
    private float _rotationAmount = 90f;
    [SerializeField]
    private float _forwardDirection = 0;

    [Header("Sliding Configs")]
    [SerializeField]
    private Vector3 _slideDirection = Vector3.left;
    [SerializeField]
    private float _slideAmount = 1.9f;

    [Header("Auto Open Config")]
    [SerializeField]
    private bool _autoOpenAfterClose = true; // Whether the door should automatically open after being closed
    [SerializeField]
    private float _autoOpenDelay = 5f; // Delay in seconds before the door opens automatically after closing

    private Vector3 _startRotation;
    private Vector3 _startPosition;
    private Vector3 _openPosition;
    private Vector3 _forward;

    private Coroutine _animationCoroutine;

    private void Awake()
    {
        _startRotation = transform.rotation.eulerAngles;
        _forward = transform.forward;
        _startPosition = transform.position;
        _openPosition = _startPosition + _slideAmount * _slideDirection;

        if (IsOpen)
        {
            transform.position = _openPosition;
        }
    }

    public void Open(Vector3 userPosition)
    {
        if (!IsOpen)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = _isRotatingDoor ?
                StartCoroutine(DoRotationOpen(Vector3.Dot(_forward, (userPosition - transform.position).normalized))) :
                StartCoroutine(DoSlidingOpen());
        }
    }

    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y + (forwardAmount > _forwardDirection ? _rotationAmount : -_rotationAmount), 0));

        IsOpen = true;

        yield return RotateDoor(startRotation, endRotation);
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = _startPosition + _slideAmount * _slideDirection;

        IsOpen = true;

        yield return MoveDoor(transform.position, endPosition);
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = _isRotatingDoor ? StartCoroutine(DoRotationClose()) : StartCoroutine(DoSlidingClose());

            if (_autoOpenAfterClose)
            {
                StartCoroutine(AutoOpenCountdown(_autoOpenDelay));
            }
        }
    }

    private IEnumerator AutoOpenCountdown(float delay)
    {
        yield return new WaitForSeconds(delay);
        Open(Vector3.zero);
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(_startRotation);

        IsOpen = false;

        yield return RotateDoor(startRotation, endRotation);
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = _startPosition;

        IsOpen = false;

        yield return MoveDoor(transform.position, endPosition);
    }

    private IEnumerator RotateDoor(Quaternion startRotation, Quaternion endRotation)
    {
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
    }

    private IEnumerator MoveDoor(Vector3 startPosition, Vector3 endPosition)
    {
        float time = 0;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }
    }
}
