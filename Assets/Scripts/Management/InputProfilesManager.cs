using InputController;
using UnityEngine;
using System.Collections.Generic;
using System;

public class InputProfilesManager : GamePadManager
{
    public event Action onLoadControllers; // Se llama cuando se cargan los controladres

    public override void Start() {
        base.Start();

        // Actualizamos los controladores
        controllerList.GetControllers();

        //Cargamos los controles
        LoadControllers();
    }

    public void DeleteAll() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void SaveControllers() {
        DeleteControllersData();

        foreach (IInputController player in controllerList.userControllers)
        {
            UserInputController _player = (UserInputController)player;

            string keyboardJson = JsonUtility.ToJson(_player.keyboardUser);
            string gamePadJson = JsonUtility.ToJson(_player.gamePadUser);

            PlayerPrefs.SetString("Keyboard keys " + player.name, keyboardJson);
            PlayerPrefs.SetString("GamePad keys " + player.name, gamePadJson);
            PlayerPrefs.SetString("Controller " + player.name, _player.useGamePad.ToString());
        }

        // Guardamos los cambios
        PlayerPrefs.Save();
    }

    public void LoadControllers() {
        List<InputUser> keyboardKeys = new List<InputUser>();
        List<ControllerUser> gamePadKeys = new List<ControllerUser>();
        List<bool> useGamePad = new List<bool>();

        for (int i = 1; i <= 4; i++)
        {
            if(!PlayerPrefs.HasKey("Keyboard keys Player " + i.ToString()) || !PlayerPrefs.HasKey("GamePad keys Player " + i.ToString())) {
                break;
            }

            string keyboardJson = PlayerPrefs.GetString("Keyboard keys Player " + i.ToString());
            string gamePadJson = PlayerPrefs.GetString("GamePad keys Player " + i.ToString());
            string useGamePaRaw = PlayerPrefs.GetString("GamePad keys Player " + i.ToString());

            keyboardKeys.Add(JsonUtility.FromJson<InputUser>(keyboardJson));
            gamePadKeys.Add(JsonUtility.FromJson<ControllerUser>(gamePadJson));
            useGamePad.Add(useGamePaRaw == "True");
        }

        for (int i = 0; i < keyboardKeys.Count; i++)
        {
            if(i >= controllerList.userControllers.Count) {
                UserInputController controller = (UserInputController)controllerList.CreateController(false, false);

                controller.keyboardUser = keyboardKeys[i];
                controller.gamePadUser = gamePadKeys[i];
                controller.useGamePad = useGamePad[i];
            } else {
                UserInputController controller = (UserInputController)controllerList.userControllers[i];

                controller.keyboardUser = keyboardKeys[i];
                controller.gamePadUser = gamePadKeys[i];
                controller.useGamePad = useGamePad[i];
            }
        }

        // Actualizamos los controladores
        controllerList.GetControllers();

        if(onLoadControllers != null)
            onLoadControllers();
    }

    public void DeleteControllersData() {
        for (int i = 1; i <= 4; i++)
        {
            if (!PlayerPrefs.HasKey("Keyboard keys Player " + i.ToString()) || !PlayerPrefs.HasKey("GamePad keys Player " + i.ToString()))
            {
                break;
            }

            PlayerPrefs.DeleteKey("Keyboard keys Player " + i.ToString());
            PlayerPrefs.DeleteKey("GamePad keys Player " + i.ToString());
        }
    }
}