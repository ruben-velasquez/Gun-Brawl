using UnityEngine;

namespace Fighter {
    public class FighterMovement : FighterJump {
        // Variables públicas
        [Space]
        [Header("Movement")]
        public float speed = 5f; // Velocidad de movimiento
        public bool facingRight = true; // Dirección a la que mira el personaje
        public bool walking;
        
        // Método que se ejecuta en cada fotograma fijo
        public void Move(float input)
        {
            if(input != 0)
                walking = true;
            else
                walking = false;

            // Movemos el personaje según la entrada horizontal y la velocidad
            rb.velocity = new Vector2(input * speed, rb.velocity.y);

            // Giramos el personaje según la dirección a la que se mueve
            if (input > 0 && !facingRight)
            {
                Flip();
            }
            else if (input < 0 && facingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}