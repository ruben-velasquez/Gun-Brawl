using UnityEngine;

public class InputControllerPage : MonoBehaviour
{
    public InputController.UserInputController controller;
    public ControllersTabGroup tabsGroup;
    public GameObject deleteButton;

    public void SetController(bool useGamePad)
    {
        controller.useGamePad = useGamePad;
    }

    public void SetKey(InputController.InputAction action, KeyCode key) {
        controller.ChangeKey(action, key);
    }
    
    public void SetKey(InputController.InputAction action, GamePadHandler.ControllerButton key) {
        controller.ChangeButton(action, key);
    }

    public void DeleteController() {
        GameManager.Instance.controllerList.DeleteController(controller);
        tabsGroup.UpdateTabs();
    }
}