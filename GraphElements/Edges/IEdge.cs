public interface IEdge
{
    public INode From { get; }
    public INode To { get; }
    float Distance { get; }

    public bool IsIncident(INode node)
    {
        return From == node || To == node;
    }

    public INode OtherNode(INode node)
    {
        return node == From ? To : From;
    }
}