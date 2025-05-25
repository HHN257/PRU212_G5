using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Required for TMP_InputField
using System.Collections;

public class StartGame : MonoBehaviour
{
    AudioManagerStartGame audioManager;

    [Header("UI References")]
    public Button startButton;
    public TMP_InputField playerNameInput; // ← Assign this in Inspector!

    [Header("Blink Settings")]
    public float delayBeforeStart = 1.5f;
    public float blinkSpeed = 0.2f;
    public int blinkCount = 4;

    public static string playerName = "Player"; // Static for global use

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerStartGame>();
    }

    public void BeginGame()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX); // Play button click sound
        startButton.interactable = false; // prevent double clicks

        // Store the name globally
        playerName = string.IsNullOrEmpty(playerNameInput.text) ? "Player" : playerNameInput.text;

        StartCoroutine(BlinkAndLoad());
    }

    private IEnumerator BlinkAndLoad()
    {
        Image buttonImage = startButton.GetComponent<Image>();
        Color originalColor = buttonImage.color;

        for (int i = 0; i < blinkCount; i++)
        {
            buttonImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // transparent
            yield return new WaitForSeconds(blinkSpeed);
            buttonImage.color = originalColor; // visible
            yield return new WaitForSeconds(blinkSpeed);
        }

        yield return new WaitForSeconds(delayBeforeStart);

        SceneManager.LoadScene("Level1");
    }
}
