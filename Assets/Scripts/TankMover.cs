using System;
using UnityEngine;
using UnityEngine.Events;
public class TankMover : MonoBehaviour
{

    public Rigidbody2D rb;

    public TankMovementData movementData;
    private Vector2 movementVector;
    bool isMoving;
    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    private void Awake()
    {
        
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector){
        this.movementVector = movementVector;

        OnSpeedChange?.Invoke(movementVector.magnitude);
    }

  private void FixedUpdate()
    {
        _ = Mathf.Clamp01(movementVector.magnitude);
        isMoving = Convert.ToBoolean(movementVector.magnitude);
        
        if (rb != null)
        {
            rb.MovePosition(rb.position + movementVector * movementData.speed * Time.fixedDeltaTime);
        }
        Rotate();
    }

    private void Rotate()
    {
        if (isMoving)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, movementData.rotationSpeed * Time.fixedDeltaTime);            
        }
    }
}
