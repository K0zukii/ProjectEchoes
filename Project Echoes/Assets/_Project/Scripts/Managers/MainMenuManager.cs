using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Sens & Audio Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;

    [Header("Graphic Settings")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CloseOptions();
        LoadSettings();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (mainPanel != null) mainPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void LoadSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        AudioListener.volume = savedVolume;

        float savedSens = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        if (sensitivitySlider != null)
        {
            sensitivitySlider.minValue = 0.1f;
            sensitivitySlider.maxValue = 3f;
            sensitivitySlider.value = savedSens;
            sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        }

        int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(savedQuality);
        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
            qualityDropdown.value = savedQuality;
            qualityDropdown.RefreshShownValue();
            qualityDropdown.onValueChanged.AddListener(SetQuality);
        }

        bool savedFullscreen = PlayerPrefs.GetInt("IsFullscreen", Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = savedFullscreen;
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = savedFullscreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
