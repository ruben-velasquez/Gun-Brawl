using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI
{
    public class ControllerSelector : Selector
    {
        [SerializeField]
        public Sprite controllerInputSprite;
        [SerializeField]
        public Sprite keyboardInputSprite;
        public InputController.InputControllerList controllerList;
        private int beforeValueChange;

        public override void Start()
        {
            base.Start();

            maxValue = controllerList.controllers.Count - 1;

            if (value > maxValue || value < 0)
            {
                value = 0;
            }

            onChange += AfterUpdateController;
            beforeChange += BeforeUpdateController;

            controllerList.onChangeControllers += OnChangeControllers;

            controllerList.controllers[value].asignedController = true;
        }

        public void OnChangeControllers()
        {
            maxValue = controllerList.controllers.Count - 1;

            // Si el controlador referenciado ya no existe cambiamos
            if (value > maxValue || value < 0)
            {
                value = 0;
            }

            UpdateController();
        }

        public void BeforeUpdateController()
        {
            maxValue = controllerList.controllers.Count - 1;

            if (!controllerList.controllers[value].repeatController)
            {
                controllerList.controllers[value].asignedController = false;
            }

            beforeValueChange = value;
        }

        public void AfterUpdateController()
        {
            // Si el controlador referenciado ya no existe cambiamos
            if (value > maxValue || value < 0)
            {
                value = 0;
            }

            if (!controllerList.controllers[value].repeatController && controllerList.controllers[value].asignedController)
            {
                int difference = (value - beforeValueChange);
                difference = difference > 1 ? -1 : difference;

                for (int i = 0; !controllerList.controllers[value].repeatController && controllerList.controllers[value].asignedController; i++)
                {
                    value += difference;

                    if (value > maxValue) value = 0;
                    if (value < 0) value = maxValue;
                }
            }
            UpdateController();
        }


        // Solo actualizamos la vista, sin comprobar errores o inconsistencias
        public void UpdateController()
        {
            if (imageContent == null)
            {
                return;
            }

            if (controllerList.controllers[value].name.StartsWith("Player"))
            {
                imageContent.enabled = true;
                if (((InputController.UserInputController)controllerList.controllers[value]).useGamePad)
                {
                    imageContent.sprite = controllerInputSprite;
                }
                else
                    imageContent.sprite = keyboardInputSprite;
            }
            else
                imageContent.enabled = false;

            controllerList.controllers[value].asignedController = true;

            textContent.text = controllerList.controllers[value].name;

            GameManager.Instance.SetPlayerController(id, controllerList.controllers[value]);
        }
    }
}