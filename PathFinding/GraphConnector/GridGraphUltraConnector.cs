using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridGraphUltraConnector : IGraphConnector
{
    private IGridGraph graph;
    private int step = 2;
    
    public GridGraphUltraConnector(IGridGraph gridGraph,IPathUser pathUser)
    {
        graph = gridGraph;
        step = (int)pathUser.Size.x+1;
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
            AddFall(node, graphInfo.GridPosition + new Vector3Int(-step, -1), 1);
        }

        if (node.Type == NodeType.Solo || node.Type == NodeType.RightEdge)
        {
            AddFall(node, graphInfo.GridPosition + new Vector3Int(step, -1), -1);
        }
    }

    private void AddFall(INode node, Vector3Int position,int direction)
    {
        if (position.x >= 0 && position.y >= 0 && position.x < graph.Rows && position.y < graph.Columns)
        {
            List<INode> nodes = new List<INode>();

            for(int i  = 0; i < step; i++)
            {
                nodes.Add(GetFall(position + new Vector3Int(i * direction, 0)));
            }
            
            if(nodes.All(x => x != null && x.Position.y == nodes[0].Position.y ))
            {
                graph.Connect(node, nodes[0], EdgeMoveInfo.FallMoveInfo);
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