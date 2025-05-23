using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text highScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        highScoreText.text = "High Score: " + highScore;
    }
}
