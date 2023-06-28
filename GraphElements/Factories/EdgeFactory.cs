using UnityEngine;

public class EdgeFactory : IEdgeFactory
{
    public IEdge Create(INode from, INode to)
    {
        return new Edge(from, to);
    }
}

public interface IEdgeFactory
{
    public IEdge Create(INode from, INode to);
}