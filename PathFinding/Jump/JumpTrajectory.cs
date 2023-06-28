using UnityEngine;

public class JumpTrajectory
{
    public INode Node { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector2 Velocity;

    public JumpTrajectory(INode node, Vector3 position,Vector2 velocity)
    {
        Position = position;
        Velocity = velocity;
        Node = node;
    }
}