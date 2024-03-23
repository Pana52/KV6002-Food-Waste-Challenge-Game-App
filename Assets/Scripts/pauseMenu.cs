/// <summary>
/// Script Summary - Manages pause menu and pause/resume state. 
/// @Author - Luke Walpole + Others
/// </summary>

using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseBG;
    public bool isPaused;

    public AudioManager audioManager; 

    void Start()
    {
        // Find AudioManager + Basic error handling
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        // Toggle pause with P
        if(Input.GetKeyUp(KeyCode.P)) 
        {
            if(isPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        PauseBG.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Play button press sound, set up sliders when pause menu opens
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonPressed, 0.30f);
            audioManager.SetupSlidersWhenMenuOpens();
        }
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        PauseBG.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonPressed, 0.30f); 
        }
    }

   public void GoToMainMenu()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonPressed, 0.30f); 
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}