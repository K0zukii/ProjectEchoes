using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask interactableLayer;
    private PlayerInputHandler playerInput;

    private IInteractable currentInteractable;

    public event Action<bool> OnHoverStateChanged;
    private bool isHovering = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInputHandler>();
        playerInput.IsInteracting += OnInteractInputReceived;
    }

    void OnDestroy()
    {
        playerInput.IsInteracting -= OnInteractInputReceived;
    }

    void Update()
    {
        if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit, interactRange, interactableLayer))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactableObj))
            {
                currentInteractable = interactableObj;

                if (isHovering == false)
                {
                    isHovering = true;
                    OnHoverStateChanged?.Invoke(true);
                }
            }
            else
            {
                ClearInteraction();
            }
        }
        else
        {
            ClearInteraction();
        }
    }

    private void ClearInteraction()
    {
        currentInteractable = null;

        if (isHovering == true)
        {
            isHovering = false;
            OnHoverStateChanged?.Invoke(false);
        }
    }

    private void OnInteractInputReceived()
    {
        currentInteractable?.Interact();
    }
}
