using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewTankMovementData", menuName = "Data/TankMovementData")]
public class TankMovementData : ScriptableObject
{
    public float speed = 1.5f;
    public float rotationSpeed = 300;

    public void ResetValues()
    {
        speed = 1.5f;
        rotationSpeed = 300; // Set to default values
    }
}
