using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject escapeZoneTrigger;

    [Header("UI References")]
    [SerializeField] private CanvasGroup victoryCanvasGroup;
    [SerializeField] private GameObject hudRoot;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    [Header("Player References")]
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private PlayerLook playerLookScript;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private bool _hasEscaped = false;
    private float _reminderCooldown = 0f;

    void Update()
    {
        if (_reminderCooldown > 0f)
        {
            _reminderCooldown -= Time.deltaTime;
        }
    }


    void Awake()
    {
        if (victoryCanvasGroup != null)
        {
            victoryCanvasGroup.alpha = 0f;
            victoryCanvasGroup.interactable = false;
            victoryCanvasGroup.blocksRaycasts = false;
        }

        if (escapeZoneTrigger != null)
        {
            escapeZoneTrigger.SetActive(false);
        }

        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (menuButton != null) menuButton.onClick.AddListener(ReturnToMainMenu);
    }

    void OnEnable()
    {
        GameEvents.OnAllGeneratorsDisabled += UnlockEscapeZone;
    }

    void OnDisable()
    {
        GameEvents.OnAllGeneratorsDisabled -= UnlockEscapeZone;
    }

    private void UnlockEscapeZone()
    {
        if (escapeZoneTrigger != null)
        {
            escapeZoneTrigger.SetActive(true);
        }
    }

    public void TriggerVictory()
    {
        if (!GameManager.Instance.HasCollectedFolder)
        {
            if (_reminderCooldown <= 0f)
            {
                SubtitleManager.Instance.ShowSubtitle("If only I had the folder, I would already be out of this nightmare...", 4f);
                _reminderCooldown = 4.5f;
            }
            return;
        }

        if (_hasEscaped) return;
        _hasEscaped = true;

        GameEvents.FireOnGameWon();

        if (playerMovementScript != null) playerMovementScript.enabled = false;
        if (playerLookScript != null) playerLookScript.enabled = false;
        if (hudRoot != null) hudRoot.SetActive(false);

        StartCoroutine(VictorySequence());
    }

    private IEnumerator VictorySequence()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            if (victoryCanvasGroup != null)
            {
                victoryCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            }
            yield return null;
        }

        if (victoryCanvasGroup != null)
        {
            victoryCanvasGroup.alpha = 1f;
            victoryCanvasGroup.interactable = true;
            victoryCanvasGroup.blocksRaycasts = true;
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
}
