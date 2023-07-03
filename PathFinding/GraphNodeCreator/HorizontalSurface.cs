using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HorizontalSurface : IHorizontalSurface
{
    public List<INode> Nodes { get; private set; }
    private IConnectableGraph graph;

    public HorizontalSurface(IConnectableGraph graph,List<INode> nodes)
    {
        Nodes = new List<INode>(nodes);
        this.graph = graph;

        ConnectAllNodes();
    }

    private void ConnectAllNodes()
    {
        INode previous = null;

        foreach(INode node in Nodes)
        {
            if(previous != null)
            {
                ConnectWithEdge(previous, node);
                ConnectWithEdge(node, previous);
            }

            previous = node;
        }
    }

    public void Insert(INode nodeToInsert)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (nodeToInsert.Position.x <= Nodes[i].Position.x)
            {
                ConnectWithEdge(nodeToInsert, Nodes[i]);

                if (i > 0)
                {
                    ConnectWithEdge(nodeToInsert, Nodes[i-1]);
                }
                return;
            }
        }

        ConnectWithEdge(nodeToInsert, Nodes.Last());
    }

    private void ConnectWithEdge(INode from, INode to)
    {
        graph.Connect(from, to, EdgeMoveInfo.HorizontalMoveInfo);
    }

    public float Distance(Vector3 position)
    {
        INode closestNode = Nodes.FirstOrDefault();
        
        foreach(INode node in Nodes)
        {
            if(Vector3.Distance(node.Position,position) < Vector3.Distance(closestNode.Position,position))
            {
                closestNode = node;
            }
        }

        return Vector3.Distance(position,closestNode.Position);
    }
}