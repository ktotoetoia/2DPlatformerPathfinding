using UnityEngine;

public class CirclePathUser : PathUser
{
    [SerializeField] private float radiusOffset = 0;

    private CircleCollider2D circleCollider;

    protected override void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();
        Offset = circleCollider.radius / 2 * transform.localScale;
        Size = Vector3.one * circleCollider.radius;
    }

    public override bool WillCollideAtPosition(Vector2 center)
    {
        return Physics2D.OverlapCircle(center, circleCollider.radius + radiusOffset, 0, groundLayer);
    }
}