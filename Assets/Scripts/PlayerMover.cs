using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private bool inputMovement;
    private float moveSpeed = 10.0f;
    private float direction;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.linearVelocityY += 10.0f;
    }

    public void StopMovement()
    {
        inputMovement = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 10.0f);
    }
}
