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
    public GameObject shield;
    private bool hasShield = false;
    private Vector3 startPosition;

    [Header("Shield Settings")]
    public float shieldDuration = 5f;
    public Vector3 shieldOffset = Vector3.zero; // Offset position cho shield

    [Header("Reset Settings")]
    public bool resetPositionOnHit = false; // Option to reset position or not
    public Vector3 customResetPosition = new Vector3(0, -3, 0); // Custom reset position

    void Start()
    {
        // Lưu vị trí ban đầu của player
        startPosition = transform.position;

        if (shield != null)
        {
            shield.SetActive(false);
        }

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
        if (other.CompareTag("Enemy") && !isInvincible && !hasShield)
        {
            audioManager.PlaySFX(audioManager.crash); // Phát âm thanh va chạm
            TakeDamage(1);
            ScoreManager.instance.AddScore(-30); // Trừ điểm khi va chạm

            // Thay vì ResetPlayer(), chỉ activate shield và invincibility
            StartCoroutine(InvincibilityCoroutine());
        }
        else if (other.CompareTag("AtkSpdBuff"))
        {
            audioManager.PlaySFX(audioManager.powerUp); // Play power-up sound
            IncreaseFireRate(0.1f, 3f); // Boost fire rate for 5 seconds
            Destroy(other.gameObject); // Remove the power-up
        }
    }

    // Chỉ sử dụng khi thực sự cần reset vị trí (ví dụ: rơi xuống hố)
    public void ResetPlayerPosition()
    {
        // Sử dụng custom reset position thay vì startPosition
        Vector3 resetPos = resetPositionOnHit ? customResetPosition : startPosition;

        Debug.Log($"Resetting player to position: {resetPos}");
        transform.position = resetPos;

        // Reset physics nếu có
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        hasShield = true;

        if (shield != null)
        {
            shield.SetActive(true);
          
        }

        // Blink effect during invincibility
        StartCoroutine(BlinkSprite());

        yield return new WaitForSeconds(shieldDuration);

        if (shield != null)
        {
            shield.SetActive(false);

        }

        isInvincible = false;
        hasShield = false;
    }

    void TakeDamage(int damage)
    {
        if (isInvincible || hasShield) return; // Prevent damage if invincible or has shield

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
    }

    IEnumerator BlinkSprite()
    {
        // Blink effect for visual feedback
        for (int i = 0; i < 10; i++) // Increased blink count
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void GameOver()
    {
        audioManager.PlaySFX(audioManager.gameOver); // Play game over sound
        Debug.Log("Game Over!");
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

    // Method to manually activate shield (for testing or power-ups)
    public void ActivateShield(float duration = 0f)
    {
        if (duration <= 0f)
            duration = shieldDuration;

        StartCoroutine(InvincibilityCoroutine());
    }

    // Debug method to check current state
    void OnGUI()
    {
        if (Application.isEditor) // Only show in editor
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"Position: {transform.position}");
            GUI.Label(new Rect(10, 30, 200, 20), $"Health: {currentHealth}");
            GUI.Label(new Rect(10, 50, 200, 20), $"Has Shield: {hasShield}");
            GUI.Label(new Rect(10, 70, 200, 20), $"Invincible: {isInvincible}");
        }
    }
}