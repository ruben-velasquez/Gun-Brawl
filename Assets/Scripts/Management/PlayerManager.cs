using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : GBSceneManager
{
    [SerializeField]
    private GameObject playerPrefab; // Jugador de referencia
    public List<PlayerInfo> playerInfo = new List<PlayerInfo>(); // Información de los jugadores
    public List<GameObject> players; // Jugadores en juego
    public List<GameObject> alivePlayers; // Jugadores en juego
    public List<GameObject> realPlayers; // Jugadores en juego
    public List<GameObject> computerPlayers; // Jugadores en juego

    private void Start()
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
    }

    // Crea todos los jugadores en la escena usando la información de cada uno
    public List<GameObject> CreatePlayers() {
        // Limpiamos todas las listas
        players = new List<GameObject>(); 
        alivePlayers = new List<GameObject>(); 
        realPlayers = new List<GameObject>(); 
        computerPlayers = new List<GameObject>(); 

        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Player Spawn"); // Obtenemos los spawns
        
        int index = 0; // inicializamos el indice

        foreach (PlayerInfo info in playerInfo)
        {
            // Creamos el HUD
            UI.FighterUI ui = CreateHUD();

            // Instanciamos el jugador y obtenemos su script
            Fighter.Fighter player = Instantiate(playerPrefab, spawns[index].transform.position, Quaternion.Euler(0,0,0)).GetComponent<Fighter.Fighter>();

            // Le ponemos su controlador
            player.inputController = info.controller;

            // Le añadimos el HUD
            player.ui = ui;

            // Evitamos que se mueva
            player.move = false;

            if(info.controller.name.StartsWith("Player")) {
                ui.SetName("Player " + (realPlayers.Count + 1).ToString());
                realPlayers.Add(player.gameObject);
            } else {
                ui.SetName("CPU " + (computerPlayers.Count + 1).ToString());
                computerPlayers.Add(player.gameObject);
            }

            // Le ponemos el animador que define su skin
            player.GetComponent<Animation.GBAnimator>().animationStack = info.skin.animator;

            // Lo añadimos a la lista
            players.Add(player.gameObject);
            alivePlayers.Add(player.gameObject);

            index++;
        }

        return players;
    }

    // Activa el movimiento de todos los jugadores
    public void EnablePlayerMove() {
        foreach (GameObject player in players)
        {
            player.GetComponent<Fighter.Fighter>().move = true;
        }
    }
    
    // Desactiva el movimiento de todos lo jugadores
    public void DisablePlayerMove() {
        foreach (GameObject player in players)
        {
            player.GetComponent<Fighter.Fighter>().move = false;
        }
    }

    // Congela a los jugadores
    public void PausePlayers() {
        foreach (GameObject player in players)
        {
            player.GetComponent<Fighter.Fighter>().move = false;
            player.GetComponent<Animation.GBAnimator>().pause = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    
    public void ResumePlayers() {
        foreach (GameObject player in players)
        {
            player.GetComponent<Fighter.Fighter>().move = true;
            player.GetComponent<Animation.GBAnimator>().pause = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }

    public virtual void OnPlayerDie(GameObject player) {
        alivePlayers.Remove(player);
    }
}