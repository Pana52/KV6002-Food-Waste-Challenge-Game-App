using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{

    private AudioManager audioManager; // Reference to AudioManager

    private void Start()
    {
        // Find and store a reference to the AudioManager on start
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Reset highscore.
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    // Coroutine to wait a little before scene load, allowing sound to play
    IEnumerator LoadSceneWithSound(string sceneName, float delay = 0.3f)
    {
        // Play button pressed sound
        audioManager?.PlaySFX(audioManager.buttonPressed, 0.30f);

        // Wait for the sound to play before loading the scene
        yield return new WaitForSeconds(delay);

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    public void GoToScene()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 4);
        PlayerPrefs.Save();
        StartCoroutine(LoadSceneWithSound("WasteManagementGAME")); // Use the correct scene name or index
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application closed");
    }

    public void BackToMain()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneWithSound("MainMenu")); // Assuming "MainMenu" is the name of your main menu scene
    }

    public void LoadLevel1()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    public void LoadLevel3()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 3);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
