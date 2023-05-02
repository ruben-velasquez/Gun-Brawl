using UnityEngine;

namespace Fighter
{
    public class MovementManager : FighterClimb
    {
        // Variables públicas
        [Space]
        [Header("Movement Manager")]
        [SerializeField]
        public InputController.IInputController inputController;
        [SerializeField]
        public bool move;

        public override void Update()
        {
            base.Update(); // Ejecutamos la lógica anterior

            // Si la variable move no está activa no ejecutamos nada
            if (!move)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            if (inputController.IsJumping())
            {
                Jump();
            }

            else if (!onClimbExitJump && !inputController.IsFollowingJump() && rb.velocity.y > 0)
            {
                FollowJump();
            }

            if (hasNearStair)
            {
                CheckClimbState(inputController.VerticalAxis());
            }
            else if (climbing)
            {
                ExitClimb(true);
            }

            if (climbing && grounded)
            {
                ExitClimb(false);

            }

            if (!climbing) Move(inputController.MoveAxis());
        }

        public void EnanbleMovement()
        {
            move = true;
        }

        public void DisableMovement()
        {
            move = false;
        }
    }
}