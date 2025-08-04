using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private bool inputMovement;
    private readonly float moveSpeed = 10.0f;
    private float direction;
    private Rigidbody2D rb;
    private Vector3 bottomOffset; // Where the bottom of the player is in relation to their center.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float halfHeight = GetComponent<BoxCollider2D>().size.y / 2.0f;
        bottomOffset = new Vector3(0, -halfHeight - 0.01f, 0.0f);
    }

    private void Update()
    {
        if (inputMovement)
        {
            rb.linearVelocityX = direction * moveSpeed;
        }
    }

    public void Move(float direction)
    {
        inputMovement = true;
        this.direction = direction;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocityY += 10.0f;
        }
    }

    public void StopMovement()
    {
        inputMovement = false;
        rb.linearVelocityX = 0;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position + bottomOffset, Vector2.down, 0.1f);
    }
}
