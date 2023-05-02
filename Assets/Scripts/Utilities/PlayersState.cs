using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayersState {
    public List<GameObject> players; // Jugadores
    public List<GameObject> alivePlayers; // Jugadores vivos
    public List<GameObject> realPlayers; // Jugadores locales
    public List<GameObject> computerPlayers; // Jugadores automatizados
}