using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class DynamicGraphCreator
{
    private GridGraphInstantiator instantiator;
    private IGridGraph graph;
    private Vector2 accuracy;
    
    public DynamicGraphCreator(GridGraphInstantiator instantiator, IGridGraph graph, Vector2 accuracy)
    {
        this.instantiator = instantiator;
        this.graph = graph;
        this.accuracy = accuracy;
    }

    public INode CreateDynamicNode(Vector2 position)
    {
        INode node = new Node(NodeType.None, position);
        ConnectNode(node);
        return node;
    }

    private void ConnectNode(INode node)
    {
        instantiator.jumpTrajectoriesConnector.GenerateJumpEdges(node,accuracy);
     
        IHorizontalSurface surface = graph.HorizontalSurfaces.OrderBy(x => x.Distance(node.Position)).First();
        
        List<IEdge> toRemove = new List<IEdge>();

        foreach (IEdge edge in node.Edges)
        {
            if(surface.Nodes.Any(x => edge.IsIncident(x)))
            {
                toRemove.Add(edge);
            }
        }

        node.Edges.RemoveAll(x => toRemove.Contains(x));
        surface.Insert(node);
    }
}