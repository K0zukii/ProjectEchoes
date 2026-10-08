using UnityEngine;

public class BatteryPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private float regenAmount = 25f;
    [SerializeField] private FlashLightController playerFlashlight;

    public void Interact()
    {
        if (playerFlashlight != null)
        {
            playerFlashlight.AddBattery(regenAmount);
        }

        Destroy(gameObject);
    }
}
