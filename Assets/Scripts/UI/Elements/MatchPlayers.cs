using UnityEngine;
using System.Collections.Generic;
using System;

public class MatchPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBox;
    [SerializeField]
    private List<GameObject> playerBoxes;
    [SerializeField]
    private GameObject addButton;
    [SerializeField]
    private Skin defaultSkin;
    [SerializeField]
    private InputController.IInputController defaultController;

    // Estilos del modo de juego -> Teams Mode
    public UI.Selector.SelectorView redTeamSkinSelector;
    public UI.Selector.SelectorView redTeamControllerSelector;
    public UI.Selector.SelectorView blueTeamSkinSelector;
    public UI.Selector.SelectorView blueTeamControllerSelector;
    public UI.Selector.SelectorView normalSkinSelector;
    public UI.Selector.SelectorView normalControllerSelector;


    private void Start()
    {
        GameManager.Instance.onGameModeChange += OnGameModeChange;

        // TODO: Encapsular comportamiento de cada Modo de juego

        // Teams Mode
        if (GameManager.Instance.gameMode.name == "Teams Mode")
        {
            // - Establecemos un minimo de 4 jugadores
            for (int i = 0; i < GameManager.Instance.gameMode.minPlayers - GameManager.Instance.playerInfo.Count; i++)
            {
                GameManager.Instance.CreatePlayerInfo();
            }

            int index = 0;

            // - Creamos las cajas dependiendo del bando del jugador
            foreach (PlayerInfo info in GameManager.Instance.playerInfo)
            {
                if (index < (int)Mathf.Floor(GameManager.Instance.playerInfo.Count / 2))
                    CreatePlayer(info, index, GameMode.TeamsMode.Teams.Red);
                else
                    CreatePlayer(info, index, GameMode.TeamsMode.Teams.Blue);

                index++;
            }
        }
        // Normal Mode
        else if (GameManager.Instance.gameMode.name == "Normal Mode")
        {
            // - Establecemos un minimo de 2 jugadores
            for (int i = 0; i < GameManager.Instance.gameMode.minPlayers - GameManager.Instance.playerInfo.Count; i++)
            {
                GameManager.Instance.CreatePlayerInfo();
            }

            int index = 0;
            foreach (PlayerInfo info in GameManager.Instance.playerInfo)
            {
                CreatePlayer(info, index);

                index++;
            }

            // - Evitamos que se borren jugadores si hay 2
            UpdateDeleteButtons();






        }

        // Evitamos que se creen más jugadores de lo necesario
        UpdateDeleteButtons();



    }

    private void OnGameModeChange(GameMode.GameMode gameMode)
    {
        CreatePlayersAndSelectors(gameMode);

        UI.SkinSelector[] skinSelectors = GetComponentsInChildren<UI.SkinSelector>();
        UI.ControllerSelector[] controllersSelectors = GetComponentsInChildren<UI.ControllerSelector>();

        if (gameMode.name == "Teams Mode")
        {
            UpdateSelectors(skinSelectors, redTeamSkinSelector, blueTeamSkinSelector);
            UpdateSelectors(controllersSelectors, redTeamControllerSelector, blueTeamControllerSelector);
        }
        else if (gameMode.name == "Normal Mode")
        {
            UpdateSelectors(skinSelectors, normalSkinSelector, normalSkinSelector);
            UpdateSelectors(controllersSelectors, normalControllerSelector, normalControllerSelector);
        }

        UpdateDeleteButtons(gameMode);
    }

    private void CreatePlayersAndSelectors(GameMode.GameMode gameMode)
    {
        UI.SkinSelector[] skinSelectors = GetComponentsInChildren<UI.SkinSelector>();

        // - Minimo de jugadores
        for (int i = 0; i < gameMode.minPlayers - skinSelectors.Length; i++)
        {
            CreatePlayer();
        }
    }

    private void UpdateSelectors(UI.Selector[] selectors, UI.Selector.SelectorView view1, UI.Selector.SelectorView view2)
    {
        int index = 0;
        foreach (UI.Selector selector in selectors)
        {
            if (index < (int)Mathf.Floor(GameManager.Instance.playerInfo.Count / 2))
                selector.ChangeView(view1);
            else
                selector.ChangeView(view2);

            index++;
        }
    }

    public GameObject CreatePlayer(int index)
    {
        GameObject box = Instantiate(playerBox, transform);
        playerBoxes.Add(box);

        box.transform.SetSiblingIndex(box.transform.GetSiblingIndex() - 1);

        box.GetComponentInChildren<UI.SkinSelector>().id = index;
        box.GetComponentInChildren<UI.ControllerSelector>().id = index;

        UpdateDeleteButtons();

        return box;
    }

    public void CreatePlayer()
    {
        if (GameManager.Instance.playerInfo.Count >= GameManager.Instance.gameMode.maxPlayers)
        {
            return;
        }

        GameObject box = Instantiate(playerBox, transform);
        playerBoxes.Add(box);

        box.transform.SetSiblingIndex(box.transform.GetSiblingIndex() - 1);

        int index = GameManager.Instance.CreatePlayerInfo();

        box.GetComponentInChildren<UI.SkinSelector>().id = index;
        box.GetComponentInChildren<UI.ControllerSelector>().id = index;

        GameManager.Instance.SetPlayerSkin(index, defaultSkin);
        GameManager.Instance.SetPlayerController(index, defaultController);

        UpdateDeleteButtons();
    }

    public void CreatePlayer(PlayerInfo info, int atIndex)
    {
        GameObject box = CreatePlayer(atIndex);

        UI.SkinSelector skinSelector = box.GetComponentInChildren<UI.SkinSelector>();

        if (info.skin == null) skinSelector.SetValue(defaultSkin.id);
        else skinSelector.value = info.skin.id;

        skinSelector.UpdateSkin();

        UI.ControllerSelector controllerSelector = box.GetComponentInChildren<UI.ControllerSelector>();

        if (info.controller == null) {
            controllerSelector.SetValue(defaultController.id);
        }
        else controllerSelector.value = info.controller.id;

        controllerSelector.UpdateController();

        UpdateDeleteButtons();
    }

    public void CreatePlayer(PlayerInfo info, int atIndex, GameMode.TeamsMode.Teams teams)
    {
        GameObject box = CreatePlayer(atIndex);

        UI.SkinSelector skinSelector = box.GetComponentInChildren<UI.SkinSelector>();

        if (info.skin == null) skinSelector.SetValue(defaultSkin.id);
        else skinSelector.value = info.skin.id;

        skinSelector.UpdateSkin();

        UI.ControllerSelector controllerSelector = box.GetComponentInChildren<UI.ControllerSelector>();

        if (info.controller == null) controllerSelector.SetValue(defaultController.id);
        else controllerSelector.value = info.controller.id;

        controllerSelector.UpdateController();

        if (teams == GameMode.TeamsMode.Teams.Red)
        {
            skinSelector.ChangeView(redTeamSkinSelector);
            controllerSelector.ChangeView(redTeamControllerSelector);
        }
        else if (teams == GameMode.TeamsMode.Teams.Blue)
        {
            skinSelector.ChangeView(blueTeamSkinSelector);
            controllerSelector.ChangeView(blueTeamControllerSelector);
        }

        UpdateDeleteButtons();
    }

    public void DeletePlayer(GameObject box)
    {
        if (GameManager.Instance.playerInfo.Count == 1 || GameManager.Instance.playerInfo.Count <= GameManager.Instance.gameMode.minPlayers)
        {
            return;
        }

        int index = box.GetComponentInChildren<UI.SkinSelector>().id;
        UI.ControllerSelector boxController = box.GetComponentInChildren<UI.ControllerSelector>();
        boxController.controllerList.controllers[boxController.value].asignedController = false;
        boxController.controllerList.onChangeControllers -= boxController.UpdateController;
        GameManager.Instance.DeletePlayerInfo(index);
        playerBoxes.Remove(box);
        DestroyImmediate(box);

        UI.SkinSelector[] skinSelectors = GetComponentsInChildren<UI.SkinSelector>();

        foreach (var skinSelector in skinSelectors)
        {
            if (skinSelector.id > 0) skinSelector.id -= 1;
        }

        UI.ControllerSelector[] controllerSelectors = GetComponentsInChildren<UI.ControllerSelector>();

        foreach (var controllerSelector in controllerSelectors)
        {
            if (controllerSelector.id > 0) controllerSelector.id -= 1;
        }

        UpdateDeleteButtons();
    }

    private void UpdateDeleteButtons(GameMode.GameMode gameMode = null)
    {
        if(!gameMode) gameMode = GameManager.Instance.gameMode;

        if (GameManager.Instance.playerInfo.Count > gameMode.minPlayers)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Enable();
            }
        }
        else
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Disable();
            }
        }

        if(GameManager.Instance.playerInfo.Count < gameMode.maxPlayers)
            addButton.SetActive(true);
        else 
            addButton.SetActive(false);
    }
}
