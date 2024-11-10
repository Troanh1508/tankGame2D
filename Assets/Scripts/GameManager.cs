using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public int autoShootMode = 0;
    
    public static GameManager Instance
     {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    private string saveLocation;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        saveLocation = Path.Combine(Application.persistentDataPath, "tankGameData.json");
        Debug.Log("Save Location: " + saveLocation);
    }
    public void SaveGame(){
        
        if (GameObject.FindGameObjectWithTag("Player"))
        {
                SaveData newSaveData = new()
            {
                currentSceneIndex = SceneManager.GetActiveScene().buildIndex, // Save the current scene index
                highestSceneIndex = SceneManager.GetActiveScene().buildIndex
            };
            if (File.Exists(saveLocation)) {
                SaveData oldSaveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
                // Check if player has unlocked a higher level
                if (oldSaveData.highestSceneIndex >= newSaveData.highestSceneIndex){
                    newSaveData.highestSceneIndex = oldSaveData.highestSceneIndex;
                }
            }
            File.WriteAllText(saveLocation, JsonUtility.ToJson(newSaveData));
            Debug.Log("Game saved at " + saveLocation);
        }
        else
        Debug.Log("Player not found");
        
    }

    public void ClearSaveData()
    {
        // Check if the file exists before attempting to delete it
        if (File.Exists(saveLocation))
        {
            File.Delete(saveLocation);
            Debug.Log("Save data file deleted.");
        }
        else
        {
            Debug.Log("No save data file found to delete.");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        return;

        resetPlayerStats();
        SaveGame();
    }

    private void resetPlayerStats()
    {
        if (GameObject.FindGameObjectWithTag("Player")){
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TankMover>().movementData.ResetValues();
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Turret>().turretData.ResetValues();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int currentSceneIndex;
    public int highestSceneIndex;
}
