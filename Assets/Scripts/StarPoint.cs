using UnityEngine;

public class StarPoint : MonoBehaviour
{
    AudioManagerLvl1 audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLvl1>();
    }

    public float fallSpeed = 2f;
    public int scoreValue = 5;

    void Update()
    {
        // Move down every frame
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.powerUp); // Play power-up sound
            ScoreManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Destroy star if it goes off-screen
        Destroy(gameObject);
    }
}
