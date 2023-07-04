using UnityEngine;

public class JumpTrajectoriesConnector : IJumpTrajectoriesConnector
{
    private float timeStepMultiplier = 5;
    private float minDistanceToNode = 0.2f;

    private IJumpTrajectoriesCreator jumpTrajectoriesCreator;
    private IGridGraph graph;
    private IPathUser pathUser;
    
    public JumpTrajectoriesConnector(IJumpTrajectoriesCreator jumpTrajectoriesCreator, IGridGraph graph,IPathUser pathUser)
    {
        this.graph = graph;
        this.pathUser = pathUser;
        this.jumpTrajectoriesCreator = jumpTrajectoriesCreator;
    }

    public void GenerateJumpEdges()
    {
        foreach (INode node in graph.Nodes)
        {
            GenerateJumpEdges(node);
        }
    }

    public void GenerateJumpEdges(INode node)
    {
        foreach (JumpTrajectory jumpTrajectory in jumpTrajectoriesCreator.GetJumpTrajectories(node))
        {
            TryConnectTrajectory(jumpTrajectory, pathUser);
        }
    }

    public void GenerateJumpEdges(INode node, Vector2 accuracy)
    {
        foreach (JumpTrajectory jumpTrajectory in jumpTrajectoriesCreator.GetJumpTrajectories(node,accuracy))
        {
            TryConnectTrajectory(jumpTrajectory, pathUser);
        }
    }

    private void TryConnectTrajectory(JumpTrajectory jumpTrajectory,IPathUser pathUser)
    {
        timeStepMultiplier = 200 / (pathUser.MaxVelocity.x + pathUser.MaxVelocity.y);
        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations * timeStepMultiplier;
        float drag = 1f - timeStep * pathUser.Rigidbody.drag;

        Vector2 gravityAccel = Physics2D.gravity * pathUser.Rigidbody.gravityScale * timeStep * timeStep;
        Vector2 moveStep = jumpTrajectory.Velocity * timeStep;
        Vector2 currentPosition = jumpTrajectory.Position;

        for(int i = 0; i < pathUser.MaxVelocity.y*10; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;

            currentPosition += moveStep;

            if (pathUser.WillCollideAtPosition(currentPosition) || ConnectIfCloseToNode(jumpTrajectory, currentPosition))
            {
                return;
            }
        }
    }
    
    public bool ConnectIfCloseToNode(JumpTrajectory jumpTrajectory, Vector2 pos)
    {
        INode node = graph.GetNearestNode(pos);

        if (!IsSameSurface(jumpTrajectory.Node, node) && InRange(node.Position,pos) && !jumpTrajectory.Node.IsIncident(node))
        {
            ConnectJump(jumpTrajectory, node);
            return true;
        }
        
        return false;
    }

    private bool IsSameSurface(INode first, INode second)
    {
        return graph.HorizontalSurfaces.Find(x => x.Nodes.Contains(first))?.Nodes.Contains(second) ?? false;
    }

    private void ConnectJump(JumpTrajectory jumpTrajectory,INode node)
    {
        EdgeMoveInfo edgeMoveInfo = new EdgeMoveInfo(jumpTrajectory.Velocity, EdgeMoveWay.jump);
        graph.Connect(jumpTrajectory.Node, node, edgeMoveInfo);
    }

    public bool InRange(Vector3 from, Vector3 to)
    {
        return Vector2.Distance(from, to) < minDistanceToNode;
    }
}