using System.Collections.Generic;
using UnityEngine;

public interface IJumpTrajectoriesCreator
{
    List<JumpTrajectory> GetJumpTrajectories(INode node);
    List<JumpTrajectory> GetJumpTrajectories(INode node, Vector2 accuracy);
}