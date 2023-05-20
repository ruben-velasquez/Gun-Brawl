using UnityEngine;
using System.Collections.Generic;

public static class PlayerUtility
{
    public static List<Transform> GetPlayerTransforms() {
        List<Transform> players = new List<Transform>();

        foreach (GameObject player in GameManager.Instance.playersState.alivePlayers)
        {
            players.Add(player.transform);
        }

        return players;
    }
}