using UnityEngine;

public interface IPathUser
{
    public Rigidbody2D Rigidbody { get;  }
    public Collider2D Collider { get;  }

    public Vector3 MaxVelocity { get; }

    public bool WillTouchAtPosition(Vector2 center);
}