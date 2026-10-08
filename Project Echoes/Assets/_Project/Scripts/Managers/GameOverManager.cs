using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private GameObject hudRoot;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    [Header("Player Reference")]
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private PlayerLook playerLookScript;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float delayBeforeFade = 0.5f;

    void Awake()
    {
        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0f;
            gameOverCanvasGroup.interactable = false;
            gameOverCanvasGroup.blocksRaycasts = false;
        }

        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (mainMenuButton != null) mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
    }

    void OnEnable()
    {
        GameEvents.OnPlayerCaught += HandlePlayerCaught;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerCaught -= HandlePlayerCaught;
    }

    private void HandlePlayerCaught()
    {
        if (playerMovementScript != null) playerMovementScript.enabled = false;
        if (playerLookScript != null) playerLookScript.enabled = false;

        if (hudRoot != null) hudRoot.SetActive(false);

        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            if (gameOverCanvasGroup != null)
            {
                gameOverCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            }
            yield return null;
        }

        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 1f;
            gameOverCanvasGroup.interactable = true;
            gameOverCanvasGroup.blocksRaycasts = true;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Game is closing...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
