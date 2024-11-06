using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public float speed = 5;
    public int damage = 20;
    public float maxDistance = 10;

    public void ResetValues()
    {
        speed = 5;
        damage = 20;
        maxDistance = 10; // Set to default values
    }
}
