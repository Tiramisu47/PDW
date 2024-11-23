using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettingsManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Dropdown qualityDropdown; // Dropdown do wyboru jakoœci
    

    private void Start()
    {
        LoadSettings();
        // Ustawienia pocz¹tkowe
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQualityLevel);

    }

    // Zmiana poziomu jakoœci
    public void SetQualityLevel(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        Debug.Log("Quality set to: " + QualitySettings.names[level]);
    }

  

   
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualityLevel", QualitySettings.GetQualityLevel());
  
        PlayerPrefs.Save();
        Debug.Log("Settings saved!");
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("QualityLevel"))
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));

      
    }
}
