using UnityEngine;
using System;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainRate;
    [SerializeField] private float regenRate;
    [SerializeField] private float timeBeforeRegen = 3f;
    [SerializeField] private float sprintThreshold = 20f;

    private float _currentStamina;
    private float regenTimer = 0f;

    public float MaxStamina => maxStamina;
    public float CurrentStamina => _currentStamina;
    public bool CanSprint { get; private set; }

    public event Action<float> OnStaminaChanged;

    void Start()
    {
        _currentStamina = maxStamina;
        CanSprint = true;

        OnStaminaChanged?.Invoke(_currentStamina / maxStamina);
    }

    public void ConsumeStamina()
    {
        _currentStamina = Mathf.Clamp(_currentStamina - drainRate * Time.deltaTime, 0f, maxStamina);
        OnStaminaChanged?.Invoke(_currentStamina / maxStamina);

        if (_currentStamina <= 0f)
        {
            CanSprint = false;
            regenTimer = timeBeforeRegen;
        }
    }

    public void RegenerateStamina()
    {
        if (regenTimer > 0)
        {
            regenTimer -= Time.deltaTime;
            return;
        }
        if (_currentStamina >= maxStamina) return;

        _currentStamina = Mathf.Clamp(_currentStamina + regenRate * Time.deltaTime, 0f, maxStamina);
        OnStaminaChanged?.Invoke(_currentStamina / maxStamina);

        if (!CanSprint && _currentStamina >= sprintThreshold)
        {
            CanSprint = true;
        }
    }
}
