using System.Collections.Generic;
using UnityEngine;

public interface IHorizontalSurface
{
    List<INode> Nodes { get; }

    public float Distance(Vector3 position);
    public void Insert(INode node);
}