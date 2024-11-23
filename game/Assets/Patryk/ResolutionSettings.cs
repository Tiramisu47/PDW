using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // Dropdown do wyboru rozdzielczo�ci
    public Toggle fullscreenToggle; // Checkbox do trybu pe�noekranowego

    private Resolution[] availableResolutions;

    private void Start()
    {
        // Pobierz dost�pne rozdzielczo�ci ekranu
        availableResolutions = Screen.resolutions;

        // Wype�nij dropdown opcjami rozdzielczo�ci
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
            options.Add(option);

            // Sprawd� aktualn� rozdzielczo��
            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Ustaw stan checkboxa na podstawie aktualnego trybu pe�noekranowego
        fullscreenToggle.isOn = Screen.fullScreen;

        // Przypisz funkcje do zdarze� UI
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        // Pobierz wybran� rozdzielczo��
        Resolution selectedResolution = availableResolutions[resolutionIndex];

        // Zmie� rozdzielczo��
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        Debug.Log($"Resolution set to: {selectedResolution.width} x {selectedResolution.height}");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen set to: " + isFullscreen);
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Resolution settings saved!");
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = resolutionIndex;
            SetResolution(resolutionIndex);
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;
            SetFullscreen(isFullscreen);
        }
    }

}
