using System.Collections.Generic;

public interface IJumpTrajectoriesCreator
{
    public List<JumpTrajectory> GetJumpTrajectories(IGraphCanConnect graph);
}
