using UnityEngine;

public class AudioManagerLvl1 : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip lvl1Music;
    public AudioClip shoot;
    public AudioClip crash;
    public AudioClip hit;
    public AudioClip powerUp;
    public AudioClip gameOver;

    private void Start()
    {
        musicSource.clip = lvl1Music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
