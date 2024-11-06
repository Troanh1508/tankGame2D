using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : MonoBehaviour {
    public Transform[] patrolPoints;
    private int i;
    public TankController tankController;
    public AIDetector detector;

    private void Start() {
        i = 0;
        if (tankController == null)
            tankController = transform.GetComponentInChildren<TankController>();
    }

    private void Update() {
        if (detector.CurrentState == AIState.Patrolling) {
            Patrol();
        }
    }

    private void Patrol() {
        if (tankController != null) {
            if (Vector2.Distance(tankController.transform.position, patrolPoints[i].position) < 1) {
                increaseIndex();
            }
            Vector2 direction = patrolPoints[i].position - tankController.transform.position;
            direction.Normalize();
            tankController.HandleMoveBody(direction);
        }
    }

    private void increaseIndex() {
        i++;
        if (i >= patrolPoints.Length)
            i = 0;
    }
}
