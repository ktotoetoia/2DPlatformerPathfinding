using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask ground = 1 << 6;

    private Collider2D collision;

    private float groundDistance;

    private void Start()
    {
        collision = GetComponent<Collider2D>();

        groundDistance = collision.bounds.size.y / 2f + collision.offset.y;
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size / 1.5f, 0f, Vector2.down, groundDistance, ground);
    }
}