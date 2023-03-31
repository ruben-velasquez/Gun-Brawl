using UnityEngine;

public class FighterJump : GroundCheck
{
    [Space]
    [Header("Jump")]
    // Variables públicas
    public float jumpForce; // La fuerza del salto

    public void Jump()
    {
        // Si el luchador está en el suelo, le aplica una fuerza hacia arriba
        if (grounded || jumpTime > 0) // Si se está en el suelo o el tiempo del coyote time es mayor a 0
        {
            grounded = false; // El luchador ya no está en el suelo
            jumpTime = 0; // Reiniciamos
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}