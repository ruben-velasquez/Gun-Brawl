using System.Collections.Generic;
using UnityEngine;
using System;

namespace InputController
{

    [CreateAssetMenu(menuName = "Gun Brawl/InputController List")]
    public class InputControllerList : ScriptableObject
    {
        public event Action onChangeControllers;
        public List<IInputController> controllers;
        private List<IInputController> userControllers = new List<IInputController>();
        [SerializeField]
        private GameObject baseController;

        public List<IInputController> GetControllers()
        {
            // Verificamos los mandos y si hay mandos
            // Creamos un nuevo controlador y lo añadimos
            // a la lista

            GameManager.Instance.CheckGamePads();

            userControllers = new List<IInputController>();

            for (int i = 0; i < controllers.Count; i++)
            {
                if(controllers[i] == null) continue;
                if (controllers[i].name.StartsWith("Player"))
                {
                    UserInputController controller = (UserInputController)controllers[i];

                    // Si algún controlador que depende de su mando no tiene el mando conectado lo eliminamos
                    if (controller.controllBased && !GameManager.Instance.connectedGamePads.Contains(controller.prefferedController))
                    {
#if !UNITY_EDITOR
                        Destroy(controllers[i].gameObject);
#endif
                        controllers.Remove(controllers[i]);
                    }
                    else
                        userControllers.Add(controllers[i]);
                }
            }

            for (int i = 0; i < GameManager.Instance.connectedGamePads.Count; i++)
            {
                // Si hay más mandos disponibles que controladores
                // Creamos más controladores para esos mandos
                if (userControllers.Count < GameManager.Instance.connectedGamePads.Count)
                {
                    IInputController newController;
                    GameObject controllerObject;

#if UNITY_EDITOR
                    controllerObject = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Input Users/Player " + (userControllers.Count + 1) + ".prefab", typeof(GameObject));

                    if (controllerObject == null)
                    {
                        controllerObject = UnityEditor.PrefabUtility.CreatePrefab("Assets/Input Users/Player " + (userControllers.Count + 1) + ".prefab", baseController);
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

                    userControllers.Add(newController);
                    controllers.Add(newController);
                }
            }

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            if (onChangeControllers != null)
                onChangeControllers();

            return controllers;
        }

        public void ClearEvents()
        {
            onChangeControllers = null;
        }
    }
}