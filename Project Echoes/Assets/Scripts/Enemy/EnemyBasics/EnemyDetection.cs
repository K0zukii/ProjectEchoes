using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Noise Settings
    private readonly float errorMarge = 3;
    public Vector3 LastKnownPosition { get; private set; }
    public bool HasHeardNoise { get; private set; }


    // Light Settings
    private float illuminationThreshold = 1.5f;
    private float currentIlluminationTime = 0;
    public bool HasSeenPlayer { get; private set; }
    public Transform PlayerTarget { get; private set; }

    public bool Gen2Activated { get; private set; }
    private int genCount = 0;
    public bool isAsleep;
    public bool IsInvesting = false;

    //Timer
    private float reactionTimer = 0;
    private float loseTrackTimer = 3f;
    [SerializeField] private float reactionDelay = 0.5f;

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

    void Update()
    {
        if (reactionTimer > 0)
        {
            reactionTimer -= Time.deltaTime;
        }

        if (HasSeenPlayer)
        {
            loseTrackTimer -= Time.deltaTime;
            if (loseTrackTimer <= 0)
            {
                ResetVisualAlert();
            }
        }
    }

    public void OnNoiseHeard(Vector3 position, Transform source, float volumeRange)
    {
        if (reactionTimer > 0 || isAsleep) return;

        float distance = Vector3.Distance(position, transform.position);

        if (distance > volumeRange)
        {
            Debug.Log("La distance est trop loin donc on ignore le bruit.");
            return;
        }
        else if (distance < 4f && source.CompareTag("Player"))
        {
            Debug.Log("La distance est proche (distance < 4) et c'est le joueur directement qui est la source, donc on rentre dans on met HasSeenPlayer a true");
            HasSeenPlayer = true;
            loseTrackTimer = 3f;
            PlayerTarget = source;
        }
        else if (!IsInvesting)
        {
            Debug.Log("On rentre dans le dernier if, cela veut dire que le bruit n'est pas ignore, est loin et on est pas deja en train d'Investigate.");
            Vector3 errorRadius = Random.insideUnitSphere * errorMarge;
            errorRadius.y = 0f;
            LastKnownPosition = position + errorRadius;
            HasHeardNoise = true;
        }
    }

    public void ResetNoiseAlert()
    {
        HasHeardNoise = false;
        reactionTimer = reactionDelay;
    }

    public void OnIlluminated(float delta, Transform playerTransform)
    {
        if (isAsleep) return;
        loseTrackTimer = 3f;
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
