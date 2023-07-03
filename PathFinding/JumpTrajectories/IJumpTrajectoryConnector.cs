using UnityEngine;

public interface IJumpTrajectoriesConnector
{
    public void GenerateJumpEdges();
    void GenerateJumpEdges(INode node);
    void GenerateJumpEdges(INode node,Vector2 accuracy);
}