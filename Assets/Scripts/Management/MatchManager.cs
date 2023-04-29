using UnityEngine;
using System;

public class MatchManager : PlayerManager
{
    public event Action onMatchStart;
    public event Action onMatchEnd;

    public void StartMatch() {
        if(onMatchStart != null)
            onMatchStart();

        onMatchStart = null;

        CreatePlayers();

        EnablePlayerMove();
    }

    public void CheckMatch() {
        if(alivePlayers.Count == 1) {
            EndMatch();
        }
    }

    public void EndMatch() {
        if (onMatchEnd != null)
            onMatchEnd();

        onMatchEnd = null;

        DisablePlayerMove();

        Debug.Log(alivePlayers[0].name + " ha ganado");
    }

    public override void OnPlayerDie(GameObject player) {
        base.OnPlayerDie(player);

        CheckMatch();
    }
}