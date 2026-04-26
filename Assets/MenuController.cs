using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;

    public Slider musicVolumeSlider;
    public Slider sensitivitySlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        LoadSettings();
        ApplySettingsToUI();
    }

    void LoadSettings()
    {
        GameSettings.mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 0.5f);
        GameSettings.fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        Debug.Log("Sens: " + GameSettings.mouseSensitivity);
        Debug.Log("Fullscreen: " + GameSettings.fullscreen);
    }

    void ApplySettingsToUI()
    {
        if (sensitivitySlider != null)
        {
            sensitivitySlider.value = GameSettings.mouseSensitivity;
            sensitivitySlider.onValueChanged.AddListener(SaveSensitivity);
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = GameSettings.fullscreen;
            fullscreenToggle.onValueChanged.AddListener(SaveFullscreen);
        }

        float music = PlayerPrefs.GetFloat("MusicVolume", 0.3f);

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = music;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(music);

        Screen.fullScreen = GameSettings.fullscreen;
    }

    //ВАЖНО: теперь UI обновляется при каждом открытии меню
    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);

        RefreshUI();
    }

    void RefreshUI()
    {
        if (sensitivitySlider != null)
            sensitivitySlider.SetValueWithoutNotify(GameSettings.mouseSensitivity);

        if (fullscreenToggle != null)
            fullscreenToggle.SetIsOnWithoutNotify(GameSettings.fullscreen);

        float music = PlayerPrefs.GetFloat("MusicVolume", 0.3f);

        if (musicVolumeSlider != null)
            musicVolumeSlider.SetValueWithoutNotify(music);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
    }

    public void SaveSensitivity(float value)
    {
        GameSettings.mouseSensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save();
        Debug.Log("Sensitivity saved: " + value);
    }

    public void SaveFullscreen(bool value)
    {
        GameSettings.fullscreen = value;
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("Fullscreen", value ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Fullscreen saved: " + value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("1");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit pressed");
    }
}