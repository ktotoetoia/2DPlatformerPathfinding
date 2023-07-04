using UnityEngine;

public class CirclePathUser : PathUser
{
    [SerializeField] private float radiusOffset = 0;
    
    private CircleCollider2D circleCollider;

    protected override void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();
        Size = circleCollider.radius * transform.localScale;
        Offset = Size / 2;
    }
    
    public override bool WillCollideAtPosition(Vector2 center)
    {
        return Physics2D.OverlapCircle(center, Size.x / 2 + radiusOffset, groundLayer);
    }
}