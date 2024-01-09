using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool _walkable;
    public Vector3 _worldPosition;
    public int _gridX;
    public int _gridY;

    public int _gCost; // ��������
    public int _hCost; // ��������
    public Node _parent;

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
        _gridX = gridX;
        _gridY = gridY;

    }

    public int FCost { get { return _gCost + _hCost; } }

}
