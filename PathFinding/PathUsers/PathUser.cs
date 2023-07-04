using UnityEngine;

public class PathUser : MonoBehaviour, IPathUser
{
    [SerializeField] protected LayerMask groundLayer = 1 << 6;
    [field: SerializeField] public Vector3 MaxVelocity { get; protected set; }
    public Vector3 Offset { get; protected set; }
    public Vector3 Size { get; protected set; }
    public virtual Rigidbody2D Rigidbody { get; protected set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual bool WillCollideAtPosition(Vector2 center)
    {
        return true;
    }
}