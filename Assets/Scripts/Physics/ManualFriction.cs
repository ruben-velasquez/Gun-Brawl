using UnityEngine;

public class ManualFriction : MonoBehaviour
{
    [SerializeField]
    private float frictionForce = 10f;
    private Rigidbody2D rb;
    private bool isOnGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isOnGround)
        {
            rb.AddForce(-rb.velocity * frictionForce);
        }
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