using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject PauseMenu;
    public GameObject PauseBG;
    public bool isPaused;

    // Reference to the AudioManager instance
    public AudioManager audioManager; // Assign this in the Unity Inspector


    void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogWarning("Failed to find AudioManager.");
            }
        }
        PauseMenu.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("Game Paused", this);
        PauseMenu.SetActive(true);
        PauseBG.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Ensure the AudioManager's sliders are set up when the pause menu is opened
        if (audioManager != null)
        {
            audioManager.SetupSlidersWhenMenuOpens();
        }
        else
        {
            Debug.LogWarning("AudioManager reference not set in pauseMenu script.");
        }

    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed", this);
        PauseMenu.SetActive(false);
        PauseBG.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

   public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }
}
