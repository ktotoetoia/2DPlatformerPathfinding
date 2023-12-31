﻿using UnityEngine;

public class GridGraphFallConnector : IGraphConnector
{
    private IGridGraph graph;

    private int step = 1;

    public GridGraphFallConnector(IGridGraph gridGraph)
    {
        graph = gridGraph;
    }

    public void ConnectGraph()
    {
        foreach (IGridGraphInfo graphInfo in graph.GridGraphInfos)
        {
            if (graphInfo != null)
            {
                ConnectFall(graphInfo);
            }
        }
    }

    private void ConnectFall(IGridGraphInfo graphInfo)
    {
        INode node = graphInfo.Node;

        if (node.Type == NodeType.Solo || node.Type == NodeType.LeftEdge)
        {
            AddFall(node, graphInfo.GridPosition + new Vector3Int(-step, -1));
        }

        if (node.Type == NodeType.Solo || node.Type == NodeType.RightEdge)
        {
            AddFall(node, graphInfo.GridPosition + new Vector3Int(step, -1));
        }
    }

    private void AddFall(INode node, Vector3Int position)
    {
        if (position.x >= 0 && position.y >= 0 && position.x < graph.Rows && position.y < graph.Columns)
        {
            INode fallNode = GetFall(position);

            if (fallNode != null)
            {
                graph.Connect(node, fallNode, EdgeMoveInfo.FallMoveInfo);
            }
        }
    }

    private INode GetFall(Vector3Int gridPosition)
    {
        for (int y = gridPosition.y; y >= 0; y--)
        {
            INode node = graph.GridGraphInfos[gridPosition.x, y]?.Node;
            
            if (node != null)
            {
                return node;
            }
        }

        return null;
    }
}