using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int MovementInputX { get; private set; }
    public int MovementInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.15f;
    private float jumpInputStartTime;

    void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        MovementInputX = Mathf.RoundToInt(RawMovementInput.x);
        MovementInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            Debug.Log("Jump stop");
            JumpInput = false;
            JumpInputStop = true;
        }
    }

    public void StopJump() => JumpInput = false;
    
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
            JumpInput = false;
    }
}
