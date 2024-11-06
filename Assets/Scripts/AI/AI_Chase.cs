using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour {
    public GameObject player;
    public TankController tankController;
    public AIDetector detector;
    public float fieldOfVisionForShooting = 90;

    private void Awake() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (tankController == null)
            tankController = GetComponentInChildren<TankController>();
    }

    private void FixedUpdate() {
        if (detector.CurrentState == AIState.Chasing) {
            Chase();
        }
    }

    private void Chase() {
        if (player != null) {
            Vector2 direction = player.transform.GetChild(0).position - transform.GetChild(0).position;
            direction.Normalize();
            tankController.HandleMoveBody(direction);
            tankController.HandleTurretMovement(direction);

            if (Vector2.Angle(tankController.aimTurret.transform.right, direction) < fieldOfVisionForShooting / 2)
                tankController.HandleShoot();
        }
    }
}
