using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private PathUser pathUser;
    [SerializeField] private Vector2 accuracy;
    [SerializeField] private Vector2 dynamicAccuracy;

    private DynamicGraphCreator dynamicNodeCreator;
    private GridGraphInstantiator instantiator = new GridGraphInstantiator();
    private IPathfindingAlgorithm pathfindingAlgorithm = new DijkstraAlgorithm();

    public IGridGraph graph { get; private set; }

    private void Start()
    {
        InstantiateGraph();
    }

    private void InstantiateGraph()
    {
        graph = instantiator.CreateGraph(tilemap,pathUser,accuracy);
        dynamicNodeCreator = new DynamicGraphCreator(instantiator,graph,dynamicAccuracy);
    }

    public Path GetPath(Vector3 from, Vector3 to)
    {
        List<INode> nodes = new List<INode>(graph.Nodes);
        
        INode fromNode = dynamicNodeCreator.CreateDynamicNode(from);
        
        nodes.Add(fromNode);

        return new Path(pathfindingAlgorithm.GetPath(nodes, fromNode, graph.GetNearestNode(to)));
    }
}