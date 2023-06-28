using UnityEngine;

public class GridGraphConnector : IGraphConnector
{
    private IGridGraph graph;

    public GridGraphConnector(IGridGraph gridGraph)
    {
        graph = gridGraph;
    }

    public void ConnectGraph()
    {
        foreach (IGridGraphInfo graphInfo in graph.GridGraphInfos)
        {
            if (graphInfo != null)
                ConnectFall(graphInfo);
        }
    }

    private void ConnectFall(IGridGraphInfo graphInfo)
    {
        INode node = graphInfo.Node;

        if (node.Type == NodeType.Solo || node.Type == NodeType.LeftEdge)
        {
            AddFall(node, graphInfo.GridPosition + new Vector3Int(-1, -1));
        }

        if (node.Type == NodeType.Solo || node.Type == NodeType.RightEdge)
        {
            AddFall(node, graphInfo.GridPosition + new Vector3Int(1, -1));
        }
    }

    private void AddFall(INode node, Vector3Int position)
    {
        if (position.x >= 0 && position.y >= 0 && position.x < graph.Rows && position.y < graph.Columns)
        {
            INode fallNode = GetFall(position);

            if (fallNode != null)
            {
                graph.ConnectSingle(node, fallNode);
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
