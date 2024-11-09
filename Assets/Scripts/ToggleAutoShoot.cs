using System;
using System.Collections;
using System.Collections.Generic;
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
            GameManager.Instance.autoShoot = true;
        }
        else {
            floatingAim.SetActive(false);
            fixedAim.SetActive(true);
            shootButton.SetActive(true);
            GameManager.Instance.autoShoot = false;
        }
    }
}
