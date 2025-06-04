using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 2f;
    public int damage = 1;
    
    private float currentFallSpeed;
    private Vector3 rotationSpeeds;

    private void Start()
    {
        currentFallSpeed = fallSpeed;
        rotationSpeeds = new Vector3(
            Random.Range(-50f, 50f),
            Random.Range(-50f, 50f),
            Random.Range(-200f, 200f)
        );
    }

    private void Update()
    {
        transform.Translate(Vector3.down * currentFallSpeed * Time.deltaTime);
        
        transform.Rotate(rotationSpeeds * Time.deltaTime);
        
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }
} 