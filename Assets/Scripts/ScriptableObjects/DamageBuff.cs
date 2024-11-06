using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data/DamageBuffData")]
public class DamageBuff : Buff
{
    public BulletData bulletData;
    public int amount;
    public override void Apply(GameObject target)
    {
        target.GetComponentInChildren<Turret>().turretData.bulletData.damage += amount;
        
    }

}
