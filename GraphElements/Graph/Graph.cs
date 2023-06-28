using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    None,
    Platform,
    LeftEdge,
    RightEdge,
    Solo,
}

public class Graph : IGraph
{
    private List<INode> nodes = new List<INode>();
    private List<IEdge> edges = new List<IEdge>();

    private INodeFactory nodeFactory = new NodeFactory();
    private IEdgeFactory edgeFactory = new EdgeFactory();
    private IEdgeFactory singleEdgeFactory = new SingleEdgeFactory();
    public List<INode> Nodes { get { return nodes; } }
    public List<IEdge> Edges { get { return edges; } }

    public INode AddNode(NodeType type, Vector3 position)
    {
        INode node = nodeFactory.Create(type,position);
        nodes.Add(node);
        return node;
    }

    public IEdge Connect(INode from, INode to)
    {
        if (from.IsIncident(to) && to.IsIncident(from)) return null;
        return CreateEdge(edgeFactory, from, to);
    }

    public IEdge ConnectSingle(INode from, INode to)
    {
        if (from.IsIncident(to)) return null;
        return CreateEdge(singleEdgeFactory,from, to);
    }

    private IEdge CreateEdge(IEdgeFactory factory, INode from, INode to)
    {
        IEdge edge = factory.Create(from,to);
        edges.Add(edge);
        return edge;
    }
}