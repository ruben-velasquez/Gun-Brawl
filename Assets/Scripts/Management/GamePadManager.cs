using System.Collections.Generic;
using XInputDotNetPure;
using System;
using UnityEngine;

public class GamePadManager : PauseManager
{
    public bool isConnectedGamePads; // ¿Hay Mandos conectados?
    public List<PlayerIndex> connectedGamePads; // Lista de mandos conectados

    // NOTA: Los "Estados" representan el estado de todos los botones
    // de un mando en un momento dado.

    public List<GamePadState> currentState; // Estado actual de los mandos
    public List<GamePadState> prevState; // Estado anterior de los mandos
    public InputController.InputControllerList controllerList; // Lista de controladores (Tanto mando como teclado)
    public event Action onCheckControllers; // Evento que se llama después de actualizar la lista de mandos conectados

    public override void Start() {
        base.Start();

        onMatchStart += ClearEvents; // Cuando la partida empiece limpiamos los eventos que puedan dar errores
        onMatchStart += CheckGamePads; // Comprobamos los mandos

        // Desactivamos todos los controladores que pueden estár erroneamente en uso
        foreach (InputController.IInputController controller in controllerList.controllers)
        {
            if(controller == null) continue;
            controller.asignedController = false;
        }
    }

    public override void StartMatch()
    {
        base.StartMatch();

        ClearEvents();
        CheckGamePads();
    }

    public override void ChangeGameMode(GameMode.GameMode newGameMode)
    {
        base.ChangeGameMode(newGameMode);
        controllerList.GetControllers(); // Actualiza todos los controladores
    }

    // Verifica y almacena los mandos presentes
    public void CheckGamePads()
    {
        connectedGamePads = new List<PlayerIndex>(); // Limpiamos la lista de mandos conectados
        currentState = new List<GamePadState>(); // Por precausión limpiamos la lista de estado actual
        prevState = new List<GamePadState>(); // Por precausión limpiamos la lista de estado actual
        isConnectedGamePads = false;

        // Recorremos el número de mandos máximo
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i; // Convertimos el indice a PlayerIndex
            GamePadState testState = GamePad.GetState(testPlayerIndex); // Obtenemos el estado de ese PlayerIndex
            if (testState.IsConnected) // Verificamos si está conectado el mando
            {
                connectedGamePads.Add(testPlayerIndex); // Si es así lo almacenamos
                currentState.Add(new GamePadState());
                prevState.Add(new GamePadState());

                isConnectedGamePads = true;
            }
        }

        if(onCheckControllers != null)
            onCheckControllers();
    }

    // Almacena el estado de un mando
    private GamePadState CheckGamePadButtons(PlayerIndex index)
    {
        int indexNumber = (int)index; // Mejoramos la legibilidad teniando una variable que almacena la conversión de PlayerIndex a Int

        prevState[indexNumber] = currentState[indexNumber]; // Seteamos el estado anterior con el actual

        currentState[indexNumber] = GamePad.GetState(index); // Actualizamos el estado

        return currentState[indexNumber];
    }

    // Devuelve el estado actualizado de un mando (Si no está conectado usa el primer mando que lo esté)
    public GamePadState GetGamePadState(PlayerIndex index)
    {
        if (!isConnectedGamePads) return GamePad.GetState(index); // Si no hay mandos conectados devolvemos su estado vacío

        // Recorremos todos los mandos conectados para comprobar si es el que se pide
        for (int i = 0; i < connectedGamePads.Count; i++) 
        {
            // Si se encuentra el mando conectado devolvemos su estado actualizado
            if (connectedGamePads[i] == index)
            {
                return CheckGamePadButtons(connectedGamePads[i]);
            }
        }

        // Si no se encontró ese mando conectado devolvemos el primer mando conectado
        return GetFirstGamePadConnected(index);
    }

    public GamePadState GetFirstGamePadConnected()
    {
        return CheckGamePadButtons(connectedGamePads[0]);
    }
    
    public GamePadState GetFirstGamePadConnected(PlayerIndex index) {
        int gamePadIndex = (int)index;

        if(gamePadIndex > connectedGamePads.Count - 1)
            gamePadIndex = connectedGamePads.Count - 1;

        return CheckGamePadButtons(connectedGamePads[gamePadIndex]);
    }

    private void ClearEvents() {
        onCheckControllers = null;
        controllerList.ClearEvents();
    }
}