using UnityEngine;
using System;

public class MatchManager : PlayerManager
{
    public event Action onMatchStart;
    public event Action onMatchEnd;

    public void StartMatch() {
        if(onMatchStart != null)
            onMatchStart();
            
        CreatePlayers();
    }
}