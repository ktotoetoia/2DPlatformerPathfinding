public class SingleEdgeFactory : IEdgeFactory
{
    public IEdge Create(INode from, INode to)
    {
        return new SingleEdge(from, to);
    }
}