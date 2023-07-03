using UnityEngine;

public interface IPathUser
{
    public Vector3 MaxVelocity { get; }
    public Vector3 Offset { get; }
    public Vector3 Size { get; }
    public Rigidbody2D Rigidbody { get;  }

    public bool WillCollideAtPosition(Vector2 center);
}