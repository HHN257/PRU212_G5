using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float backgroundHeight = 20f;

    void Update()
    {
        // Scroll down
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // If the background goes off-screen at bottom, move it back to top
        if (transform.position.y <= -backgroundHeight)
        {
            Vector3 newPos = transform.position;
            newPos.y += backgroundHeight * 2f;
            transform.position = newPos;
        }
    }
}
