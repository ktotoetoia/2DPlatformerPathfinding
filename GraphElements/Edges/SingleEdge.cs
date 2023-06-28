using UnityEngine;

public class SingleEdge : IEdge
{
    public INode From { get; }
    public INode To { get; }
    public float Distance { get; }

    public SingleEdge(INode from, INode to)
    {
        From = from;
        To = to;
        from.Edges.Add(this);
        Distance = Vector2.Distance(from.Position, to.Position);
    }

    public bool IsIncident(INode node)
    {
        return From == node || To == node;
    }

    public INode OtherNode(INode node)
    {
        return node == From ? To : From;
    }
}