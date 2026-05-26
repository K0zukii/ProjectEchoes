using UnityEngine;
using System;

public class PlayerStamina : MonoBehaviour
{
    private float _maxStamina;
    public float MaxStamina
    {
        get {return _maxStamina;}
    }
    [SerializeField] private float drainRate;
    [SerializeField] private float regenRate;
    [SerializeField] private float timeBeforeRegen = 3f;
    [SerializeField] private float sprintThreshold = 20f;
    private float regenTimer = 0f;
    public bool CanSprint {get; private set;}
    private float _currentStamina;
    public float CurrentStamina
    {
        get {return _currentStamina;}
    }

    public event Action<float> OnStaminaChanged;

    void Start()
    {
        _maxStamina = 100f;
        _currentStamina = _maxStamina;
        CanSprint = true;

        OnStaminaChanged?.Invoke(_currentStamina / _maxStamina);
    }

    public void ConsumeStamina()
    {
        _currentStamina -= drainRate * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

        float ratio = _currentStamina / _maxStamina;
        OnStaminaChanged?.Invoke(ratio);

        if (_currentStamina <= 0)
        {
            _currentStamina = 0;
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
        else
        {
            _currentStamina += regenRate * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

            float ratio = _currentStamina / _maxStamina;
            OnStaminaChanged?.Invoke(ratio);

            if (CanSprint == false && _currentStamina >= sprintThreshold)
            {
                CanSprint = true;
            }
        }
        
    }
}
