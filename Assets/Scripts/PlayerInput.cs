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

    public Joystick moveJoystick;
    public Joystick aimJoystick;
    public Joystick fixedAimJoystick;
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
        if (moveJoystick != null)
        {
            GetBodyMovement();
        }
        if (aimJoystick != null)
        {
            GetTurretMovement();   
        }
        if (fixedAimJoystick != null)
        {
            GetFixedTurretMovement();
        }
        if (GameManager.Instance.autoShoot){
            GetComponentInChildren<TankController>().HandleShoot(); //Shoot as soon as ready
        }
        
    }

    
    private void GetTurretMovement()
    {
        Vector2 movementVector = new Vector2(aimJoystick.Horizontal,aimJoystick.Vertical);
        OnMoveTurret?.Invoke(movementVector.normalized);
    }
    private void GetFixedTurretMovement()
    {
        Vector2 movementVector = new Vector2(fixedAimJoystick.Horizontal,fixedAimJoystick.Vertical);
        OnMoveTurret?.Invoke(movementVector.normalized);
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(moveJoystick.Horizontal,moveJoystick.Vertical);
        OnMoveBody?.Invoke(movementVector.normalized);
    }


    void OnButtonClick()
    {
        //Debug.Log("Button clicked!");
        GetComponentInChildren<TankController>().HandleShoot();
    }


}
