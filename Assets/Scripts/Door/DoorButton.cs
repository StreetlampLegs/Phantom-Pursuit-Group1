using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField]
    private Door _door;

    private Vector3 _position;

    AudioSource clip;

    private void Awake()
    {
        _position = transform.position;

        clip = GetComponent<AudioSource>();
    }

    public void OnUse()
    {
        if (_door.IsOpen)
        {
            _door.Close();
        }
        else
        {
            _door.Open(_position);
        }

        clip.Play();
    }
}
