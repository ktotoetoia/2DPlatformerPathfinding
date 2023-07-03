using UnityEngine;

public class CapsulePathUser : PathUser
{
    [SerializeField] private Vector2 SizeOffset = Vector2.one;

    private CapsuleCollider2D capsuleCollider;

    protected override void Awake()
    {
        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Size = capsuleCollider.size * transform.localScale;
        Offset = Size/2;
    }

    public override bool WillCollideAtPosition(Vector2 center)
    {
        return Physics2D.OverlapCapsule(center, capsuleCollider.size + SizeOffset, CapsuleDirection2D.Vertical, 0, groundLayer);
    }
}