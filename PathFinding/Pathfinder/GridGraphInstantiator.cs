using UnityEngine.Tilemaps;
using UnityEngine;

public class GridGraphInstantiator
{
    public IGridGraph graph { get; private set; }
    public IGraphNodeCreator graphCreator { get; private set; }
    public IGraphConnector graphConnector { get; private set; }
    public IJumpTrajectoriesCreator jumpTrajectoriesCreator { get; private set; }
    public IJumpTrajectoriesConnector jumpTrajectoriesConnector { get; private set; }

    public IGridGraph CreateGraph(Tilemap tilemap,IPathUser pathUser, Vector2 accuracy)
    {
        graph = new GridGraph(tilemap.cellBounds.size.x, tilemap.cellBounds.size.y);

        graphCreator = new GridGraphNodeCreator(graph,pathUser);
        jumpTrajectoriesCreator = new JumpTrajectoriesCreator(pathUser, accuracy);
        
        graphConnector = new GridGraphUltraConnector(graph,pathUser);
        jumpTrajectoriesConnector = new JumpTrajectoriesConnector(jumpTrajectoriesCreator, graph, pathUser);

        graph.HorizontalSurfaces = graphCreator.AddNodesFromTilemap(tilemap);
        graphConnector.ConnectGraph();
        jumpTrajectoriesConnector.GenerateJumpEdges();

        return graph;
    }
}