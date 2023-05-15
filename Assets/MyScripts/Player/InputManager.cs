using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerMovement playerMovement;

    public Vector2 movementInp;
    public Vector2 cameraInp;

    public float cameraInpX;
    public float cameraInpY;

    public float xInp;
    public float yInp;

    public bool jumpInp;
    public bool punchInp;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInp = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInp = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Jump.performed += i => jumpInp = true;
            playerControls.PlayerActions.Punch.performed += i => punchInp = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        MovementInputHandler();
        JumpInputHandler();
        playerAttacks();
    }

    private void MovementInputHandler()
    {
        yInp = movementInp.y;
        xInp = movementInp.x;

        cameraInpX = cameraInp.x;
        cameraInpY = cameraInp.y;
    }
    private void JumpInputHandler()
    {
        if(jumpInp)
        {
            jumpInp = false;
            playerMovement.HandleJump();
        }
    }

    private void playerAttacks()
    {
        if(punchInp)
        {
            punchInp = false;
        } 
    }

}
