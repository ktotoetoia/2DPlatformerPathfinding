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

            foreach (IEdge edge in graph.Edges)
            {
                Gizmos.color = Color.black;
                
                if (edge is SingleEdge)
                {
                    Gizmos.color = Color.cyan;
                }

                Gizmos.DrawLine(edge.From.Position, edge.To.Position);
            }
        }
    }
}