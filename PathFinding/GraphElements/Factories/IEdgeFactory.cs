public interface IEdgeFactory
{
    public IEdge Create(INode from, INode to, EdgeMoveInfo moveInfo);
}