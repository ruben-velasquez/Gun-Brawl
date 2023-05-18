using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ControllerButtonField : MonoBehaviour, IPointerClickHandler
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
        keyText.text = page.controller.gamePadUser.GetKey(action).ToString();
    }

    void Update()
    {
        if (waiting)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                waiting = false;
                keyText.color = normalColor;

                UpdateKey();
                return;
            }

            GamePadHandler.ControllerButton button = GamePadHandler.GetAnyButton();

            if(button == GamePadHandler.ControllerButton.None) {
                return;
            }

            waiting = false;
            keyText.color = normalColor;

            page.SetKey(action, button);
            GameManager.Instance.SaveControllers();

            UpdateKey();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        waiting = true;
        keyText.color = waitingColor;
        keyText.text = "Press a Button";
    }

    private void OnDestroy() {
        GameManager.Instance.onLoadControllers -= UpdateKey;
    }
}