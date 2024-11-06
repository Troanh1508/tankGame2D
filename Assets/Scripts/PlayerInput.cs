using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    public Button shootButton;

    public Joystick joystick;
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();

    void Start(){
        if (shootButton != null)
        {
            // Add a listener to the button
            shootButton.onClick.AddListener(OnButtonClick);
        }
    }
    private void Awake() {
        if (mainCamera == null)
            mainCamera = Camera.main;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (joystick != null)
        {
            GetBodyMovement();
            GetTurretMovement();   
        }
    }

    
    private void GetTurretMovement()
    {
        Vector2 movementVector = new Vector2(joystick.Horizontal,joystick.Vertical);
        OnMoveTurret?.Invoke(movementVector.normalized);
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(joystick.Horizontal,joystick.Vertical);
        OnMoveBody?.Invoke(movementVector.normalized);
    }


    void OnButtonClick()
    {
        //Debug.Log("Button clicked!");
        GetComponentInChildren<TankController>().HandleShoot();
    }


}
