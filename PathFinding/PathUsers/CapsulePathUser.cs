using UnityEngine;

public class CapsulePathUser : IPathUser
{
    private CapsuleCollider2D capsuleCollider;
    public Rigidbody2D Rigidbody { get; private set; }
    public Collider2D Collider { get { return capsuleCollider; } }
    public Vector3 MaxVelocity { get; }

    public CapsulePathUser(CapsuleCollider2D collider, Vector3 maxVelocity)
    {
        capsuleCollider = collider;
        Rigidbody = collider.attachedRigidbody;
        MaxVelocity = maxVelocity;
    }

    public bool WillTouchAtPosition(Vector2 center)
    {
        return Physics2D.OverlapCapsule(center, capsuleCollider.size- Vector2.one/10,CapsuleDirection2D.Vertical,0);
    }
}