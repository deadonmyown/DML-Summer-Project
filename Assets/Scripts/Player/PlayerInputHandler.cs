using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    //public static event Action onInteract;
    
    public Vector2 RawMovementInput { get; private set; }
    public int MovementInputX { get; private set; }
    public int MovementInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool IsInteract { get; private set; }

    [SerializeField] private float jumpInputHoldTime = 0.4f;
    private float _jumpInputStartTime;

    private void Update()
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
            _jumpInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            //Debug.Log("Jump stop");
            JumpInput = false;
            JumpInputStop = true;
        }
    }

    public void StopJump() => JumpInput = false;
    
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + jumpInputHoldTime)
            JumpInput = false;
    }

    public void OnPlayerSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerManager.Instance.SwitchPlayer();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("Start Interaction");
            IsInteract = true;
        }
        else if (context.canceled)
        {
            //Debug.Log("End Interaction");
            IsInteract = false;
        }
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
