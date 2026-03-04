using UnityEngine;

public class CustomAudioPlayer : MonoBehaviour 
{
    public static CustomAudioPlayer Instance;

    public AudioSource bgMusic;
    public AudioClip menu;
    public AudioClip level;

    public AudioSource ballSFX;
    public AudioClip ballHit;
    public AudioClip ballRolling;

    public AudioSource miscFX;
    public AudioClip claps;

    public AudioSource UIFX;
    public AudioClip hoverUI;
    public AudioClip clickUI;

    float startmag;

    void Awake()
    {
        Instance = this;
        startmag = ballSFX.volume;
    }
    public void PlayAudio(string audio, float magnitude)
    {
        if (audio == "ballHit")
        {
            ballSFX.clip = ballHit;
            ballSFX.volume = startmag * magnitude;
            ballSFX.Play();
        }
    }
    public void PlayAudio(string audio)
    {
        if (audio == "menu")
        {
            bgMusic.clip = menu;
            bgMusic.Play();
        }
        else if (audio == "level")
        {
            bgMusic.clip = level;
            bgMusic.Play();
        }
        else if (audio == "claps")
        {
            miscFX.clip = claps;
            miscFX.Play();
        }
        else if (audio == "ballHit")
        {
            ballSFX.clip = ballHit;
            ballSFX.Play();
        }
        else if (audio == "ballRolling")
        {
            ballSFX.clip = ballRolling;
            ballSFX.Play();
        }
        else if (audio == "hoverUI")
        {
            UIFX.clip = hoverUI;
            UIFX.Play();
        }
        else if (audio == "clickUI")
        {
            UIFX.clip = clickUI;
            UIFX.Play();
        }
    }
}
