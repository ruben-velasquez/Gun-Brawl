using UnityEngine;

public class ManualFriction : MonoBehaviour
{
    [SerializeField]
    private Vector2 globalRaySideOffset = new Vector2(0.25f, 0f);
    [SerializeField]
    private float rayLength = 0.85f;
    [SerializeField]
    private Vector2 boxSize = new Vector2(1f, 1f);
    [SerializeField]
    private LayerMask groudMask;
    [SerializeField]
    private float frictionForce = 10f;
    private Rigidbody2D rb;
    private bool isOnGround;
    private bool isOnGroundAbove;
    private GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        gameManager.onPause += OnPause;
        gameManager.onResume += OnResume;
        gameManager.onMatchEnd += OnMatchEnd;
    }

    private void OnPause() => rb.simulated = false;
    private void OnResume() => rb.simulated = true;
    private void OnMatchEnd() {
        gameManager.onPause -= OnPause;
        gameManager.onResume -= OnResume;
        gameManager.onMatchEnd -= OnMatchEnd;
    }

    private void Update()
    {
        CheckGroundAbove();

        if (isOnGround && isOnGroundAbove)
        {
            rb.AddForce(-rb.velocity * frictionForce);
        }
    }

    private void CheckGroundAbove()
    {
        if(!isOnGround) return;
        Vector2 positionCenter = (Vector2)transform.position + new Vector2(0, globalRaySideOffset.y);
        Vector2 direction = Vector2.down;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(positionCenter, boxSize, 0f, direction, rayLength, groudMask);

        // Dibujar el BoxCast
        Debug.DrawRay(positionCenter, direction * rayLength, Color.red);

        // Dibujar el cubo
        Vector2 topLeft = positionCenter + new Vector2(-boxSize.x, boxSize.y) / 2;
        Vector2 topRight = positionCenter + new Vector2(boxSize.x, boxSize.y) / 2;
        Vector2 bottomLeft = positionCenter + new Vector2(-boxSize.x, -boxSize.y) / 2;
        Vector2 bottomRight = positionCenter + new Vector2(boxSize.x, -boxSize.y) / 2;

        Debug.DrawRay(topLeft, Vector2.right * boxSize.x, Color.red);
        Debug.DrawRay(topLeft, Vector2.down * boxSize.y, Color.red);
        Debug.DrawRay(bottomRight, Vector2.left * boxSize.x, Color.red);
        Debug.DrawRay(bottomRight, Vector2.up * boxSize.y, Color.red);

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != this.gameObject)
            {
                isOnGroundAbove = true;
                return;
            }
        }

        isOnGroundAbove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isOnGround && (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Platform")))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = false;
        }
    }
}