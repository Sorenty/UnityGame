using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip musicClip;
    public AudioClip chopClip;
    public AudioClip pickupClip;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                musicSource = sources[0];
                sfxSource = sources[1];
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
            musicSource.Play();
        }
    }

    public void PlayPickup()
    {
        if (sfxSource != null && pickupClip != null)
            sfxSource.PlayOneShot(pickupClip);
    }

    public void PlayChop()
    {
        if (sfxSource != null && chopClip != null)
            sfxSource.PlayOneShot(chopClip);
    }

    public void SetMusicVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
}