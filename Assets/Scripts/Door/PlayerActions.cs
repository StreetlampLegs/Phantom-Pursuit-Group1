using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayer;

    public void OnUse()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit doorHit, MaxUseDistance, UseLayer))
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

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit buttonHit, MaxUseDistance, UseLayer))
        {
            if (buttonHit.collider.TryGetComponent(out DoorButton button))
            {
                button.OnUse();
            }
        }
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit doorHit, MaxUseDistance, UseLayer) && doorHit.collider.TryGetComponent<Door>(out Door door))
        {
            if (!door.AllowDirectInteraction) return;
            if (door.IsOpen)
            {
                UseText.SetText("Close \"E\"");
            }
            else
            {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = doorHit.point - (doorHit.point - Camera.position).normalized * 0.5f;
            UseText.transform.rotation = Quaternion.LookRotation((doorHit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit buttonHit, MaxUseDistance, UseLayer) && buttonHit.collider.TryGetComponent<DoorButton>(out DoorButton button))
        {
            UseText.SetText("Use \"E\"");
            UseText.gameObject.SetActive(true);
            UseText.transform.position = buttonHit.point - (buttonHit.point - Camera.position).normalized * 0.5f;
            UseText.transform.rotation = Quaternion.LookRotation((buttonHit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
