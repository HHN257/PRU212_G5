using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject background; // Reference to your background GameObject

    AudioManagerStartGame audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerStartGame>();
    }

    public void ShowInstructions()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX); // Play button click sound
        instructionPanel.SetActive(true);
        background.SetActive(false); // Hide background
    }

    public void HideInstructions()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX); // Play button click sound
        instructionPanel.SetActive(false);
        background.SetActive(true); // Show background again
    }
}
