using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private bool _isActivated = false;
    public bool IsActivated { get => _isActivated; }

    public void Activate()
    {
        _isActivated = true;
        //Log the switch activation
        Debug.Log("Switch Activated");
    }
}
