/// <summary>
/// Script Summary - Manages button interactions. Scene transtitions and highscore resets.
/// @Author - Luke Walpole + Others
/// </summary>

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private AudioManager audioManager; 

    private void Start()
    {
        // Find AudioManager + Basic error handling
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void Update()
    {
        //Reset highscore with R
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    // Coroutine to wait before scene load, allowing sound to play
    IEnumerator LoadSceneWithSound(string sceneName, float delay = 0.3f)
    {
        // Play button pressed sound + Basic error handling
        if (audioManager != null)
        {
            audioManager?.PlaySFX(audioManager.buttonPressed, 0.30f);
        }

        // Wait for sound to play then load scene
        yield return new WaitForSeconds(delay);
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
    }

    public void BackToMain()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneWithSound("MainMenu"));
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