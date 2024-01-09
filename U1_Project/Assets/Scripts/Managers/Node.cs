using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool _walkable;
    public Vector3 _worldPosition;

    public Node(bool walkable, Vector3 worldPos)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
    }

}
