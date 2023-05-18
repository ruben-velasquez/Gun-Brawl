using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ResolutionDropdown : MonoBehaviour
{
    private Dropdown dropdown;

    private void Start() {
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (Resolution resolution in Screen.resolutions)
        {
            string option = ResToString(resolution);

            options.Add(option);
        }

        dropdown.AddOptions(options);

        string currentRes = screenResToString();

        if(options.Contains(currentRes)) {
            dropdown.value = options.IndexOf(currentRes);
        } else {
            dropdown.value = 0;
            SetResolution(Screen.resolutions[0]);
        }

        dropdown.onValueChanged.AddListener(delegate { OnValueChange(); });
    }

    private string ResToString(Resolution resolution) {
        return String.Format("{0} x {1}", resolution.width, resolution.height);
    }
    
    private string screenResToString() {
        return String.Format("{0} x {1}", Screen.width, Screen.height);
    }

    private void OnValueChange() {
        Resolution resolution = Screen.resolutions[dropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    private void SetResolution(Resolution resolution) {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }
}