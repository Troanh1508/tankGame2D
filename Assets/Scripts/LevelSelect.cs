using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    private string saveLocation;
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++) {
            int level = i+1;
            levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(level.ToString());
        }
        saveLocation = Path.Combine(Application.persistentDataPath, "tankGameData.json");
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            for (int i = 0; i < levelButtons.Length; i++) {
                if (i + 1 > saveData.highestSceneIndex) {
                    levelButtons[i].interactable = false;
                }
            }
        }
        else {
            for (int i = 1; i< levelButtons.Length; i++){
                levelButtons[i].interactable = false;
            }
        }
    }
    public void LoadLevel(int levelIndex){
        SceneManager.LoadScene(levelIndex);
    }
}
