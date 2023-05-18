using UnityEngine;
using System;
using System.Collections.Generic;

public class ControllersTabGroup : TabsGroup
{
    public GameObject tabPrefab;
    public GameObject pagePrefab;
    public GameObject addTabButton;

    void Start() {
        GameManager.Instance.controllerList.onChangeControllers += UpdateTabs;
        GameManager.Instance.controllerList.GetControllers(); // Actualiza todos los controladores
    }

    public void UpdateTabs() {
        if(GetTabsLenght() != GameManager.Instance.controllerList.userControllers.Count) {
            for (int i = 0; i < GetTabsLenght() - GameManager.Instance.controllerList.userControllers.Count; i++)
            {
                DeleteLastTab();
            }
            
            for (int i = 0; i < GameManager.Instance.controllerList.userControllers.Count - GetTabsLenght(); i++)
            {
                // 4 - (4 - 3) = 3
                int index = GameManager.Instance.controllerList.userControllers.Count - (GameManager.Instance.controllerList.userControllers.Count - GetTabsLenght());

                InputController.UserInputController controller = (InputController.UserInputController)GameManager.Instance.controllerList.userControllers[index];

                InputControllerPage page = CreateTab(tabPrefab, pagePrefab, "Player " + (GetTabsLenght() + 1).ToString()).GetComponent<InputControllerPage>();
                page.controller = controller;
                page.tabsGroup = this;
            }
            GameManager.Instance.controllerList.GetControllers(); // Actualiza todos los controladores, esto provoca que toda la función se vuelva a llamar
        }

        foreach (GameObject pageObj in pages)
        {
            InputControllerPage page = pageObj.GetComponent<InputControllerPage>();

            List<InputController.IInputController> controllers = GameManager.Instance.controllerList.controllers;

            if (page.controller.name == "Player 1" || page.controller.name != controllers[controllers.Count - 1].name)
            {
                page.deleteButton.SetActive(false);
            } else {
                page.deleteButton.SetActive(true);
            }
        }

        addTabButton.SetActive(GameManager.Instance.controllerList.userControllers.Count != 4);
    }
}