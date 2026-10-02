using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get; private set; }

    public event Action OnFlashlightChanged;
    public event Action IsInteracting;
    public event Action IsThrowing;
    public void Move(InputAction.CallbackContext context)
    {
        if (!enabled) return;
        MoveInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (!enabled) return;
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        IsSprinting = context.ReadValueAsButton();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (!enabled) return;
        IsCrouching = context.ReadValueAsButton();
    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (!enabled) return;
        if (context.started)
        {
            OnFlashlightChanged?.Invoke();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsInteracting?.Invoke();
        }
    }

    public void OnThrowing(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsThrowing?.Invoke();
        }
    }

    void OnEnable()
    {
        GameEvents.OnPlayerCaught += DisableInputs;
        GameEvents.OnGameWon += DisableInputs;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerCaught -= DisableInputs;
        GameEvents.OnGameWon -= DisableInputs;
    }

    private void DisableInputs()
    {
        MoveInput = Vector2.zero;
        LookInput = Vector2.zero;
        IsSprinting = false;
        IsCrouching = false;
        enabled = false;

        if (TryGetComponent<UnityEngine.InputSystem.PlayerInput>(out var inputComponent))
        {
            inputComponent.DeactivateInput();
        }
    }
}
