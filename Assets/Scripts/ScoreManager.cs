using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    public int targetScore = 500; // <-- Set win score here
    public string nextSceneName = "Level2"; // <-- Name of next scene to load

    public GameObject winMessageUI; // Assign in Inspector
    public float delayBeforeLoad = 2f;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        score = Mathf.Max(0, score); // Prevent negative score
        scoreText.text = "Score: " + score;

        if (score >= targetScore)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        // Save score before scene change
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.Save();

        if (winMessageUI != null)
            winMessageUI.SetActive(true); // Show win message

        Invoke("LoadNextLevel", delayBeforeLoad); // Wait before loading next scene
    }


    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
