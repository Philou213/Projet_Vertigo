using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    public Button btnBridge;
    public Button btnRoof;
    public Button btnPlatform;
    public Button btnGarden;

    private string currentLevel = "Jardin";

    void Start()
    {
        UpdateButtons();
    }

    public void SelectLevel(string levelName)
    {
        currentLevel = levelName;
        Debug.Log("Niveau sélectionné : " + levelName);
        UpdateButtons();
    }

    void UpdateButtons()
    {
        btnBridge.interactable = currentLevel != "Bridge";
        btnRoof.interactable = currentLevel != "Roof";
        btnPlatform.interactable = currentLevel != "Platform";
        btnGarden.interactable = currentLevel != "Jardin";
    }
}
