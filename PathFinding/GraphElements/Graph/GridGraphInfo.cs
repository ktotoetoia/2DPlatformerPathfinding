using UnityEngine;

public class GridGraphInfo : IGridGraphInfo
{
    public INode Node { get; set; }
    public Vector3Int GridPosition { get; set; }

    public GridGraphInfo(INode node, Vector3Int gridPosition)
    {
        Node = node;
        GridPosition = gridPosition;
    }
}

public interface IGridGraphInfo
{
    public INode Node { get; set; }
    public Vector3Int GridPosition { get; set; }
}