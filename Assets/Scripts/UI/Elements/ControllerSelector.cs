using UnityEngine;
using System;

namespace UI
{
    public class ControllerSelector : Selector
    {
        public InputController.InputControllerList controllerList;
        private int beforeValueChange;

        public override void Start()
        {
            base.Start();

            controllerList.GetControllers();

            maxValue = controllerList.controllers.Count - 1;

            onChange += AfterUpdateController;
            beforeChange += BeforeUpdateController;

            controllerList.onChangeControllers += UpdateController;

            controllerList.controllers[value].asignedController = true;
        }

        public void BeforeUpdateController()
        {
            // Si el controlador referenciado ya no existe cambiamos
            if (value >= controllerList.controllers.Count || value < 0)
            {
                maxValue = controllerList.controllers.Count - 1;
                value = maxValue;
            }
            else if (!controllerList.controllers[value].repeatController)
            {
                controllerList.controllers[value].asignedController = false;
            }
            beforeValueChange = value;
        }

        public void AfterUpdateController() {
            if (!controllerList.controllers[value].repeatController && controllerList.controllers[value].asignedController)
            {
                int difference = (value - beforeValueChange);

                for (int i = 0; !controllerList.controllers[value].repeatController && controllerList.controllers[value].asignedController; i++)
                {
                    value += difference;

                    if (value > maxValue) value = 0;
                    if (value < 0) value = maxValue;
                }
            }
            UpdateController();
        }

        public void UpdateController()
        {            
            controllerList.controllers[value].asignedController = true;

            textContent.text = controllerList.controllers[value].name;

            GameManager.Instance.SetPlayerController(id, controllerList.controllers[value]);
        }
    }
}