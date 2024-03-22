using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    // You can assign this through the Unity Inspector
    public GameObject targetGameObject;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void buttonClick()
    {
        if(audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonPressed, 0.35f);
        }
        else
        {
            Debug.LogWarning("AudioManager not found.Ensure there's a GameObject tagged with 'Audio' that has an AudioManager component.");
        }


        // Check if the targetGameObject is not null to avoid errors
        if (targetGameObject != null)
        {
            // Toggle the active state
            targetGameObject.SetActive(!targetGameObject.activeSelf);
        }
    }
}
