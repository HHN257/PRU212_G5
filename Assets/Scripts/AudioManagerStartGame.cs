using UnityEngine;

public class AudioManagerStartGame : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip startGameMusic;
    public AudioClip buttonClickSFX;

    private void Start()
    {
        musicSource.clip = startGameMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
