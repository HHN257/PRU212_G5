using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2"); // Use the exact scene name
    }

    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index); // Use index from Build Settings
    }
}
