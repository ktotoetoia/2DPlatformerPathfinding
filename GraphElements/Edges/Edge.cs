using UnityEngine;

public class Edge : IEdge
{
    public INode From { get; }
    public INode To { get; }
    public float Distance { get; }
    public Edge(INode from, INode to)
    {
        From = from;
        To = to;
        from.Edges.Add(this);
        to.Edges.Add(this);
        Distance = Vector2.Distance(from.Position,to.Position);
    }

    public bool IsIncident(INode node)
    {
        return From == node || To == node;
    }
}