using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private bool _isActivated = false;
    public bool IsActivated { get => _isActivated; }



    public void Activate()
    {
        if (_isActivated) return;
        _isActivated = true;
        //Log the switch activation
        Debug.Log("Switch Activated");

        GameController _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _gameController.ActivateSwitch();

        // Play sound here
    }
}
