using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalGenerators = 3;
    private int genRemaining;

    void Start()
    {
        genRemaining = totalGenerators;

        GameEvents.OnTerminalActivated += DecreaseGenerator;
    }

    void OnDisable()
    {
        GameEvents.OnTerminalActivated -= DecreaseGenerator;
    }

    void DecreaseGenerator()
    {
        genRemaining--;
        Debug.Log("Generateur desactive ! Generateur restant : " + genRemaining);
        
        if(genRemaining <= 0)
        {
            Debug.Log("Porte deverouille, echappe toi vite !");
        }
    }
}
