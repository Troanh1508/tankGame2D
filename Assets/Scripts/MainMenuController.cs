using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button newGameButton;
    public Button loadGameButton;
    public Button exitButton;
    private string saveLocation;

    private void Start()
    {
        // Add listeners to the buttons
        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(OnStartGameClicked);
        }

        saveLocation = Path.Combine(Application.persistentDataPath, "tankGameData.json");

        if (loadGameButton != null)
        {
            if (!File.Exists(saveLocation))
            loadGameButton.interactable = false;
            else
            loadGameButton.onClick.AddListener(OnLoadGameClicked);
        }
        else
        Debug.Log("No save data found");

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitClicked);
        }
        
    }

    private void OnExitClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void OnStartGameClicked()
    {
        // Clear data
        GameManager.Instance.ClearSaveData();
        // Load the game scene
        SceneManager.LoadScene(1);
    }

    private void OnLoadGameClicked()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            SceneManager.LoadScene(saveData.currentSceneIndex);
        }
        else
        Debug.Log("No save data found");
        
    }    
}
