using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public Buff buff;
    private void OnTriggerEnter2D(Collider2D collider2D) {

        
        Destroy(gameObject);
        buff.Apply(collider2D.gameObject);
    }

}
