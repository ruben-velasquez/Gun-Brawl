using System.Collections.Generic;
using UnityEngine;
using System;

namespace InputController
{

    [CreateAssetMenu(menuName = "Gun Brawl/InputController List")]
    [System.Serializable]
    public class InputControllerList : ScriptableObject
    {
        public event Action onChangeControllers;
        public List<IInputController> controllers;
        public List<IInputController> userControllers = new List<IInputController>();
        [SerializeField]
        private GameObject baseController;

        public void GetControllers()
        {
            // Verificamos los mandos y si hay mandos
            // Creamos un nuevo controlador y lo añadimos
            // a la lista

            GameManager.Instance.CheckGamePads();

            for (int i = 0; i < controllers.Count; i++)
            {
                if(controllers[i] == null) continue;
                if (controllers[i].name.StartsWith("Player"))
                {
                    UserInputController controller = (UserInputController)controllers[i];

                    // Si algún controlador que depende de su mando no tiene el mando conectado lo eliminamos
                    if (controller.controllBased && !GameManager.Instance.connectedGamePads.Contains(controller.prefferedController) &&
                        userControllers.Count > GameManager.Instance.connectedGamePads.Count)
                    {
                        DeleteController(controller);

                    } else if(!userControllers.Contains(controllers[i])) {
                        userControllers.Add(controllers[i]);
                    }
                }
            }

            for (int i = 0; i < userControllers.Count; i++)
            {
                userControllers[i].name = "Player " + (i + 1).ToString();
                userControllers[i].gameObject.name = "Player " + (i + 1).ToString();
                ((UserInputController)userControllers[i]).prefferedController = (XInputDotNetPure.PlayerIndex)i;
            }

            for (int i = 0; i < GameManager.Instance.connectedGamePads.Count; i++)
            {
                // Si hay más mandos disponibles que controladores
                // Creamos más controladores para esos mandos
                if (userControllers.Count < GameManager.Instance.connectedGamePads.Count)
                {
                    CreateController(true, true);
                }
            }

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            if (onChangeControllers != null)
                onChangeControllers();
        }

        public void ClearEvents()
        {
            onChangeControllers = null;
        }

        public void DeleteController(IInputController controller) {
            controller.asignedController = false;
#if !UNITY_EDITOR
            Destroy(controller.gameObject);
#endif
            userControllers.Remove(controller);
            controllers.Remove(controller);
        }

        public IInputController CreateController(bool UseGamePad, bool controllerBased) {
            IInputController newController;
            GameObject controllerObject;

#if UNITY_EDITOR
            controllerObject = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Input Users/Player " + (userControllers.Count + 1) + ".prefab", typeof(GameObject));

            if (controllerObject == null)
            {
                controllerObject = UnityEditor.PrefabUtility.SaveAsPrefabAsset(baseController, "Assets/Input Users/Player " + (userControllers.Count + 1) + ".prefab");
            }

            controllerObject.name = "Player " + (userControllers.Count + 1);

            newController = controllerObject.GetComponent<IInputController>();
#else
                    controllerObject = Instantiate(baseController);
                    DontDestroyOnLoad(controllerObject);
                    newController = controllerObject.GetComponent<IInputController>();
#endif
            newController.name = "Player " + (userControllers.Count + 1);
            newController.id = controllers.Count;
            ((UserInputController)newController).prefferedController = (XInputDotNetPure.PlayerIndex)userControllers.Count;
            ((UserInputController)newController).useGamePad = UseGamePad;
            ((UserInputController)newController).controllBased = controllerBased;

            userControllers.Add(newController);
            controllers.Add(newController);

            return newController;
        }
        
        public void CreateController(UserInputController controller) {
            IInputController newController;
            GameObject controllerObject;

#if UNITY_EDITOR
            controllerObject = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Input Users/Player " + (userControllers.Count + 1) + ".prefab", typeof(GameObject));

            if (controllerObject == null)
            {
                controllerObject = UnityEditor.PrefabUtility.SaveAsPrefabAsset(baseController, "Assets/Input Users/Player " + (userControllers.Count + 1) + ".prefab");
            }

            controllerObject.name = "Player " + (userControllers.Count + 1);

            newController = controllerObject.GetComponent<IInputController>();
#else
                    controllerObject = Instantiate(baseController);
                    DontDestroyOnLoad(controllerObject);
                    newController = controllerObject.GetComponent<IInputController>();
#endif
            newController.name = "Player " + (userControllers.Count + 1);
            newController.id = controllers.Count;
            ((UserInputController)newController).prefferedController = (XInputDotNetPure.PlayerIndex)userControllers.Count;
            ((UserInputController)newController).useGamePad = controller.useGamePad;
            ((UserInputController)newController).controllBased = false;
            ((UserInputController)newController).keyboardUser = controller.keyboardUser;
            ((UserInputController)newController).gamePadUser = controller.gamePadUser;

            userControllers.Add(newController);
            controllers.Add(newController);
        }
    }
}