using UnityEngine;

public class GroundCheck : Entity
{
    [Header("Ground Check")]
    public bool grounded; // Variable pública que indica si el objeto está en el suelo o no
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float checkLength = 0.75f; // Variable pública que define la longitud del chequeo
    [SerializeField]
    private Vector2 raycastDistance = new Vector2(0.2f, 0);
    
    [SerializeField]
    private float coyoteTime = 0.5f; // Variable pública que define el tiempo para poder saltar    
    [HideInInspector]
    public float jumpTime; // Variable privada que almacena el tiempo restante para poder saltar

    public virtual void Update()
    {
        // Lanza un rayo desde el centro del objeto hacia abajo solo en la capa "Ground" con la longitud definida
        RaycastHit2D hitLeft = Physics2D.Raycast((Vector2)transform.position - raycastDistance, Vector2.down, checkLength, groundLayer);
        RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, Vector2.down, checkLength, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast((Vector2)transform.position - raycastDistance, Vector2.down, checkLength, groundLayer);

        // Vista previa de los rayos
        Debug.DrawLine(transform.position, ((Vector2)transform.position - raycastDistance) + (Vector2.down * checkLength));
        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.down * checkLength));
        Debug.DrawLine(transform.position, ((Vector2)transform.position + raycastDistance) + (Vector2.down * checkLength));

        // Si el rayo colisiona con algo, el objeto está en el suelo y se le asigna el tiempo para poder saltar
        if (hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null)
        {
            grounded = true;
            jumpTime = coyoteTime;
        }
        else // Si no, el objeto está en el aire y se le resta el tiempo transcurrido al tiempo para poder saltar
        {
            grounded = false;
            jumpTime -= Time.deltaTime;
        }
    }
}