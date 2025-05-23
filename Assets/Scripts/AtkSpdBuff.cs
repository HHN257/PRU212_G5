using UnityEngine;

public class AtkSpdBuff : MonoBehaviour
{
    public float fallSpeed = 2f;
    void Update()
    {
        // Move down every frame
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Destroy star if it goes off-screen
        Destroy(gameObject);
    }
}
