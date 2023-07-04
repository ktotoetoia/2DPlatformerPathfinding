using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public class GridGraphNodeCreator : IGraphNodeCreator
{
    private IGridGraph graph;
    private Vector3 offset;

    public GridGraphNodeCreator(IGridGraph graph,IPathUser pathUser)
    {
        this.graph = graph;
        offset = new Vector3(0.5f, pathUser.Offset.y);
    }

    public List<IHorizontalSurface> AddNodesFromTilemap(Tilemap tilemap)
    {
        SetOffset(tilemap);
        List<IHorizontalSurface> horizontalSurfaces = new List<IHorizontalSurface>();
        List<INode> horizontalNodes = new List<INode>();

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            INode node = TryAddNode(tilemap, position);

            if (node != null)
            {
                horizontalNodes.Add(node);
                continue;
            }
        
            if(horizontalNodes.Count > 0)
            {
                horizontalSurfaces.Add(CreateHorizontalSurface(horizontalNodes));
            }
        }

        return horizontalSurfaces;
    }

    private void SetOffset(Tilemap tilemap)
    {
        offset.x = tilemap.cellSize.x/2;
    }

    private IHorizontalSurface CreateHorizontalSurface(List<INode> horizontalNodes)
    {
        IHorizontalSurface horizontalSurface = new HorizontalSurface(graph, horizontalNodes);
        horizontalNodes.Clear();
        return horizontalSurface;
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

        INode node = graph.AddNode(GetNodeType(tilemap, position), position + offset, gridPosition);

        return node;
    }

    private NodeType GetNodeType(Tilemap tilemap, Vector3Int position)
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