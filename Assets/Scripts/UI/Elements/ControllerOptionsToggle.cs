using UnityEngine;

public class ControllerOptionsToggle : OptionsToggle
{
    private InputControllerPage page;

    void Start() {
        page = GetComponentInParent<InputControllerPage>();
        UpdateValue();
        GameManager.Instance.onLoadControllers += UpdateValue;
    }

    void UpdateValue() {
        SetValue(page.controller.useGamePad);
    }

    public override void OnValueChange()
    {
        base.OnValueChange();

        GameManager.Instance.SaveControllers();
        page.SetController(isOn);
    }

    private void OnDestroy() {
        GameManager.Instance.onLoadControllers -= UpdateValue;
    }
}