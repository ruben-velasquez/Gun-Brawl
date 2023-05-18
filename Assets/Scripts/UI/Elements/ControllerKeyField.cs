using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ControllerKeyField : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private InputController.InputAction action;
    private InputControllerPage page;
    [SerializeField]
    private Text keyText;
    private bool waiting = false;

    [SerializeField]
    private Color waitingColor;
    private Color normalColor;

    void Start()
    {
        page = GetComponentInParent<InputControllerPage>();
        GameManager.Instance.onLoadControllers += UpdateKey;
        normalColor = keyText.color;
        UpdateKey();
        waiting = false;
    }

    void UpdateKey()
    {
        keyText.text = page.controller.keyboardUser.GetKey(action).ToString();
    }

    void Update()
    {
        if (waiting && Input.anyKey)
        {
            waiting = false;
            keyText.color = normalColor;

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                UpdateKey();
                return;
            }

            KeyCode[] keyCodes = (KeyCode[])Enum.GetValues(typeof(KeyCode));

            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    page.SetKey(action, keyCode);
                    GameManager.Instance.SaveControllers();
                    break;
                }
            }

            UpdateKey();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        waiting = true;
        keyText.color = waitingColor;
        keyText.text = "Press a Key";
    }
}