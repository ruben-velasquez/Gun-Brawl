using UnityEngine;
using System;

public class ConfigurationManager : InputProfilesManager {
    // public event Action onLoadScreenConfiguration;

    public override void Start()
    {
        base.Start();
        LoadScreenConfiguration();
    }

    public void SaveScreenConfiguration() {
        //  Creamos la configuración
        ScreenConfiguration config = new ScreenConfiguration();

        config.quality = QualitySettings.GetQualityLevel();

        // La pasamos a JSON
        PlayerPrefs.SetString("Screen Configuration", JsonUtility.ToJson(config));
    }

    public void LoadScreenConfiguration() {
        if(!PlayerPrefs.HasKey("Screen Configuration")) return;

        string configJson = PlayerPrefs.GetString("Screen Configuration");
        ScreenConfiguration config = JsonUtility.FromJson<ScreenConfiguration>(configJson);

        QualitySettings.SetQualityLevel(config.quality);
    }
}

[System.Serializable]
public class ScreenConfiguration {
    // Pronto podría agregar más cosas
    public int quality;
}