using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    AudioManagerLvl1 audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLvl1>();
    }

    public GameObject[] hearts; // Assign HP, HP (1), HP (2) in Inspector
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Coroutine fireRateCoroutine;
    public float fireRate = 0.5f; // Time in seconds between shots
    private float nextFireTime = 0f;
    public int maxHealth = 3;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, moveY, 0f);

        ClampToScreen();

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set the next fire time
        }
    }

    void Shoot()
    {
        audioManager.PlaySFX(audioManager.shoot); // Play shooting sound
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    void ClampToScreen()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            audioManager.PlaySFX(audioManager.crash); // Play hit sound
            TakeDamage(1);
            ScoreManager.instance.AddScore(-30); // Deduct score on hit
        }
        else if (other.CompareTag("AtkSpdBuff"))
        {
            audioManager.PlaySFX(audioManager.powerUp); // Play power-up sound
            IncreaseFireRate(0.1f, 3f); // Boost fire rate for 5 seconds
            Destroy(other.gameObject); // Remove the power-up
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Hide the corresponding heart
        if (currentHealth >= 0 && currentHealth < hearts.Length)
        {
            hearts[currentHealth].SetActive(false);
        }

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(BlinkSprite());
        }
    }


    IEnumerator BlinkSprite()
    {
        isInvincible = true;

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    void GameOver()
    {
        audioManager.PlaySFX(audioManager.gameOver); // Play game over sound
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
        PlayerPrefs.SetInt("FinalScore", ScoreManager.instance.score);
        SceneManager.LoadScene("GameOver");

    }

    public void IncreaseFireRate(float amount, float duration)
    {
        audioManager.PlaySFX(audioManager.powerUp);

        // Only stop the fire rate boost coroutine
        if (fireRateCoroutine != null)
            StopCoroutine(fireRateCoroutine);

        fireRateCoroutine = StartCoroutine(FireRateBoost(amount, duration));
    }


    IEnumerator FireRateBoost(float amount, float duration)
    {
        float originalFireRate = fireRate;
        fireRate = Mathf.Max(0.1f, fireRate - amount); // Prevent too-fast fire rate

        yield return new WaitForSeconds(duration);

        fireRate = originalFireRate; // Restore original fire rate
    }

}
