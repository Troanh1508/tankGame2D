using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimTurret : MonoBehaviour
{

    bool isMoving;

    public float turretRotationSpeed = 200;
    public void Aim(Vector2 movementVector)
    {
        isMoving = Convert.ToBoolean(movementVector.magnitude);
        if (isMoving)
        {

        var desiredAngle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;

        var rotationStep = turretRotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);
        }
    }

}
