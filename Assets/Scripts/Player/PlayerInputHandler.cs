using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int MovementInputX { get; private set; }
    public int MovementInputY { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        MovementInputX = Mathf.RoundToInt(RawMovementInput.x);
        MovementInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
}
