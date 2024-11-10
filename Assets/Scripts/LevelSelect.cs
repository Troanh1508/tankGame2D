using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    private string saveLocation;
    void Start()
    {
        int currentLevel = 1;
        saveLocation = Path.Combine(Application.persistentDataPath, "tankGameData.json");
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            currentLevel = saveData.currentSceneIndex;
            for (int i = 0; i< levelButtons.Length; i++){
                if (i + 1 > currentLevel)
                levelButtons[i].interactable = false;
            }
        }
        else
        Debug.Log("No save data found");
    }
}
