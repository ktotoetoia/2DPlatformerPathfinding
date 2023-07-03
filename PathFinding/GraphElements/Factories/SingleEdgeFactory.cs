public class SingleEdgeFactory : IEdgeFactory
{
    public IEdge Create(INode from, INode to, EdgeMoveInfo moveInfo)
    {
        return new SingleEdge(from, to,moveInfo);
    }
}