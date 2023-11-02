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
        [SerializeField]
        private float knockbackForce = 5;
        [SerializeField]
        private float knockbackDuration = 1;
        private bool onKnockback = false;
        private Vector2 savedVelocity = Vector2.zero;
        private bool waitingResume = false;

        public override void Update()
        {
            base.Update(); // Ejecutamos la lógica anterior

            if(gameManager.matchEnd) {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }

            if(!alive || (climbing && !move) || gameManager.paused) {
                rb.velocity = Vector2.zero;
                return;
            }

            // Si la variable move no está activa no ejecutamos nada
            if (!move)
            {
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

        public void EnableMovement()
        {
            if (gameManager.paused) return;

            if(waitingResume) {
                waitingResume = false;
                rb.velocity = savedVelocity;
            }

            if(rb != null)
                rb.drag = 0;

            move = true;
            onKnockback = false;
        }

        public void DisableMovement()
        {
            if (gameManager.paused) {
                waitingResume = true;
                savedVelocity = rb.velocity;
            }

            move = false;

            if(rb != null)
                rb.velocity = Vector2.zero;
        }

        public void Knockback(Vector2 direction) {
            if(!gameObject || climbing || onKnockback) return;
            
            onKnockback = true;

            // Desactivamos el movimiento
            DisableMovement();

            // Aplica una fuerza al Rigidbody en la dirección opuesta a la que la bala impactó
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

            rb.drag = 1;

            // Reactiva el movimiento después de un cierto tiempo
            Invoke("EnableMovement", knockbackDuration);
        }
    }
}