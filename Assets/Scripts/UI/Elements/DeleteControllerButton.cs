using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeleteControllerButton : MonoBehaviour
{
    private InputControllerPage page;
    private Button button;

    void Start()
    {
        page = GetComponentInParent<InputControllerPage>();
        button = GetComponent<Button>();
        List<InputController.IInputController> controllers = GameManager.Instance.controllerList.controllers;

        button.onClick.AddListener(OnClick);
        
        if(page.controller.name == "Player 1" || page.controller.name != controllers[controllers.Count - 1].name) {
            gameObject.SetActive(false);
        }

    }

    void OnClick() {
        page.DeleteController();
    }
}