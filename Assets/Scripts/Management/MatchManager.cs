using UnityEngine;
using System;

public class MatchManager : PlayerManager
{
    public event Action onMatchStart;
    public event Action onMatchEnd;
    public bool matchEnd;

    public void StartMatch() {
        if(onMatchStart != null)
            onMatchStart();

        matchEnd = false;

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
        matchEnd = true;

        DisablePlayerMove();

        Debug.Log(alivePlayers[0].name + " ha ganado");
    }

    public virtual void ClearMatchInfo() {
        onMatchEnd = null;
        matchEnd = true;

        foreach (GameObject player in players)
        {
            player.GetComponent<Fighter.Fighter>().OnMatchEnd();
        }
    }

    public override void OnPlayerDie(GameObject player) {
        base.OnPlayerDie(player);

        CheckMatch();
    }
}