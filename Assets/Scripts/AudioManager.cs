using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

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
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            // If another AudioManager exists, destroy this one
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, make this AudioManager persistent
            DontDestroyOnLoad(gameObject);
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                musicSource.clip = mainMenu;
                break;
            case "WasteManagementGAME":
                musicSource.clip = mainGame;
                // Play the conveyor belt sound effect at the start of the WasteManagementGame
                SFXSource.clip = conveyorBelt; // Assuming conveyorBelt is your AudioClip for the conveyor belt sound
                SFXSource.Play(); // Play the sound effect
                break;
            default:
                // Optional: handle any default case or do nothing
                break;
        }
        musicSource.Play(); // Play the background music
    }

    private void Start()
    {
        SetMusicVolume(); // sets music volume at start
        SetSFXVolume(); // sets sfx volume at start
    }
    public void SetMusicVolume() 
    {
        float volume = MusicSlider.value; 
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()  
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    /**
    
    private void Start()
    {
        musicSource.clip = mainGame;
        musicSource.Play();
        SFXSource.clip = conveyorBelt;
        SFXSource.Play();
    }

    **/
    

    public void PlaySFX(AudioClip clip)
    {
        Debug.Log($"Attempting to play SFX: {clip.name}");
        SFXSource.PlayOneShot(clip);
    }
}