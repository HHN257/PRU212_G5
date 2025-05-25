using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    //public TMP_Text highScoreText;
    public HighScoreManagerOver highScoreManager;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        string playerName = StartGame.playerName;

        if (highScoreManager != null && !string.IsNullOrEmpty(playerName))
        {
            highScoreManager.AddNewScore(playerName, finalScore);
            highScoreManager.ShowLeaderboard(); // 💡 Refresh display
        }

        //highScoreText.text = $"Score: {finalScore}";
    }
}
