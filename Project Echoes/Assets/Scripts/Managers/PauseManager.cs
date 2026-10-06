using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject hudRoot;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    [Header("Settings UI")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensSlider;

    [Header("Player References")]
    [SerializeField] private PlayerLook playerLookScript;

    private bool isPaused = false;
    private bool canPause = true;

    void Awake()
    {
        if (resumeButton != null) resumeButton.onClick.AddListener(ResumeGame);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (menuButton != null) menuButton.onClick.AddListener(ReturnToMainMenu);

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if (sensSlider != null && playerLookScript != null)
        {
            sensSlider.minValue = 0.1f;
            sensSlider.maxValue = 3f;

            sensSlider.value = playerLookScript.Sensitivity;
            sensSlider.onValueChanged.AddListener(SetSensitivity);
        }
    }

    void OnEnable()
    {
        GameEvents.OnPlayerCaught += LockPause;
        GameEvents.OnGameWon += LockPause;
    }


    void OnDisable()
    {
        GameEvents.OnPlayerCaught -= LockPause;
        GameEvents.OnGameWon -= LockPause;
    }

    private void LockPause()
    {
        canPause = false;
    }

    void Update()
    {
        if (!canPause) return;

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null) pausePanel.SetActive(true);
        if (hudRoot != null) hudRoot.SetActive(false);

        if (playerLookScript != null) playerLookScript.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null) pausePanel.SetActive(false);
        if (hudRoot != null) hudRoot.SetActive(true);

        if (playerLookScript != null) playerLookScript.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    private void SetSensitivity(float value)
    {
        if (playerLookScript != null)
        {
            playerLookScript.Sensitivity = value;
        }
    }
}
