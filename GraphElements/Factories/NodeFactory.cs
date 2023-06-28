using UnityEngine;

public class NodeFactory : INodeFactory
{
    public INode Create(NodeType type,Vector3 position)
    {
        return new Node(type,position);
    }
}

public interface INodeFactory
{
    public INode Create(NodeType type, Vector3 position);
}