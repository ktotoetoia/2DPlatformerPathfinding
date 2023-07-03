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

    public List<JumpTrajectory> GetJumpTrajectories(INode node)
    {
        return GetJumpTrajectories(node,accuracy);
    }
    
    public List<JumpTrajectory> GetJumpTrajectories(INode node,Vector2 accuracy)
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

        JumpTrajectory rightTrajectory = new JumpTrajectory(node, node.Position, new Vector2(jumpSpeed,jumpHeight));
        JumpTrajectory leftTrajectory = new JumpTrajectory(node, node.Position, new Vector2(-jumpSpeed, jumpHeight));

        jumpTrajectories.Add(rightTrajectory);
        jumpTrajectories.Add(leftTrajectory);

        return jumpTrajectories;
    }
}