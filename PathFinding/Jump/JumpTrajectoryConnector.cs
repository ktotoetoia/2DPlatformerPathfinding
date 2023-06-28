using UnityEngine;
public class JumpTrajectoriesConnector : IJumpTrajectoriesConnector
{
    private float stepCount = 100;
    private float timeStepMultiplier = 10;
    private float minDistanceToNode = 0.3f;

    private IJumpTrajectoriesCreator jumpTrajectoriesCreator;
    private IGraphCanConnect graph;
    private IPathUser pathUser;
    
    public JumpTrajectoriesConnector(IJumpTrajectoriesCreator jumpTrajectoriesCreator, IGraphCanConnect graph,IPathUser pathUser)
    {
        this.graph = graph;
        this.pathUser = pathUser;
        this.jumpTrajectoriesCreator = jumpTrajectoriesCreator;
    }

    public void GenerateJumpEdges()
    {
        foreach (JumpTrajectory jumpTrajectory in jumpTrajectoriesCreator.GetJumpTrajectories(graph))
        {
            TryConnectTrajectory(jumpTrajectory,pathUser);
        }
    }

    private void TryConnectTrajectory(JumpTrajectory jumpTrajectory,IPathUser pathUser)
    {
        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations * timeStepMultiplier;
        float drag = 1f - timeStep * pathUser.Rigidbody.drag;

        Vector2 gravityAccel = Physics2D.gravity * pathUser.Rigidbody.gravityScale * timeStep * timeStep;
        Vector2 velocity = jumpTrajectory.Velocity;
        Vector2 moveStep = velocity * timeStep;
        Vector2 currentPosition = jumpTrajectory.Position;

        for(int i = 0; i < stepCount; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;

            currentPosition += moveStep;

            if (CloseToNode(jumpTrajectory, currentPosition) || pathUser.WillTouchAtPosition(currentPosition))
            {
                return;
            }
        }
    }

    public bool CloseToNode(JumpTrajectory jumpTrajectory, Vector2 pos)
    {
        INode node = GetNearestNode(pos);

        if (node.Position.y != jumpTrajectory.Node.Position.y && InRange(node.Position,pos))
        {
            graph.ConnectSingle(jumpTrajectory.Node,node);
            return true;
        }

        return false;
    }

    public INode GetNearestNode(Vector2 position)
    {
        INode result = graph.Nodes[0];

        foreach (INode node in graph.Nodes)
        {
            if (Vector2.Distance(position, node.Position) < Vector2.Distance(position, result.Position))
            {
                result = node;
            }
        }

        return result;
    }

    public bool InRange(Vector3 from, Vector3 to)
    {
        return Vector2.Distance(from, to) < minDistanceToNode;
    }
}