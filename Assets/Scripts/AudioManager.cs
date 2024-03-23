/// <summary>
/// Script Summary - Manages all audio functionaility, i.e. music, sfx, volume, sliders.
///                  Singleton structure so only one instance of audiomanager in the game.
///                  BG music, transitions, user prefs for audio volume.
/// @Author - Luke Walpole
/// @Generated - ChatGPT
/// @Generated - ChatGPT was used to help come up with the awake(), setupsliderswhenmenuopens(), 
///              waitandsetupsliders(), ensureaudiosourcesenabled(), and stopmusicandconveyorbelt().
///              Also helped me debug my code that wasn't working. 
/// </summary>

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

    // Single instance of Audiomanager
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
    }

    // Adjusts audio sources when new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureAudioSourcesEnabled();

        StartCoroutine(WaitAndSetupSliders());

        // Stop the conveyor belt audio if not in the main game scene
        if (scene.name != "WasteManagementGAME" && conveyorBeltSource.isPlaying)
        {
            conveyorBeltSource.Stop();
        }

        switch (scene.name)
        {
            case "MainMenu":
                musicSource.clip = mainMenu;
                musicSource.volume = 0.20f;
                break;
            case "WasteManagementGAME":
                musicSource.clip = mainGame;
                musicSource.volume = 0.20f;
                conveyorBeltSource.clip = conveyorBelt; 
                conveyorBeltSource.volume = 0.20f;
                conveyorBeltSource.loop = true;
                conveyorBeltSource.Play(); 
                break;
            default:
                break;
        }
        musicSource.Play();
    }

    public void SetupSlidersWhenMenuOpens()
    {
        MusicSlider = GameObject.FindGameObjectWithTag("MusicSlider")?.GetComponent<Slider>();
        SFXSlider = GameObject.FindGameObjectWithTag("SFXSlider")?.GetComponent<Slider>();

        if (MusicSlider != null)
        {
            MusicSlider.onValueChanged.RemoveAllListeners(); 
            MusicSlider.onValueChanged.AddListener(SetMusicVolume); 
            MusicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f); 
        }

        if (SFXSlider != null)
        {
            SFXSlider.onValueChanged.RemoveAllListeners(); 
            SFXSlider.onValueChanged.AddListener(SetSFXVolume); 
            SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.75f); 
            SetSFXVolume(SFXSlider.value); 
        }
    }

    private IEnumerator WaitAndSetupSliders()
    {
        yield return new WaitForEndOfFrame();
        SetupSlidersWhenMenuOpens(); 
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
            return;
        }

        if (!SFXSource.enabled)
        {
            SFXSource.enabled = true; 
        }
        SFXSource.PlayOneShot(clip, volume);
    }

    private void EnsureAudioSourcesEnabled()
    {
        if (!musicSource.enabled) musicSource.enabled = true;
        if (!SFXSource.enabled) SFXSource.enabled = true;
        if (!conveyorBeltSource.enabled) conveyorBeltSource.enabled = true;
    }

    public void StopMusicAndConveyorBelt()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        if (conveyorBeltSource.isPlaying)
        {
            conveyorBeltSource.Stop();
        }
    }
}