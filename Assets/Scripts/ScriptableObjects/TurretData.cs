using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewTurretData", menuName ="Data/TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadDelay = 1;
    public BulletData bulletData;

    public void ResetValues()
    {
        reloadDelay = 1; // Set to default values
        bulletData.ResetValues();
    }
}
