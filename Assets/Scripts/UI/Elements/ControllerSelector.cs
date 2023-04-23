using UnityEngine;

namespace UI
{
    public class ControllerSelector : Selector
    {
        public InputController.InputControllerList controllerList;

        public override void Start()
        {
            base.Start();

            maxValue = controllerList.controllers.Capacity - 1;

            onChange += UpdateController;
        }

        public void UpdateController()
        {
            textContent.text = controllerList.controllers[value].name;

            GameManager.Instance.SetPlayerController(id, controllerList.controllers[value]);
        }
    }
}