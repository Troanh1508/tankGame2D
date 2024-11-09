using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAutoShoot : MonoBehaviour
{
    public GameObject floatingAim, fixedAim, shootButton;

    public void ToggleUI(bool toggleValue)
    {
        if (toggleValue){
            floatingAim.SetActive(true);
            fixedAim.SetActive(false);
            shootButton.SetActive(false);
            GameManager.Instance.autoShootMode = 1;
            PlayerPrefs.SetInt("autoShootMode", 1);
        }
        else {
            floatingAim.SetActive(false);
            fixedAim.SetActive(true);
            shootButton.SetActive(true);
            GameManager.Instance.autoShootMode = 0;
            PlayerPrefs.SetInt("autoShootMode", 0);

        }
    }
}
