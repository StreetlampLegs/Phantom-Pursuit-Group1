using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _useText;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private float _maxUseDistance = 5f;
    [SerializeField]
    private LayerMask _useLayer;

    public void OnUse()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit doorHit, _maxUseDistance, _useLayer))
        {
            if (doorHit.collider.TryGetComponent(out Door door))
            {
                if (!door.AllowDirectInteraction) return;
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }

            }
        }

        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit buttonHit, _maxUseDistance, _useLayer))
        {
            if (buttonHit.collider.TryGetComponent(out DoorButton button))
            {
                button.OnUse();
            }
        }

        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit switchHit, _maxUseDistance, _useLayer))
        {
            if (switchHit.collider.TryGetComponent(out Switch switchObject))
            {
                switchObject.Activate();
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit doorHit, _maxUseDistance, _useLayer) && doorHit.collider.TryGetComponent<Door>(out Door door))
        {
            if (!door.AllowDirectInteraction) return;
            if (door.IsOpen)
            {
                _useText.SetText("Close \"E\"");
            }
            else
            {
                _useText.SetText("Open \"E\"");
            }
            _useText.gameObject.SetActive(true);
            _useText.transform.position = doorHit.point - (doorHit.point - _camera.position).normalized * 0.5f;
            _useText.transform.rotation = Quaternion.LookRotation((doorHit.point - _camera.position).normalized);
        }
        else if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit buttonHit, _maxUseDistance, _useLayer) && buttonHit.collider.TryGetComponent<DoorButton>(out DoorButton button))
        {
            _useText.SetText("Use \"E\"");
            _useText.gameObject.SetActive(true);
            _useText.transform.position = buttonHit.point - (buttonHit.point - _camera.position).normalized * 0.5f;
            _useText.transform.rotation = Quaternion.LookRotation((buttonHit.point - _camera.position).normalized);
        }
        else if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit switchHit, _maxUseDistance, _useLayer) && switchHit.collider.TryGetComponent<Switch>(out Switch switchObject))
        {
            if (!switchObject.IsActivated)
            {
                _useText.SetText("Use \"E\"");
            }
            else
            {
                _useText.SetText("Activated");
            }
            _useText.gameObject.SetActive(true);
            _useText.transform.position = switchHit.point - (switchHit.point - _camera.position).normalized * 0.5f;
            _useText.transform.rotation = Quaternion.LookRotation((switchHit.point - _camera.position).normalized);
        }
        else
        {
            _useText.gameObject.SetActive(false);
        }
    }
}
