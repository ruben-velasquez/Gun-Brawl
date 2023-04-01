using UnityEngine;

public class MovementManager : FighterMovement {
    // Variables públicas
    [Space]
    [Header("Movement Manager")]
    [SerializeField]
    public IInputController inputController;

    // Variables Privadas
    [SerializeField]
    private bool move;

    public override void Update() {
        base.Update(); // Ejecutamos la lógica anterior

        // Si la variable move no está activa no ejecutamos nada
        if(!move) return;

        if(inputController.IsJumping()) {
            Jump();
        }

        Move(inputController.MoveAxis());
    }

    public void EnanbleMovement() {
        move = true;
    }

    public void DisableMovement() {
        move = false;
    }
}