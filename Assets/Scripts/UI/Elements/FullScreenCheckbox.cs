using UnityEngine;
using UnityEngine.UI;

public class FullScreenCheckbox : MonoBehaviour {
    private Toggle toggle;

    private void Start() {
        toggle = GetComponent<Toggle>();

        toggle.isOn = Screen.fullScreen;

        toggle.onValueChanged.AddListener(delegate {OnValueChange();});
    }
    
    private void OnValueChange() {
        Screen.fullScreen = toggle.isOn;
        GameManager.Instance.SaveScreenConfiguration();
    }
}