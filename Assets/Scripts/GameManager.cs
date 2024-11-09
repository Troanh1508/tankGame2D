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

    public bool loadFromMainMenu;
    public int autoShootMode;

    GameObject fixedAimJoystick, floatingAimJoystick, shootButton;


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
                SaveData saveData = new()
            {
                playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
                playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Damagable>().Health,
                // playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TankMover>().movementData.speed,
                ActiveEnemyNames = CheckActiveEnemies(),
                ActiveBuffNames = CheckActiveBuffs(),
                currentSceneIndex = SceneManager.GetActiveScene().buildIndex // Save the current scene index

            };
            File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
            Debug.Log("Game saved at " + saveLocation);
        }
        else
        Debug.Log("Player not found");
        
    }

    public void LoadFromMainMenu(){
        if (File.Exists(saveLocation)){
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Damagable>().Health = saveData.playerHealth;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            List<string> savedEnemies = saveData.ActiveEnemyNames;

            // Destroy enemies not in the saved list
            foreach (var enemy in enemies)
            {
                if (!savedEnemies.Contains(enemy.name))
                {
                    Destroy(enemy);
                }
            }

            GameObject[] buffs = GameObject.FindGameObjectsWithTag("Buff");
            List<string> savedBuffs = saveData.ActiveBuffNames;
            // Load saved buffs to player then Destroy
            foreach (var buff in buffs)
            {
                if (!savedBuffs.Contains(buff.name))
                {
                    ApplyBuff applyBuff = buff.GetComponent<ApplyBuff>();
                    applyBuff.buff.Apply(GameObject.FindGameObjectWithTag("Player"));
                    Destroy(buff);
                }
            }

            Debug.Log("Game loaded.");
        }
        else
        Debug.Log("No save data found");
    }

    public void LoadNextLevel()
    {
        if (File.Exists(saveLocation)){
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            // GameObject.FindGameObjectWithTag("Player").GetComponent<Damagable>().Health = saveData.playerHealth;

            Debug.Log("Game loaded.");
        }
        else
        Debug.Log("No save data found");
    }

    public List<string> CheckActiveEnemies()
    {
        // Find all GameObjects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<string> ActiveEnemyNames = new List<string>();
        int ActiveCount = 0;

        foreach (GameObject enemy in enemies)
        {
            // Check if the enemy GameObject is active
            if (enemy.activeInHierarchy)
            {
                ActiveCount++;
                ActiveEnemyNames.Add(enemy.name);
            }
        }

        Debug.Log("Active enemies: " + ActiveCount);
        Debug.Log("Active enemy names: " + string.Join(", ", ActiveEnemyNames));
        return ActiveEnemyNames;
    }

    public List<string> CheckActiveBuffs()
    {
        GameObject[] buffs = GameObject.FindGameObjectsWithTag("Buff");
        List<string> ActiveBuffNames = new List<string>();
        int ActiveCount = 0;

        foreach (GameObject buff in buffs)
        {
            // Check if the buff GameObject is active
            if (buff.activeInHierarchy)
            {
                ActiveCount++;
                ActiveBuffNames.Add(buff.name);
            }
        }
        Debug.Log("Active buff: " + ActiveCount);
        Debug.Log("Active buff names: " + string.Join(", ", ActiveBuffNames));
        return ActiveBuffNames;
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

        if (loadFromMainMenu)
        {
            LoadFromMainMenu();

            
        }
        else
        {
            LoadNextLevel();
        }

        if (GameObject.FindGameObjectWithTag("FloatingAimJoystick") != null)
            floatingAimJoystick = GameObject.FindGameObjectWithTag("FloatingAimJoystick");
            if (GameObject.FindGameObjectWithTag("FixedAimJoystick") != null)
            fixedAimJoystick = GameObject.FindGameObjectWithTag("FixedAimJoystick");
            if (GameObject.FindGameObjectWithTag("ShootButton") != null)
            shootButton = GameObject.FindGameObjectWithTag("ShootButton");
            
            Debug.Log(floatingAimJoystick.GetInstanceID());
            
            if (PlayerPrefs.HasKey("autoShootMode")) {
                autoShootMode = PlayerPrefs.GetInt("autoShootMode");
            }
            else {
                PlayerPrefs.SetInt("autoShootMode", 0);
                autoShootMode = 0; // Mặc định là bấm nút để bắn đạn
            }
            if (autoShootMode == 1) {
                    shootButton.SetActive(false);
                    fixedAimJoystick.SetActive(false);
                    floatingAimJoystick.SetActive(true);
                }
            else {
                    shootButton.SetActive(true);
                    fixedAimJoystick.SetActive(true);
                    floatingAimJoystick.SetActive(false);
            }
        
        
    }



}

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public int playerHealth;
    public List<String> ActiveEnemyNames;
    public List<String> ActiveBuffNames;
    public int currentSceneIndex;
}
