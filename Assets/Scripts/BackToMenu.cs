using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BackToMenu : MonoBehaviour
{
    AudioManagerGameOver audioManagerGameOver;
    private void Awake()
    {
        audioManagerGameOver = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerGameOver>();
    }

    public Button menuButton;
    public float delayBeforeStart = 1.5f;
    public float blinkSpeed = 0.2f;
    public int blinkCount = 4;

    public void BackToMainMenu()
    {
        audioManagerGameOver.PlaySFX(audioManagerGameOver.buttonClickSFX); // Play button click sound
        menuButton.interactable = false; // prevent double clicks
        StartCoroutine(BlinkAndLoad());
    }

    private IEnumerator BlinkAndLoad()
    {
        Image buttonImage = menuButton.GetComponent<Image>();
        Color originalColor = buttonImage.color;

        for (int i = 0; i < blinkCount; i++)
        {
            buttonImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // transparent
            yield return new WaitForSeconds(blinkSpeed);
            buttonImage.color = originalColor; // visible
            yield return new WaitForSeconds(blinkSpeed);
        }

        yield return new WaitForSeconds(delayBeforeStart);
        SceneManager.LoadScene("StartGame");
    }
}
