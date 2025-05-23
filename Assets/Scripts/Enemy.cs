using UnityEngine;

public class Enemy : MonoBehaviour
{
    AudioManagerLvl1 audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLvl1>();
    }

    public float speed = 2f;
    public int scoreValue = 1;

    public int maxHealth = 2; // Can be set per enemy in the Inspector
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            audioManager.PlaySFX(audioManager.hit); // Play crash sound
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            ScoreManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
