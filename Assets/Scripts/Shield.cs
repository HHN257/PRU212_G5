using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldDuration = 3f;

    private void OnEnable()
    {
        // Bắt đầu coroutine để tự hủy sau 3 giây
        StartCoroutine(ShieldDuration(shieldDuration));
    }

    private IEnumerator ShieldDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Kiểm tra xem object còn tồn tại không trước khi hủy
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    // Optional: Thêm visual effect khi shield sắp hết hạn
    private void Start()
    {
        StartCoroutine(BlinkBeforeDestroy());
    }

    private IEnumerator BlinkBeforeDestroy()
    {
        // Chờ đến 2 giây cuối trước khi hết hạn
        yield return new WaitForSeconds(shieldDuration - 1f);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Nhấp nháy trong 1 giây cuối
            for (int i = 0; i < 5; i++)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}