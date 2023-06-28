using UnityEngine.Tilemaps;
using UnityEngine;

public class GridGraphNodeCreator : IGraphNodeCreator
{
    private IGridGraph graph;
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0);
    public GridGraphNodeCreator(IGridGraph graph)
    {
        this.graph = graph;
    }

    public void AddNodesFromTilemap(Tilemap tilemap)
    {
        INode prevNode = null;

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            INode node = TryAddNode(tilemap, position);

            if (node != null && prevNode != null)
            {
                graph.Connect(node, prevNode);
            }

            prevNode = node;
        }
    }

    private INode TryAddNode(Tilemap tilemap, Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);
        TileBase downTile = tilemap.GetTile(new Vector3Int(position.x, position.y - 1));

        if (tile == null && downTile != null)
        {
            return AddNodeToGraph(tilemap, position);
        }

        return null;
    }

    private INode AddNodeToGraph(Tilemap tilemap, Vector3Int position)
    {
        Vector3Int gridPosition = position - tilemap.cellBounds.min;

        INode node = graph.AddNode(GetNodeType(position, tilemap), position + offset, gridPosition);

        return node;
    }

    private NodeType GetNodeType(Vector3Int position, Tilemap tilemap)
    {
        bool hasLeftTile = tilemap.GetTile(new Vector3Int(position.x - 1, position.y - 1)) != null;
        bool hasRightTile = tilemap.GetTile(new Vector3Int(position.x + 1, position.y - 1)) != null;

        if (hasRightTile && hasLeftTile)
            return NodeType.Platform;
        if (!hasRightTile && !hasLeftTile)
            return NodeType.Solo;
        if (!hasLeftTile)
            return NodeType.LeftEdge;
        else
            return NodeType.RightEdge;
    }
}
