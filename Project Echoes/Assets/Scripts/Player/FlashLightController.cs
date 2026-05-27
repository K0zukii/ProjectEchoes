using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class FlashLightController : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private Transform playerCam;
    [SerializeField] private LayerMask enemyLayer;
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
    public event Action<float> OnBatteryChanged;

    void Start()
    {
        playerInput = GetComponent<PlayerInputHandler>();
        playerInput.OnFlashlightChanged += ToggleFlashlight;
        playerInput.OnCheatActivated += AddBattery;

        flashlight.enabled = isFlashlightOn;
        _currentBattery = maxBattery;

        OnBatteryChanged?.Invoke(_currentBattery);
    }

    void Update()
    {
        if (isFlashlightOn)
        {
            _currentBattery -= drainRate * Time.deltaTime;
            OnBatteryChanged?.Invoke(_currentBattery);

            if (_currentBattery <= 0)
            {
                isFlashlightOn = false;
                flashlight.enabled = false; 
            }

            
            if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit, Mathf.Infinity, enemyLayer))
            {
                GameEvents.FireOnIlluminatingMonster(Time.deltaTime);
                Debug.DrawLine(playerCam.position, playerCam.position + playerCam.forward * hit.distance, Color.red);
            }
            else
            {
                Debug.DrawLine(playerCam.position, playerCam.position + playerCam.forward, Color.white);
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

    public void AddBattery(float amount)
    {
        _currentBattery += amount;
        _currentBattery = Mathf.Clamp(_currentBattery, 0, maxBattery);
        OnBatteryChanged?.Invoke(_currentBattery);
    }

    void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.OnFlashlightChanged -= ToggleFlashlight;
            playerInput.OnCheatActivated -= AddBattery;
        }
    }
}
