using System.Collections.Generic;
using UnityEngine;

public class GridGraph : IGridGraph
{
    private IGraph graph = new Graph();
    public List<IHorizontalSurface> HorizontalSurfaces { get; set; }
    public IGridGraphInfo[,] GridGraphInfos { get; }
    public int Rows { get { return GridGraphInfos.GetLength(0); } }
    public int Columns { get { return GridGraphInfos.GetLength(1); } }

    public List<INode> Nodes
    {
        get { return graph.Nodes; }
    }

    public List<IEdge> Edges
    {
        get { return graph.Edges; }
    }

    public GridGraph(int rows, int columns)
    {
        GridGraphInfos = new IGridGraphInfo[rows, columns];
    }

    public INode AddNode(NodeType type, Vector3 worldPosition, Vector3Int position)
    {
        INode node = graph.AddNode(type, worldPosition);
        CreateGraphInfo(node,position);
        return node;
    }

    private void CreateGraphInfo(INode node, Vector3Int position)
    {
        GridGraphInfos[position.x, position.y] = new GridGraphInfo(node, position);
    }

    public IEdge Connect(INode from, INode to, EdgeMoveInfo moveInfo)
    {
        return graph.Connect(from, to, moveInfo);
    }

    public INode GetNearestNode(Vector2 position)
    {
        return graph.GetNearestNode(position);
    }
}