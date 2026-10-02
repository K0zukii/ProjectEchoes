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
        Debug.Log($"[GAME MANAGER] Generateur coupe. Restants : {GeneratorsRemaining}");

        if (GeneratorsRemaining <= 0)
        {
            Debug.Log("<color=green>[GAME MANAGER] Tous les gens sont coupes ! </color>");
            GameEvents.FireOnAllGeneratorsDisabled();
        }
    }

    private void HandleFolderCollected()
    {
        HasCollectedFolder = true;
        Debug.Log("[GAME MANAGER] Dossier securise.");
    }

    public bool CanPlayerEscape()
    {
        return GeneratorsRemaining <= 0 && HasCollectedFolder;
    }
}
