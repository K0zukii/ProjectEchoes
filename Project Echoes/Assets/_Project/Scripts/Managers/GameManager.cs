using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int totalGenerators = 3;
    public int GeneratorsRemaining { get; private set; }
    public bool HasCollectedFolder { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        GeneratorsRemaining = totalGenerators;
    }
    void OnEnable()
    {
        GameEvents.OnTerminalActivated += HandleGeneratorsDeactivated;
        GameEvents.OnFolderCollected += HandleFolderCollected;
    }

    void OnDisable()
    {
        GameEvents.OnTerminalActivated -= HandleGeneratorsDeactivated;
        GameEvents.OnFolderCollected -= HandleFolderCollected;
    }

    private void HandleGeneratorsDeactivated()
    {
        GeneratorsRemaining--;
        GameEvents.FireOnGeneratorDeactivated(GeneratorsRemaining);

        if (GeneratorsRemaining <= 0)
        {
            GameEvents.FireOnAllGeneratorsDisabled();
        }
    }

    private void HandleFolderCollected()
    {
        HasCollectedFolder = true;
    }

    public bool CanPlayerEscape()
    {
        return GeneratorsRemaining <= 0 && HasCollectedFolder;
    }
}
