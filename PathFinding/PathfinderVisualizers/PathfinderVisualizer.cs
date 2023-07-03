using UnityEngine;

public class PathfinderVisualizer : MonoBehaviour
{
    [SerializeField] private bool enabled;
    private IGridGraph graph;

    private void Start()
    {
        graph = FindAnyObjectByType<Pathfinder>().graph;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && enabled)
        {
            DrawNodes();
            DrawEdges();
        }
    }

    private void DrawNodes()
    {
        foreach (INode node in graph.Nodes)
        {
            if (node == null) continue;

            switch (node.Type)
            {
                case NodeType.Solo:
                    Gizmos.color = Color.black;
                    break;
                case NodeType.LeftEdge:
                    Gizmos.color = Color.red;
                    break;
                case NodeType.RightEdge:
                    Gizmos.color = Color.blue;
                    break;
                case NodeType.Platform:
                    Gizmos.color = Color.white;
                    break;
            }

            Gizmos.DrawSphere(node.Position, 0.2f);
        }
    }

    private void DrawEdges()
    {
        foreach (IEdge edge in graph.Edges)
        {
            Gizmos.color = Color.cyan;

            if (edge.MoveInfo.EdgeMoveWay == EdgeMoveWay.horizontalMove)
            {
                Gizmos.color = Color.black;
            }

            Gizmos.DrawLine(edge.From.Position, edge.To.Position);
        }
    }
}