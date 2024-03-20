using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool _walkable;
    public Vector3 _worldPosition;
    public int _gridX;
    public int _gridY;

    public int _gCost; // 시작지점
    public int _hCost; // 도착지점
    public Node _parent;

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
        _gridX = gridX;
        _gridY = gridY;
    }

    public Node(int gridX, int gridY)
    {
        _gridX = gridX;
        _gridY = gridY;
    }

    public int FCost { get { return _gCost + _hCost; } }

}

public class MapManager
{
    public LayerMask _unwalkableMask;
    public float _gridWorldSizeX = 100.0f;
    public float _gridWorldSizeY = 90.0f;
    public float _nodeRadius = 5.0f;
    public Node[,] _grid; // 2차원 배열

    // 지름
    public float _nodeDiameter;
    public int _gridSizeX, _gridSizeY;
    Vector3 worldBottomLeft;

    public void Load()
    {
         Managers.Resource.Instantiate("Map/Astar");
        
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSizeX / _nodeDiameter); // X 칸수 
        _gridSizeY = Mathf.RoundToInt(_gridWorldSizeY / _nodeDiameter); // Y 칸수
        CreateGrid();
    }

    void CreateGrid()
    {
            _grid = new Node[_gridSizeX, _gridSizeY];
            worldBottomLeft = Vector3.zero - Vector3.right * _gridWorldSizeX / 2 - Vector3.forward * _gridWorldSizeY / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    // 좌하단 끝점에서 중앙을 기준으로 지름만큼 움직이는 원리
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                    //bool walkable = !(Physics.CheckSphere(worldPoint, _nodeRadius, _unwalkableMask));
                    _grid[x, y] = new Node(x, y); // 그리드 정보 담기
                }
            }
    }

    //public bool CanGo(Vector3Int cellPos)
    //{
    //    if (cellPos.x < 0 || cellPos.x >= _gridSizeX)
    //        return false;
    //    if (cellPos.z < 0 || cellPos.z >= _gridSizeY)
    //        return false;

    //    return !_collision[cellPos.x, cellPos.y];
    //}
    public bool SearchRange(Node node, int range)
    {
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                // 자기 자신은 제외
                if (x == 0 && y == 0)
                    continue;


            }
        }

        return false;
    }

    int[] _dy = { 1, 0, -1, 0 };
    int[] _dx = { 0, -1, 0, 1 };
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        // 상하좌우 4칸으로 변경
        for (int i = 0; i < 4; i++)
        {
            int checkX = node._gridX + _dx[i];
            int checkY = node._gridY + _dy[i];

            if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                neighbours.Add(_grid[checkX, checkY]);
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // 0~1
        float percentX = (worldPosition.x + _gridWorldSizeX / 2) / _gridWorldSizeX;
        float percentY = (worldPosition.z + _gridWorldSizeY / 2) / _gridWorldSizeY;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        return _grid[x, y];
    }

    public Vector3 CellPosToWorldPoint(Vector3Int cellPos)
    {
        Vector3 ret;
        int x = (int)worldBottomLeft.x + (int)_nodeRadius +  Mathf.RoundToInt(_nodeDiameter) * cellPos.x;
        int y = cellPos.y;
        int z = (int)worldBottomLeft.z + (int)_nodeRadius +  Mathf.RoundToInt(_nodeDiameter) * cellPos.z;

        ret.x = x;
        ret.y = y;
        ret.z = z;

        return ret;
    }

    private Vector2 Vector2(int v1, int v2)
    {
        throw new NotImplementedException();
    }

    public List<Node> _path;
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));

        //if( _grid != null)
        //{
        //    Node playerNode = NodeFromWorldPoint(player.position);
        //    foreach (Node node in _grid)
        //    {
        //        Gizmos.color = (node._walkable) ? Color.white : Color.red;
        //        if (playerNode == node)
        //            Gizmos.color = Color.cyan;

        //        if (_path != null)
        //            if (_path.Contains(node))
        //                Gizmos.color = Color.black;

        //        Gizmos.DrawCube(node._worldPosition, Vector3.one * (_nodeDiameter) - new Vector3(0.0f, 9.0f, 0.0f));
        //    }
        //}
    }
}
