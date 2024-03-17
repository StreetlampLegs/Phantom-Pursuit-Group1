using UnityEngine;
using UnityEngine.UI;



public class MenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject OptionMenu;
    public GameObject Mixer;
    public GameObject PauseBackground;
    public StarterAssets.FirstPersonController firstPersonController;



    private bool PauseMenuIsShowing = false;
    private bool OptionMenuIsShowing = false;
   // private bool MixerIsShowing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuIsShowing = PauseMenu.activeInHierarchy;
            OptionMenuIsShowing = OptionMenu.activeInHierarchy;

            if (!PauseMenuIsShowing && !OptionMenuIsShowing)
            {
                Pause();
            }
            else if (PauseMenuIsShowing)
            {
                Resume();
            }
            else if (OptionMenuIsShowing)
            {
                Back();
            }
        }
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        OptionMenu.SetActive(false);
        Mixer.SetActive(true);
        PauseBackground.SetActive(true);
        Time.timeScale = 0.0f;
        //Cursor.lockState = CursorLockMode.Confined;
        //firstPersonController.SetActive(false);

    }


    void Resume()
    {
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        Mixer.SetActive(false);
        PauseBackground.SetActive(false);
        Time.timeScale = 1f;
       // Cursor.lockState = CursorLockMode.None;
        //firstPersonController.SetActive(true);
    }
    void Back()
    {
        PauseMenu.SetActive(true);
        OptionMenu.SetActive(false);
        PauseBackground.SetActive(true);
        Mixer.SetActive(true);
        Time.timeScale = 0f;
        // firstPersonController.SetActive(false);
    }

}
