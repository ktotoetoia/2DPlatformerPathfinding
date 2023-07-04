using UnityEngine;

// Example class
public class PathMover : MonoBehaviour , IPathMover
{
    private IDirectionalMover mover;
    private IDirectionalJumper jumper;

    private Collider2D collider;
    private Pathfinder pathfinder;
    private Path path;
    private Vector2 target;

    public float Speed { get; set; }
    public float JumpForce { get; set; }
    
    private void Start()
    {
        mover = GetComponent<IDirectionalMover>();
        jumper = GetComponent<IDirectionalJumper>();
        collider = GetComponent<Collider2D>();
        pathfinder = FindObjectOfType<Pathfinder>();

        Speed = mover.Speed;
        JumpForce = jumper.JumpForce;

        jumper.OnJump += UpdatePath;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            path = pathfinder.GetPath(collider.bounds.center,target);
        }
    }

    private void FixedUpdate()
    {
        if (path == null) return;

        foreach(IEdge edge in path.Edges)
        {
            Debug.DrawLine(edge.From.Position, edge.To.Position);
        }

        if (path.arrived)
        {
            mover.StopMoving();
            return;
        }

        FollowPath();
    }

    private void FollowPath()
    {
        if (path.MoveInfo.EdgeMoveWay == EdgeMoveWay.horizontalMove && !jumper.IsJumping || path.MoveInfo.EdgeMoveWay == EdgeMoveWay.fall)
        {
            mover.Move(path.Current.Position);
        }

        if (path.MoveInfo.EdgeMoveWay == EdgeMoveWay.jump && !jumper.IsJumping)
        {
            transform.position = (path.Previous?.Position ?? path.Current.Position) - (Vector3)collider.offset/2;
            jumper.Jump(path.MoveInfo.MaxVelocity);
        }

        path.Update(collider);
    }

    private void UpdatePath()
    {
        if (!path.arrived)
        {
            path = pathfinder.GetPath(collider.bounds.center, target);
        }
    }
}