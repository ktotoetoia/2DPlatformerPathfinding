using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector2 accuracy;
    [SerializeField] private Vector2Int path;
    
    public IGridGraph graph { get; private set; }

    private IGraphNodeCreator graphCreator;
    private IGraphConnector graphConnector;
    private IJumpTrajectoriesConnector jumpTrajectoriesConnector;
    private IJumpTrajectoriesCreator jumpTrajectoriesCreator;
    private IPathUser pathUser;
    
    private DijkstraAlgorithm algorithm = new DijkstraAlgorithm();
    
    private void Awake()
    {
        InstantiateGraph();
        ConnectGraph();
    }

    private void Update()
    {
        INode prev = null;
        foreach(INode node in algorithm.GetPath(graph, graph.Nodes[path.x], graph.Nodes[path.y]))
        {
            if(prev != null)
            {
                Debug.DrawLine(prev.Position, node.Position);
            }

            prev = node;
        }
    }

    private void InstantiateGraph()
    {
        pathUser = CreatePathUser();
        graph = new GridGraph(tilemap.cellBounds.size.x, tilemap.cellBounds.size.y);

        graphCreator = new GridGraphNodeCreator(graph);
        graphConnector = new GridGraphConnector(graph);

        jumpTrajectoriesCreator = new JumpTrajectoriesCreator(pathUser, accuracy);
        jumpTrajectoriesConnector = new JumpTrajectoriesConnector(jumpTrajectoriesCreator, graph, pathUser);
    }

    private void ConnectGraph()
    {
        graphCreator.AddNodesFromTilemap(tilemap);
        graphConnector.ConnectGraph();

        jumpTrajectoriesConnector.GenerateJumpEdges();
    }

    private IPathUser CreatePathUser()
    {
        GameObject gameObject = FindObjectOfType<Movement>().gameObject;
        return new CapsulePathUser(gameObject.GetComponent<CapsuleCollider2D>(), new Vector3(8, 12));
    }

    public List<IEdge> GetPath(INode from, INode to)
    {
        return null;
    }
}