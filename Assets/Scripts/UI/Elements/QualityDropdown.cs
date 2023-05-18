using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class QualityDropdown : MonoBehaviour
{
    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();

        List<string> options = new List<string>();

        int index = 0;
        int value = 0;
        

        foreach (string quality in QualitySettings.names)
        {
            options.Add(quality);

            if (QualitySettings.GetQualityLevel() == index)
            {
                value = index;
            }


            index++;
        }

        dropdown.AddOptions(options);

        dropdown.value = value;

        dropdown.onValueChanged.AddListener(delegate { OnValueChange(); });
    }

    private void OnValueChange()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
    }
}