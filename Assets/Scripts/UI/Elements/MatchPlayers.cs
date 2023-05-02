using UnityEngine;

public class MatchPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBox;
    [SerializeField]
    private GameObject addButton;
    [SerializeField]
    private Skin defaultSkin;
    [SerializeField]
    private InputController.IInputController defaultController;
    [SerializeField]
    private int maxPlayers = 4;

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
            for (int i = 0; i < 4 - GameManager.Instance.playerInfo.Count; i++)
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
            for (int i = 0; i < 2 - GameManager.Instance.playerInfo.Count; i++)
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
            if (GameManager.Instance.playerInfo.Count == 2)
            {
                foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
                {
                    button.Disable();
                }
            }

        }

        // Evitamos que se creen más jugadores de lo necesario
        if (GameManager.Instance.playerInfo.Count >= maxPlayers)
        {
            addButton.SetActive(false);
        }
    }

    private void OnGameModeChange(GameMode.GameMode gameMode)
    {
        // Teams Mode
        if (gameMode.name == "Teams Mode")
        {

            UI.SkinSelector[] skinSelectors = GetComponentsInChildren<UI.SkinSelector>();
            UI.ControllerSelector[] controllersSelectors;
            UI.DeleteBoxButton[] deleteBoxButtons;

            // - Minimo 4 jugadores
            for (int i = 0; i < 4 - skinSelectors.Length; i++)
            {
                CreatePlayer();
            }

            // - Modificamos las cajas dependiendo del bando del jugador

            skinSelectors = GetComponentsInChildren<UI.SkinSelector>();
            controllersSelectors = GetComponentsInChildren<UI.ControllerSelector>();
            deleteBoxButtons = GetComponentsInChildren<UI.DeleteBoxButton>();

            int index = 0;

            // -- Modificamos el skin selector
            foreach (UI.SkinSelector skinSelector in skinSelectors)
            {
                if (index < (int)Mathf.Floor(GameManager.Instance.playerInfo.Count / 2))
                    skinSelector.ChangeView(redTeamSkinSelector);
                else
                    skinSelector.ChangeView(blueTeamSkinSelector);

                index++;
            }

            index = 0;

            // -- Modificamos el controller selector
            foreach (UI.ControllerSelector controllerSelector in controllersSelectors)
            {
                if (index < (int)Mathf.Floor(GameManager.Instance.playerInfo.Count / 2))
                    controllerSelector.ChangeView(redTeamControllerSelector);
                else
                    controllerSelector.ChangeView(blueTeamControllerSelector);

                index++;
            }

            // - Evitamos que se borren jugadores
            if (deleteBoxButtons != null)
                foreach (UI.DeleteBoxButton deleteBoxButton in deleteBoxButtons)
                {
                    if (deleteBoxButton != null)
                        deleteBoxButton.Disable();
                }

            addButton.SetActive(false);
        }
        // Normal Mode
        else if (gameMode.name == "Normal Mode")
        {
            UI.SkinSelector[] skinSelectors = GetComponentsInChildren<UI.SkinSelector>();
            UI.ControllerSelector[] controllersSelectors;
            UI.DeleteBoxButton[] deleteBoxButtons;

            // - Minimo 4 jugadores
            for (int i = 0; i < 2 - skinSelectors.Length; i++)
            {
                CreatePlayer();
            }

            skinSelectors = GetComponentsInChildren<UI.SkinSelector>();
            controllersSelectors = GetComponentsInChildren<UI.ControllerSelector>();
            deleteBoxButtons = GetComponentsInChildren<UI.DeleteBoxButton>();

            foreach (UI.SkinSelector skinSelector in skinSelectors)
            {
                skinSelector.ChangeView(normalSkinSelector);
            }

            foreach (UI.ControllerSelector controllerSelector in controllersSelectors)
            {
                controllerSelector.ChangeView(normalControllerSelector);
            }
            foreach (UI.DeleteBoxButton deleteBoxButton in deleteBoxButtons)
            {
                if (GameManager.Instance.playerInfo.Count > 2)
                    deleteBoxButton.Enable();
                else
                    deleteBoxButton.Disable();

            }

        }
    }

    public GameObject CreatePlayer(int index)
    {
        GameObject box = Instantiate(playerBox, transform);
        box.transform.SetSiblingIndex(box.transform.GetSiblingIndex() - 1);

        box.GetComponentInChildren<UI.SkinSelector>().id = index;
        box.GetComponentInChildren<UI.ControllerSelector>().id = index;

        if (GameManager.Instance.playerInfo.Count >= 3)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Enable();
            }
        }
        else if (GameManager.Instance.playerInfo.Count <= 2)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Disable();
            }
        }

        return box;
    }

    public void CreatePlayer()
    {
        if (GameManager.Instance.playerInfo.Count >= maxPlayers)
        {
            return;
        }

        GameObject box = Instantiate(playerBox, transform);
        box.transform.SetSiblingIndex(box.transform.GetSiblingIndex() - 1);

        int index = GameManager.Instance.CreatePlayerInfo();

        box.GetComponentInChildren<UI.SkinSelector>().id = index;
        box.GetComponentInChildren<UI.ControllerSelector>().id = index;

        GameManager.Instance.SetPlayerSkin(index, defaultSkin);
        GameManager.Instance.SetPlayerController(index, defaultController);

        if (GameManager.Instance.playerInfo.Count >= maxPlayers)
        {
            addButton.SetActive(false);
        }

        if (GameManager.Instance.playerInfo.Count >= 3)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Enable();
            }
        }
        else if (GameManager.Instance.playerInfo.Count <= 2)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Disable();
            }
        }
    }

    public void CreatePlayer(PlayerInfo info, int atIndex)
    {
        GameObject box = CreatePlayer(atIndex);

        UI.SkinSelector skinSelector = box.GetComponentInChildren<UI.SkinSelector>();

        if (!info.skin) skinSelector.value = defaultSkin.id;
        else skinSelector.value = info.skin.id;
        skinSelector.UpdateSkin();

        UI.ControllerSelector controllerSelector = box.GetComponentInChildren<UI.ControllerSelector>();

        if (!info.controller) controllerSelector.value = defaultController.id;
        else controllerSelector.value = info.controller.id;
        controllerSelector.UpdateController();

        if (GameManager.Instance.playerInfo.Count >= 3)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Enable();
            }
        }
        else if (GameManager.Instance.playerInfo.Count <= 2)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Disable();
            }
        }
    }

    public void CreatePlayer(PlayerInfo info, int atIndex, GameMode.TeamsMode.Teams teams)
    {
        GameObject box = CreatePlayer(atIndex);

        UI.SkinSelector skinSelector = box.GetComponentInChildren<UI.SkinSelector>();

        if (!info.skin) skinSelector.value = defaultSkin.id;
        else skinSelector.value = info.skin.id;
        skinSelector.UpdateSkin();

        UI.ControllerSelector controllerSelector = box.GetComponentInChildren<UI.ControllerSelector>();

        if (!info.controller) controllerSelector.value = defaultController.id;
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

        if (GameManager.Instance.playerInfo.Count >= 3)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Enable();
            }
        }
        else if (GameManager.Instance.playerInfo.Count <= 2)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Disable();
            }
        }
    }

    public void DeletePlayer(GameObject box)
    {
        if (GameManager.Instance.playerInfo.Count == 1)
        {
            return;
        }

        int index = box.GetComponentInChildren<UI.SkinSelector>().id;
        GameManager.Instance.DeletePlayerInfo(index);
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

        if (GameManager.Instance.playerInfo.Count <= 2)
        {
            foreach (UI.DeleteBoxButton button in GetComponentsInChildren<UI.DeleteBoxButton>())
            {
                button.Disable();
            }
        }

        addButton.SetActive(true);
    }
}