using System.Collections.Generic;
using UnityEngine;

public interface INode : IHasEdges
{
    public NodeType Type { get; }
    public Vector3 Position { get; }

    bool IsIncident(INode other);
}

public interface IHasEdges
{
    public List<IEdge> Edges { get; }
}