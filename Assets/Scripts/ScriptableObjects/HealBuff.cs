using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewHealBuff", menuName = "Data/HealBuffData")]
public class HealBuff : Buff
{
    public int amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Damagable>().Heal(amount);
    }

    
}
