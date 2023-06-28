using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Node : INode
{
    public NodeType Type { get; }
    public Vector3 Position { get; }
    public List<IEdge> Edges { get; set; } = new List<IEdge>();

    public Node(NodeType type, Vector3 position)
    {
        Type = type;
        Position = position;
    }

    public bool IsIncident(INode other)
    {
        return Edges.Any(x => x.IsIncident(other));
    }
}