using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class DisabledCam : MonoBehaviour
   {

    public StarterAssets.FirstPersonController firstPersonController;
    public MenuScript menuScript;


    void Start()
    {
        if (firstPersonController != null)
        {
            Debug.Log(firstPersonController.CinemachineCameraTarget);
        }
        else
        {
            Debug.LogError("FirstPersonController reference is not assigned in DisabledCam script.");
        }
    }

}