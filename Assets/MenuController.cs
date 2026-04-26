using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;

    public Slider sensitivitySlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        // Загружаем сохранённые значения
        GameSettings.mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 0.5f);
        Debug.Log("Sens: " + GameSettings.mouseSensitivity);
        GameSettings.fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Debug.Log("Sens: " + GameSettings.fullscreen);
        // Устанавливаем значения в UI
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

        Screen.fullScreen = GameSettings.fullscreen;
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
        SceneManager.LoadScene("1"); // Убедись, что имя сцены верное
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit pressed");
    }
}