using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/SpeedBuffData")]

public class SpeedBuff : Buff
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponentInChildren<TankMover>().movementData.speed += amount;
    }

}
