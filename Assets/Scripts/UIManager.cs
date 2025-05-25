using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject leaderboardPanel; // Reference to your leaderboard panel GameObject
    public GameObject background; // Reference to your background GameObject
    public GameObject inputPlayerName; // Reference to your player name input field

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
        inputPlayerName.SetActive(false); // Hide player name input field
    }

    public void HideInstructions()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX); // Play button click sound
        instructionPanel.SetActive(false);
        background.SetActive(true); // Show background again
        inputPlayerName.SetActive(true);
    }

    public void ShowLeaderboard()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX); // Play button click sound
        leaderboardPanel.SetActive(true);
        background.SetActive(false); // Hide background
        inputPlayerName.SetActive(false); // Hide player name input field
    }

    public void HideLeaderboard()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX); // Play button click sound
        leaderboardPanel.SetActive(false);
        background.SetActive(true); // Show background again
        inputPlayerName.SetActive(true);
    }
}
