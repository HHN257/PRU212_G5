using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject historyPanel;
    public GameObject background; // Reference to your background GameObject


    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
        background.SetActive(false); // Hide background
    }

    public void HideInstructions()
    {
        instructionPanel.SetActive(false);
        background.SetActive(true); // Show background again
    }

   
}
