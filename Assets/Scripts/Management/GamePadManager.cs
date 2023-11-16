using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
public class GamePadManager : PauseManager
{
    public bool isConnectedGamePads; // ¿Hay Mandos conectados?
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
        isConnectedGamePads = false;

        if(Gamepad.all.Count > 0) isConnectedGamePads = true;

        if(onCheckControllers != null)
            onCheckControllers();
    }

    // Devuelve el estado actualizado de un mando (Si no está conectado usa el primer mando que lo esté)
    public Gamepad GetGamePadState(int index)
    {
        if (!isConnectedGamePads) return null; // Si no hay mandos conectados devolvemos su estado vacío

        // Recorremos todos los mandos conectados para comprobar si es el que se pide
        for (int i = 0; i < Gamepad.all.Count; i++) 
        {
            // Si se encuentra el mando conectado devolvemos su estado actualizado
            if (i == index)
            {
                return Gamepad.all[i];
            }
        }

        // Si no se encontró ese mando conectado devolvemos el primer mando conectado
        return GetFirstGamePadConnected(index);
    }
    
    public Gamepad GetFirstGamePadConnected(int index) {
        if(index > Gamepad.all.Count - 1)
            index = Gamepad.all.Count - 1;

        return Gamepad.all[index];
    }

    private void ClearEvents() {
        onCheckControllers = null;
        controllerList.ClearEvents();
    }
}