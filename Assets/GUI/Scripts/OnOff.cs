/// <summary>
/// Script Summary - Toggles active state of button on and off when clicked.
/// @Author - Luke Walpole + Others
/// </summary>

using UnityEngine;

public class OnOff : MonoBehaviour
{
    public GameObject targetGameObject;
    private AudioManager audioManager;

    private void Start()
    {
        // Find AudioManager + Basic error handling
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    }

    // Toggle active state and play sound of button click
    public void buttonClick()
    {
        if(audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonPressed, 0.30f);
        }

        if (targetGameObject != null)
        {
            targetGameObject.SetActive(!targetGameObject.activeSelf);
        }
    }
}