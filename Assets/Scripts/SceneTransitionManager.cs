/// <summary>
/// Script Summary - Manages scene transitions. Adds a delay. This was created to allow time for button click audio
///                  to play before the scene transitioned. 
/// @Author - Luke Walpole + ChatGPT 
/// AI Usage - ChatGPT was used to help come up with a solution to fix the problem the button audio not playing as 
///            well as help debug my code that wasn't working.
/// </summary>

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    private AudioManager audioManager;
    public float delayBeforeSceneLoads = 0.5f; 

    private void Start()
    {
        // Find AudioManager + Basic error handling
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    }

    public void LoadSceneWithDelay(string sceneName)
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonPressed, 0.30f);
        }
        // Coroutine loads scene after delay
        StartCoroutine(DelayedSceneLoad(sceneName, delayBeforeSceneLoads));
    }

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        // Wait for the delay then load the scene
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void exitApp()
    {
        Application.Quit();
    }
}