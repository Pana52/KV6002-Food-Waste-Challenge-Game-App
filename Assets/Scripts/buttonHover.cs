/// <summary>
/// Script Summary - Plays hover sound effect when the mouse cursor hovers over buttons.
/// @Author - Luke Walpole
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    private AudioManager audioManager;

    void Start()
    {
        // Find AudioManager + Basic error handling
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.buttonHover, 0.35f);
        }
    }
}