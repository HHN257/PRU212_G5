using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

[System.Serializable]
public class HighScoreEntry2
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class HighScoreList2
{
    public List<HighScoreEntry2> scores = new List<HighScoreEntry2>();
}

public class HighScoreManagerOver : MonoBehaviour
{
    AudioManagerStartGame audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerStartGame>();
    }

    public TMP_Text[] entryTexts;

    private HighScoreList2 highScoreList = new HighScoreList2();
    private const string HighScoreKey = "HighScores";

    void Start()
    {
        LoadScores();
    }

    public void AddNewScore(string name, int score)
    {
        LoadScores(); // Load existing scores

        highScoreList.scores.Add(new HighScoreEntry2 { playerName = name, score = score });

        // Sort and keep only top 5
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
            highScoreList = JsonUtility.FromJson<HighScoreList2>(json);
        }
    }

    public void ShowLeaderboard()
    {
        LoadScores();

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
    }
}
