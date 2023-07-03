using UnityEngine;

public interface INodeFactory
{
    public INode Create(NodeType type, Vector3 position);
}