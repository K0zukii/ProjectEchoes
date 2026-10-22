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

    void Awake()
    {
        playerInput = GetComponent<PlayerInputHandler>();
    }

    void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.IsInteracting += OnInteractInputReceived;
        }
    }

    void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.IsInteracting -= OnInteractInputReceived;
        }
    }

    void Update()
    {
        if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit, interactRange, interactableLayer) && hit.collider.TryGetComponent(out IInteractable interactableObj))
        {
            currentInteractable = interactableObj;

            if (!isHovering)
            {
                isHovering = true;
                OnHoverStateChanged?.Invoke(true);
            }
            return;
        }

        ClearInteraction();
    }

    private void ClearInteraction()
    {
        currentInteractable = null;

        if (isHovering)
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
