using UnityEngine;

public class SingleEdge : IEdge
{
    public INode From { get; protected set; }
    public INode To { get; protected set; }
    public float Distance { get; protected set; }

    public EdgeMoveInfo MoveInfo { get; set; }

    public SingleEdge(INode from, INode to, EdgeMoveInfo moveInfo)
    {
        From = from;
        To = to;
        MoveInfo = moveInfo;
        Distance = Vector2.Distance(from.Position, to.Position);
     
        From.Edges.Add(this);
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