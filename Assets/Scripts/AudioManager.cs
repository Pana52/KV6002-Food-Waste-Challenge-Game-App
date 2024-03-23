using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource conveyorBeltSource;

    [Header("Mixers")]
    [SerializeField] private AudioMixer myMixer;

    [Header("Sliders")]
    private Slider MusicSlider;
    private Slider SFXSlider;

    [Header("Audio Clip")]
    public AudioClip mainMenu;
    public AudioClip mainGame;
    public AudioClip buttonPressed;
    public AudioClip buttonHover;
    public AudioClip conveyorBelt;
    public AudioClip binOpen;
    public AudioClip binClose;
    public AudioClip itemRight;
    public AudioClip itemWrong;
    public AudioClip levelComplete;
    public AudioClip levelFail;
    public AudioClip cerealBox;
    public AudioClip foodCan;
    public AudioClip paperFlip;
    public AudioClip carton;
    public AudioClip foilTray;
    public AudioClip plastic;
    public AudioClip cardboardBox;
    public AudioClip straw;
    public AudioClip toy;
    public AudioClip polystyrene;
    public AudioClip aerosol;
    public AudioClip battery;
    public AudioClip cigarette;
    public AudioClip plasticBag;
    public AudioClip canOpen;
    public AudioClip glassBottle;
    public AudioClip lightBulb;
    public AudioClip lighter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureAudioSourcesEnabled();

        StartCoroutine(WaitAndSetupSliders());

        switch (scene.name)
        {
            case "MainMenu":
                musicSource.clip = mainMenu;
                musicSource.volume = 0.20f;
                break;
            case "WasteManagementGAME":
                musicSource.clip = mainGame;
                musicSource.volume = 0.20f;
                // Play the conveyor belt sound effect at the start of the WasteManagementGame
                conveyorBeltSource.clip = conveyorBelt; // Assuming conveyorBelt is your AudioClip for the conveyor belt sound
                conveyorBeltSource.volume = 0.20f;
                conveyorBeltSource.loop = true;
                conveyorBeltSource.Play(); // Play the sound effect
                break;
            default:
                // Optional: handle any default case or do nothing
                break;
        }
        musicSource.Play();
    }

    public void SetupSlidersWhenMenuOpens()
    {
        MusicSlider = GameObject.FindGameObjectWithTag("MusicSlider")?.GetComponent<Slider>();
        Debug.Log($"Music Slider Found: {MusicSlider != null}", this);
        SFXSlider = GameObject.FindGameObjectWithTag("SFXSlider")?.GetComponent<Slider>();
        Debug.Log($"SFX Slider Found: {SFXSlider != null}", this);

        if (MusicSlider != null)
        {
            MusicSlider.onValueChanged.RemoveAllListeners(); // Avoid duplicates
            MusicSlider.onValueChanged.AddListener(SetMusicVolume); // Setup listener
            MusicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f); // Use saved volume
        }

        if (SFXSlider != null)
        {
            SFXSlider.onValueChanged.RemoveAllListeners(); // Avoid duplicates
            SFXSlider.onValueChanged.AddListener(SetSFXVolume); // Setup listener
            SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.75f); // Use saved volume
        }
    }

    private IEnumerator WaitAndSetupSliders()
    {
        yield return new WaitForEndOfFrame();
        SetupSlidersWhenMenuOpens(); // Attempt to setup sliders, if available
    }

    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }


    public void SetSFXVolume(float volume)
    {
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogWarning("Attempted to play a null AudioClip.");
            return;
        }

        if (!SFXSource.enabled)
        {
            Debug.LogWarning("SFXSource is disabled at play time.");
            SFXSource.enabled = true; // Ensure it's enabled
        }

        Debug.Log($"Playing SFX: {clip.name} at volume: {volume}");
        SFXSource.PlayOneShot(clip, volume);
    }



    // Ensure all AudioSource components are enabled
    private void EnsureAudioSourcesEnabled()
    {
        if (!musicSource.enabled) musicSource.enabled = true;
        if (!SFXSource.enabled) SFXSource.enabled = true;
        if (!conveyorBeltSource.enabled) conveyorBeltSource.enabled = true;

        Debug.Log("All AudioSource components are ensured to be enabled.");
    }

}