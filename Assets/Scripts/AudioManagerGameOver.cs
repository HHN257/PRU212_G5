using UnityEngine;

public class AudioManagerGameOver : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip gameOverMusic;
    public AudioClip buttonClickSFX;

    private void Start()
    {
        musicSource.clip = gameOverMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
