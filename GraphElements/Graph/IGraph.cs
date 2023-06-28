using System.Collections.Generic;
using UnityEngine;

public interface IGraph : IGraphCanConnect
{
    public INode AddNode(NodeType type, Vector3 position);
}

public interface IHasGraphNodes
{
    public List<INode> Nodes { get; }
    public List<IEdge> Edges { get; }
}

public interface ICanConnectNodes
{
    public IEdge Connect(INode from, INode to);
    public IEdge ConnectSingle(INode from, INode to);
}

public interface IGridGraph : IGraphCanConnect
{
    public int Rows { get; }
    public int Columns { get; }
    public IGridGraphInfo[,] GridGraphInfos { get; }
    public INode AddNode(NodeType type, Vector3 worldPosition, Vector3Int position);
}

public interface IGraphCanConnect : IHasGraphNodes, ICanConnectNodes
{

}