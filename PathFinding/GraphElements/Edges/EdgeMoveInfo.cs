using UnityEngine;

public class EdgeMoveInfo
{
    public static EdgeMoveInfo FallMoveInfo
    {
        get
        {
            return new EdgeMoveInfo(Vector3.positiveInfinity, EdgeMoveWay.fall);
        }
    }

    public static EdgeMoveInfo HorizontalMoveInfo
    {
        get
        {
            return new EdgeMoveInfo(Vector3.positiveInfinity, EdgeMoveWay.horizontalMove);
        }
    }
    
    public Vector3 MaxVelocity { get; set; }
    public EdgeMoveWay EdgeMoveWay { get; set; }
    
    public EdgeMoveInfo(Vector3 maxVelocity, EdgeMoveWay edgeMoveWay)
    {
        MaxVelocity = maxVelocity;
        EdgeMoveWay = edgeMoveWay;
    }
}

public enum EdgeMoveWay
{
    horizontalMove,
    jump,
    fall,
}