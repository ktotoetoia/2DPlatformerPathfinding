using UnityEngine;

public class Movement : MonoBehaviour , IMovable
{
    [SerializeField] private float speed = 8;
    [SerializeField] private float jumpForce = 12;

    public float Speed { get { return speed; } }

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        rigidbody.velocity = new Vector2(speed*direction,rigidbody.velocity.y);
    }

    public void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y + jumpForce);
    }
}