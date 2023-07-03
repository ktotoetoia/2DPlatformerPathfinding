public interface IEdge
{
    public INode From { get; }
    public INode To { get; }
    public float Distance { get; }
    public EdgeMoveInfo MoveInfo { get; set; }

    public bool IsIncident(INode node)
    {
        return From == node || To == node;
    }

    public INode OtherNode(INode node)
    {
        return node == From ? To : From;
    }
}