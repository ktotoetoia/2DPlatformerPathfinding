using UnityEngine.Tilemaps;
using System.Collections.Generic;

public interface IGraphNodeCreator
{
    public List<IHorizontalSurface> AddNodesFromTilemap(Tilemap tilemap);
}