public interface IMovable : ICanMove,ICanJump,IHasSpeed
{

}

public interface ICanMove
{
    public void Move(float direction);
}
public interface ICanJump
{
    public void Jump();
}
public interface IHasSpeed
{
    float Speed { get;}
}