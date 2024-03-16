using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip mainGame;
    public AudioClip conveyorBelt;
    public AudioClip binOpen;
    public AudioClip binClose;
    public AudioClip itemRight;
    public AudioClip itemWrong;
    public AudioClip levelComplete;
    public AudioClip levelFail;
    public AudioClip newspaper;
    public AudioClip milkBottle;

    private void Start()
    {
        musicSource.clip = mainGame;
        musicSource.Play();
        SFXSource.clip = conveyorBelt;
        SFXSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}