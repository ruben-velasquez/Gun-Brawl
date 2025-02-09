using UnityEngine;
using System;

public class MatchManager : PlayerManager {
    [SerializeField]
    public GameMode.GameMode gameMode;
    public event Action onMatchStart;
    public event Action onMatchEnd;
    public event Action<GameMode.GameMode> onGameModeChange;
    public bool matchEnd;
    private Coroutine startGameCoroutine;

    public virtual void StartMatch() {
        if(onMatchStart != null)
            onMatchStart();

        matchEnd = false;

        startedMatch = false;

        onMatchStart = null;

        onGameModeChange = null;

        CreatePlayers();

        startGameCoroutine = StartCoroutine(StartGame());
        
        gameMode.StartMatch();
    }

    public void CheckMatch() {
        if(gameMode.CheckMatch()) {
            EndMatch();
        }
    }

    public virtual void EndMatch() {
        onMatchEnd?.Invoke();

        onMatchEnd = null;
        matchEnd = true;

        DisablePlayerMove();
    }

    public virtual void ClearMatchInfo() {
        onMatchEnd = null;
        matchEnd = true;

        if(startGameCoroutine != null)
            StopCoroutine(startGameCoroutine);

        foreach (GameObject player in playersState.players)
        {
            player.GetComponent<Fighter.Fighter>().OnMatchEnd();
        }
    }

    public override void OnPlayerDie(GameObject player) {
        base.OnPlayerDie(player);

        CheckMatch();
    }


    public virtual void ChangeGameMode(GameMode.GameMode newGameMode) {
        if(onGameModeChange != null) 
            onGameModeChange(newGameMode);

        gameMode = newGameMode;
    }
}