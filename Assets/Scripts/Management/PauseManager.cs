using UnityEngine;
using System;

public class PauseManager : MatchManager
{
    public bool paused;
    public event Action onPause;
    public event Action onResume;

    private void Awake() {
        onMatchEnd += ResetPauseEvents;
    }

    public override void StartMatch()
    {
        paused = false;
        base.StartMatch();
    }

    public void Pause() {
        if(onPause != null) onPause();

        paused = true;

        PausePlayers();
    }

    public void Resume() {
        if(onResume != null) onResume();

        paused = false;

        ResumePlayers();
    }

    public void ResetPauseEvents() {
        onPause = null;
        onResume = null;
    }

    public override void ClearMatchInfo()
    {
        base.ClearMatchInfo();
        ResetPauseEvents();
    }
}