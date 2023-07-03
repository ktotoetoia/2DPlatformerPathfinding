using System.Collections.Generic;

public interface IPathfindingAlgorithm
{
    public List<INode> GetPath(List<INode> Nodes, INode start, INode end);
}