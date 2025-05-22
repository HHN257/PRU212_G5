using UnityEngine;

public class StarPoint : MonoBehaviour
{
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
