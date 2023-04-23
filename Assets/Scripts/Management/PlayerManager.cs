using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab; // Jugador de referencia
    private List<PlayerInfo> playerInfo = new List<PlayerInfo>(); // Información de los jugadores
    public List<GameObject> players; // Jugadores en juego

    private void Start()
    {
        // TODO: Agregar cantidad de jugadores desde la interfaz
        playerInfo.Add(new PlayerInfo()); // Añadimos un solo jugador de testeo
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
        players = new List<GameObject>(); // Limpiamos la lista

        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Player Spawn"); // Obtenemos los spawns
        
        int index = 0; // inicializamos el indice

        foreach (PlayerInfo info in playerInfo)
        {
            // Instanciamos el jugador y obtenemos su script
            Fighter.Fighter player = Instantiate(playerPrefab, spawns[index].transform.position, Quaternion.Euler(0,0,0)).GetComponent<Fighter.Fighter>();

            // Le ponemos su controlador
            player.inputController = info.controller;

            // Evitamos que se mueva
            player.move = false;

            // Le ponemos el animador que define su skin
            player.GetComponent<Animation.GBAnimator>().animationStack = info.skin.animator;

            // Lo añadimos a la lista
            players.Add(player.gameObject);

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
}