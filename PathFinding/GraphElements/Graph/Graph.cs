using System.Collections.Generic;
using UnityEngine;

public class Graph : IGraph
{
    private List<INode> nodes = new List<INode>();
    private List<IEdge> edges = new List<IEdge>();

    private INodeFactory nodeFactory = new NodeFactory();
    private IEdgeFactory singleEdgeFactory = new SingleEdgeFactory();
    public List<INode> Nodes { get { return nodes; } }
    public List<IEdge> Edges { get { return edges; } }

    public INode AddNode(NodeType type, Vector3 position)
    {
        INode node = nodeFactory.Create(type,position);
        nodes.Add(node);
        return node;
    }

    public IEdge Connect(INode from, INode to, EdgeMoveInfo moveInfo)
    {
        IEdge edge = singleEdgeFactory.Create(from, to, moveInfo);
        
        if(Nodes.Contains(from) && nodes.Contains(to))
        {
            edges.Add(edge);
        }

        return edge;
    }

    public INode GetNearestNode(Vector2 position)
    {
        INode result = Nodes[0];

        foreach (INode node in Nodes)
        {
            if (Vector2.Distance(position, node.Position) < Vector2.Distance(position, result.Position))
            {
                result = node;
            }
        }

        return result;
    }
}

public enum NodeType
{
    None,
    Platform,
    LeftEdge,
    RightEdge,
    Solo,
}