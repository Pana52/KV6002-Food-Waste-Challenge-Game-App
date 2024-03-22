using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    private AudioManager audioManager;
    public float delayBeforeSceneLoads = 0.5f; // Adjust this value based on your sound clip length

    private void Start()
    {
        // Find the AudioManager in the scene
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void LoadSceneWithDelay(string sceneName)
    {
        if (audioManager != null)
        {
            Debug.Log("Playing button pressed SFX");
            // Play the buttonPressed sound effect
            audioManager.PlaySFX(audioManager.buttonPressed, 0.35f);
        }
        else
        {
            Debug.Log("AudioManager not found");
        }

        // Start the coroutine to delay the scene loading
        StartCoroutine(DelayedSceneLoad(sceneName, delayBeforeSceneLoads));
    }

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    public void exitApp()
    {
        Application.Quit();
    }
}
