using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class HighScoreList
{
    public List<HighScoreEntry> scores = new List<HighScoreEntry>();
}

public class HighScoreManager : MonoBehaviour
{
    AudioManagerStartGame audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerStartGame>();
    }
    public GameObject inputPlayerName;
    public GameObject leaderboardPanel;
    public TMP_Text[] entryTexts;

    private HighScoreList highScoreList = new HighScoreList();
    private const string HighScoreKey = "HighScores";

    void Start()
    {
        LoadScores();
    }

    public void AddNewScore(string name, int score)
    {
        LoadScores(); // ← Add this line to ensure you load existing scores first!

        highScoreList.scores.Add(new HighScoreEntry { playerName = name, score = score });

        highScoreList.scores = highScoreList.scores
            .OrderByDescending(entry => entry.score)
            .Take(5)
            .ToList();

        SaveScores();
    }



    void SaveScores()
    {
        string json = JsonUtility.ToJson(highScoreList);
        PlayerPrefs.SetString(HighScoreKey, json);
        PlayerPrefs.Save();
    }

    void LoadScores()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            string json = PlayerPrefs.GetString(HighScoreKey);
            highScoreList = JsonUtility.FromJson<HighScoreList>(json);
        }
    }

    public void ShowLeaderboard()
    {
        audioManager.PlaySFX(audioManager.buttonClickSFX);
        LoadScores();

        leaderboardPanel.SetActive(true);
        for (int i = 0; i < entryTexts.Length; i++)
        {
            if (i < highScoreList.scores.Count)
            {
                var entry = highScoreList.scores[i];
                entryTexts[i].text = $"{i + 1}. {entry.playerName} - {entry.score}";
            }
            else
            {
                entryTexts[i].text = $"{i + 1}. ---";
            }
        }

        inputPlayerName.SetActive(false); // Hide player name input field
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    public void ResetLeaderboard()
    {
        PlayerPrefs.DeleteKey("HighScores");
        PlayerPrefs.Save();

        highScoreList.scores.Clear(); // ← Clear in-memory list too!

        // Optional: refresh leaderboard display
        for (int i = 0; i < entryTexts.Length; i++)
        {
            entryTexts[i].text = $"{i + 1}. ---";
        }

        Debug.Log("Leaderboard has been reset.");
    }

}
