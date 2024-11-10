using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    
    public GameObject finishPanel,beatTheGameCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ( SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("You completed the game");
                Time.timeScale = 0f;
                Instantiate(beatTheGameCanvas);
            }
            else {
                finishPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            
        }
    }
}
