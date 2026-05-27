using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput {get; private set;}
    public Vector2 LookInput {get; private set;}
    public bool IsSprinting {get; private set;}
    public bool IsCrouching {get; private set;}

    public event Action OnFlashlightChanged;

    public void Move(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    
    public void Look(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        IsSprinting = context.ReadValueAsButton();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        IsCrouching = context.ReadValueAsButton();
    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnFlashlightChanged?.Invoke();
        }
    }
}
