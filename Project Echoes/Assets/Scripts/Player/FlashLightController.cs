using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class FlashLightController : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private float maxBattery;
    [SerializeField] private float drainRate;
    [SerializeField] private float regenRate;
    [SerializeField] private float _currentBattery;   // a enleve (juste pour teste)
    public float CurrentBattery
    {
        get {return _currentBattery;}
    }

    private bool isFlashlightOn = false;
    private PlayerInputHandler playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInputHandler>();
        playerInput.OnFlashlightChanged += ToggleFlashlight;

        flashlight.enabled = isFlashlightOn;
        _currentBattery = maxBattery;
    }

    void Update()
    {
        if (isFlashlightOn)
        {
            _currentBattery -= drainRate * Time.deltaTime;

            if (_currentBattery <= 0)
            {
                isFlashlightOn = false;
                flashlight.enabled = false; 
            }
        }
        
    }

    public void ToggleFlashlight()
    {
        if (isFlashlightOn == false && _currentBattery <= 0)
        {
            return;
        }

        isFlashlightOn = !isFlashlightOn;
        flashlight.enabled = isFlashlightOn;
    }

    void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.OnFlashlightChanged -= ToggleFlashlight;
        }
    }
}
