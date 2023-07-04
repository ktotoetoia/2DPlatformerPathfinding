using UnityEngine;

public class BoxPathUser : PathUser
{
    [SerializeField] private Vector3 SizeOffset;

    private BoxCollider2D boxCollider;

    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider2D>();
        Size = boxCollider.size * transform.localScale ;
        Offset = Size/2;
    }

    public override bool WillCollideAtPosition(Vector2 center)
    {
        return Physics2D.OverlapBox(center, Size + SizeOffset, 0, groundLayer);
    }
}