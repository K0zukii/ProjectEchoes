using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDetection : MonoBehaviour
{
    // Noise Settings
    private readonly float errorMarge = 3;
    public Vector3 LastKnownPosition {get; private set;}
    public bool HasHeardNoise {get; private set;}
    

    // Light Settings
    private float illuminationThreshold = 1.5f;
    private float currentIlluminationTime = 0;
    public bool HasSeenPlayer {get; private set;}
    public Transform PlayerTarget {get; private set;}

    public bool Gen2Activated {get; private set;}
    private int genCount = 0;

    void OnEnable()
    {
        GameEvents.OnNoiseEmitted += OnNoiseHeard;
        GameEvents.OnTerminalActivated += CountGenActivated;
        GameEvents.OnIlluminatingMonster += OnIlluminated;
    }

    void OnDisable()
    {
        GameEvents.OnNoiseEmitted -= OnNoiseHeard;
        GameEvents.OnIlluminatingMonster -= OnIlluminated;
        GameEvents.OnTerminalActivated -= CountGenActivated;
    }

    public void OnNoiseHeard(Vector3 position, Transform source, float volumeRange)
    {
        float distance = Vector3.Distance(position, transform.position);
        
        if(distance > volumeRange)
        {
            return;
        }
        else if (distance < (volumeRange / 3f) && source.CompareTag("Player"))
        {
            HasSeenPlayer = true;
            PlayerTarget = source;
        }
        else
        {
            Vector3 errorRadius = Random.insideUnitSphere * errorMarge;
            errorRadius.y = 0f;
            LastKnownPosition = position + errorRadius;
            HasHeardNoise = true;
        }
    }

    public void ResetNoiseAlert()
    {
        HasHeardNoise = false;
    }

    public void OnIlluminated(float delta, Transform playerTransform)
    {
        currentIlluminationTime += delta;
        if (currentIlluminationTime > illuminationThreshold)
        {
            HasSeenPlayer = true;
            currentIlluminationTime = 0;
            PlayerTarget = playerTransform;
        }
    }

    public void ResetVisualAlert()
    {
        HasSeenPlayer = false;
        PlayerTarget = null;
    }

    public void CountGenActivated()
    {
        genCount++;
        if (genCount >= 2)
        {
            Gen2Activated = true;
        }
    }
}
