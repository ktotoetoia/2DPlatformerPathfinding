using System.Collections.Generic;
using UnityEngine;

public interface IGraph : IConnectableGraph
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
    public IEdge Connect(INode from, INode to, EdgeMoveInfo moveInfo);
}

public interface IGridGraph : IConnectableGraph
{
    public int Rows { get; }
    public int Columns { get; }
    public List<IHorizontalSurface> HorizontalSurfaces { get; set; }
    public IGridGraphInfo[,] GridGraphInfos { get; }
    public INode AddNode(NodeType type, Vector3 worldPosition, Vector3Int position);
}

public interface IConnectableGraph : IHasGraphNodes, ICanConnectNodes
{
    public INode GetNearestNode(Vector2 position);
}