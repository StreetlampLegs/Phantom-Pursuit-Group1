using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject OptionMenu;
    public GameObject Music;
    public GameObject PauseBackground;

    private bool PauseMenuIsShowing = false;
    private bool OptionMenuIsShowing = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuIsShowing = PauseMenu.activeInHierarchy;
            OptionMenuIsShowing = OptionMenu.activeInHierarchy;

            if(!PauseMenuIsShowing && !OptionMenuIsShowing)
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
        Music.SetActive(true);
        PauseBackground.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void Resume()
    {
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        Music.SetActive(false);
        PauseBackground.SetActive(false);
        Time.timeScale = 1f;
    }
    void Back()
    {
        PauseMenu.SetActive(true );
        OptionMenu.SetActive(false );
        PauseBackground.SetActive(true);
        Music.SetActive(true);
        Time.timeScale = 0f;
    }
}
