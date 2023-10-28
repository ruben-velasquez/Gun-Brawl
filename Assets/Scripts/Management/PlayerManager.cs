using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : GBSceneManager
{
    [SerializeField]
    private GameObject playerPrefab; // Jugador de referencia
    public List<PlayerInfo> playerInfo = new List<PlayerInfo>(); // Información de los jugadores
    public PlayersState playersState;

    public virtual void Start()
    {
        
    }

    public int CreatePlayerInfo() {
        playerInfo.Add(new PlayerInfo());
        return playerInfo.Count - 1;
    }

    public void DeletePlayerInfo(int index) {
        playerInfo.RemoveAt(index);
    }

    // Cambiar la información de un jugador
    public void SetPlayerInfo(int index, PlayerInfo info) {
        playerInfo[index] = info;
    }

    // Cambiar la skin de un jugador
    public void SetPlayerSkin(int index, Skin newSkin) {
        playerInfo[index].skin = newSkin;
    }

    // Cambiar el controlador de un jugador
    public void SetPlayerController(int index, InputController.IInputController newController) {
        playerInfo[index].controller = newController;

        CheckControllersUsage();
    }

    // Crea todos los jugadores en la escena usando la información de cada uno
    public List<GameObject> CreatePlayers() {
        // Limpiamos todas las listas
        playersState.players = new List<GameObject>(); 
        playersState.alivePlayers = new List<GameObject>(); 
        playersState.realPlayers = new List<GameObject>(); 
        playersState.computerPlayers = new List<GameObject>(); 

        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Player Spawn"); // Obtenemos los spawns

        if(spawns.Length == 0) Debug.LogError("There are no Spawns in the Scene");
        else if(spawns.Length < 4) Debug.LogError("There are not enough Spawns in the Scene");
        
        int index = 0; // inicializamos el indice

        foreach (PlayerInfo info in playerInfo)
        {
            // Creamos el HUD
            UI.FighterUI ui = CreateHUD();

            // Instanciamos el jugador y obtenemos su script
            Fighter.Fighter player = Instantiate(playerPrefab, spawns[index].transform.position, Quaternion.Euler(0,0,0)).GetComponent<Fighter.Fighter>();

            // Le ponemos su controlador
            if (info.controller.name.StartsWith("Player")) {
                player.inputController = info.controller;
            } else {
                // Suponiendo que es un CPU le asignamos un controlador independiente
                InputController.ComputerController controller = player.gameObject.AddComponent<InputController.ComputerController>();
                // Lo asignamos
                player.inputController = controller;
                controller.options = ((InputController.ComputerController)info.controller).options;
            }

            // Le añadimos el HUD
            player.ui = ui;

            // Evitamos que se mueva
            player.move = false;

            // Establecemos el nombre que aparecerá en la HUD
            if(info.controller.name.StartsWith("Player")) {
                ui.SetName(info.controller.name);
                playersState.realPlayers.Add(player.gameObject);
            } else {
                ui.SetName("CPU " + (playersState.computerPlayers.Count + 1).ToString());
                playersState.computerPlayers.Add(player.gameObject);
            }

            player.name = ui.name.text;

            // Le ponemos el animador que define su skin
            player.GetComponent<Animation.GBAnimator>().animationStack = info.skin.animator;

            // Lo añadimos a la lista
            playersState.players.Add(player.gameObject);
            playersState.alivePlayers.Add(player.gameObject);

            // Teams Mode -> Dependiendo del bando cambiamos el HUD
            if (GameManager.Instance.gameMode.name == "Teams Mode")
            {
                if (index < (int)Mathf.Floor(playerInfo.Count / 2))
                {
                    SetHUDRedTeam(ui);
                    CreateTag(player, GameMode.TeamsMode.Teams.Red);
                }
                else
                {
                    SetHUDBlueTeam(ui);
                    CreateTag(player, GameMode.TeamsMode.Teams.Blue);
                }
            }
            // Normal Mode -> Solo ponemos los tags a los jugadores si son locales
            else if (GameManager.Instance.gameMode.name == "Normal Mode") {
                CreateTag(player);
            }

            index++;
        }

        return playersState.players;
    }

    // Activa el movimiento de todos los jugadores
    public void EnablePlayerMove() {
        foreach (GameObject player in playersState.players)
        {
            player.GetComponent<Fighter.Fighter>().move = true;
        }
    }
    
    // Desactiva el movimiento de todos lo jugadores
    public void DisablePlayerMove() {
        foreach (GameObject player in playersState.players)
        {
            player.GetComponent<Fighter.Fighter>().move = false;
        }
    }

    // Congela a los jugadores
    public void PausePlayers() {
        foreach (GameObject player in playersState.players)
        {
            player.GetComponent<Fighter.Fighter>().move = false;
            player.GetComponent<Animation.GBAnimator>().pause = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    
    public void ResumePlayers() {
        foreach (GameObject player in playersState.players)
        {
            player.GetComponent<Fighter.Fighter>().move = true;
            player.GetComponent<Animation.GBAnimator>().pause = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }

    public virtual void OnPlayerDie(GameObject player) {
        playersState.alivePlayers.Remove(player);
    }

    public void CheckControllersUsage() {
        // Obtenemos la lista de controladores
        InputController.InputControllerList controllerList = GetComponent<GamePadManager>().controllerList;

        foreach (InputController.IInputController controller in controllerList.controllers) {
            controller.asignedController = false;
        }

        // Recorremos cada PlayerInfo
        foreach (PlayerInfo info in playerInfo) {
            info.controller.asignedController = true;
        }
    }
}