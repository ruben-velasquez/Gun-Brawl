using UnityEngine;

namespace Fighter
{
    public class FighterJump : GroundCheck
    {
        [Space]
        [Header("Jump")]
        // Variables públicas
        public float jumpForce; // La fuerza del salto
        public float minJumpDuration = 1; // Minima duración del salto
        private float LastJump = 0; // Momento en el que se hizo el último salto

        public void Jump()
        {
            // Si el luchador está en el suelo, le aplica una fuerza hacia arriba
            if (grounded || jumpTime > 0) // Si se está en el suelo o el tiempo del coyote time es mayor a 0
            {
                grounded = false; // El luchador ya no está en el suelo
                jumpTime = 0; // Reiniciamos
                LastJump = Time.time;
                animator.Play(jumpingAnimation);
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }


        // Esta función se ejecuta cuando no se está presionando el boton de saltar
        // Pero aún así el jugador sigue teniendo un impulso hacia arriba
        public void FollowJump() {
            if (Time.time - LastJump > minJumpDuration) {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
}