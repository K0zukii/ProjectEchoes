using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float maxStamina;
    [SerializeField] private float drainRate;
    [SerializeField] private float regenRate;
    [SerializeField] private float timeBeforeRegen = 3f;
    [SerializeField] private float sprintThreshold = 20f;
    private float regenTimer = 0f;
    public bool canSprint {get; private set;}
    private float _currentStamina;
    public float currentStamina
    {
        get {return _currentStamina;}
    }

    void Start()
    {
        _currentStamina = maxStamina;
        canSprint = true;
    }

    public void ConsumeStamina()
    {
        _currentStamina -= drainRate * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);

        if (_currentStamina <= 0)
        {
            _currentStamina = 0;
            canSprint = false;
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
            _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);

            if (canSprint == false && currentStamina >= sprintThreshold)
            {
                canSprint = true;
            }
        }
        
    }
}
