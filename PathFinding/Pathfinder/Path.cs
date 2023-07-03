using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{
    public List<INode> Nodes { get; }
    public List<IEdge> Edges { get; } = new List<IEdge>();

    public bool arrived { get { return Nodes.Count == 0; } }
    
    public INode Current { get { return Nodes.FirstOrDefault(); } }
    public INode Previous { get; set; }

    public EdgeMoveInfo MoveInfo
    { 
        get 
        {
            return (Previous?.Edges.Find(x => x.IsIncident(Current)) ?? Edges[0]).MoveInfo; 
        }
    }

    public Path(List<INode> nodes)
    {
        Nodes = nodes;
        InstantiateEdges();
    }

    private void InstantiateEdges()
    {
        INode previous = null;

        foreach (INode node in Nodes)
        {
            if (previous != null)
            {
                Edges.Add(previous.Edges.Find(x => x.IsIncident(node)));
            }

            previous = node;
        }
    }

    public void Update(Collider2D collider)
    {
        if(collider.OverlapPoint(Current.Position))
        {
            Arrived();
        }
    }

    public void Arrived()
    {
        Previous = Current;
        Nodes.RemoveAt(0);
    }
}