using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedButtonActivation : MonoBehaviour
{
    public float delayInSeconds = 5f; // Adjust this value to set the delay

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false; // Initially disable the button

        StartCoroutine(ActivateButtonAfterDelay());
    }

    IEnumerator ActivateButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        button.interactable = true; // Enable the button after the delay
    }
}

