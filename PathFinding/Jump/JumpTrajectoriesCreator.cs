using System.Collections.Generic;
using UnityEngine;

public class JumpTrajectoriesCreator : IJumpTrajectoriesCreator
{
    private Vector2 maxVelocity = Vector2.zero;

    private Vector2 accuracy;
    public JumpTrajectoriesCreator(IPathUser pathUser,Vector2 accuracy)
    {
        maxVelocity = pathUser.MaxVelocity;
        this.accuracy = accuracy;
    }

    public List<JumpTrajectory> GetJumpTrajectories(IGraphCanConnect graph)
    {
        List<JumpTrajectory> jumpTrajectoriesToValidate = new();

        foreach (INode node in graph.Nodes)
        {
            if (node != null)
            {
                jumpTrajectoriesToValidate.AddRange(GetJumpTrajectories(node));
            }
        }

        return jumpTrajectoriesToValidate;
    }

    private List<JumpTrajectory> GetJumpTrajectories(INode node)
    {
        List<JumpTrajectory> jumpTrajectories = new List<JumpTrajectory>();

        for (float i = 1; i <= accuracy.y; i++)
        {
            float jumpHeight = maxVelocity.y * (i / accuracy.y);

            for (float j = 1; j <= accuracy.x; j++)
            {
                float jumpSpeed = maxVelocity.x * (j / accuracy.x);

                jumpTrajectories.AddRange(GetLeftRightTrajectory(node, jumpHeight, jumpSpeed));
            }
        }

        return jumpTrajectories;
    }

    private List<JumpTrajectory> GetLeftRightTrajectory(INode node, float jumpHeight, float jumpSpeed)
    {
        List<JumpTrajectory> jumpTrajectories = new List<JumpTrajectory>();
        Vector2 position = node.Position + new Vector3(0, 0.3f);
        JumpTrajectory rightTrajectory = new JumpTrajectory(node, position, new Vector2(jumpSpeed,jumpHeight));
        JumpTrajectory leftTrajectory = new JumpTrajectory(node, position, new Vector2(-jumpSpeed, jumpHeight));

        jumpTrajectories.Add(rightTrajectory);
        jumpTrajectories.Add(leftTrajectory);

        return jumpTrajectories;
    }
}