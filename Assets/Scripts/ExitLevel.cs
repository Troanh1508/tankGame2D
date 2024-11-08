using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    
    public GameObject finishPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SaveGame();
            GameManager.Instance.loadFromMainMenu = false;
            finishPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
