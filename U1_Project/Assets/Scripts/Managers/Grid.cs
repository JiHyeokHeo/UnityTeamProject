using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask _unwalkableMask;
    public Vector2 _gridWorldSize;
    public float _nodeRadius;
    Node[,] _grid; // 2차원 배열

    // 지름
    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;

    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter); // X 칸수 
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter); // Y 칸수
        CreateGrid();
    }

    void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * _gridWorldSize.x/2 - Vector3.forward * _gridWorldSize.y/2;

        for(int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                // 좌하단 끝점에서 중앙을 기준으로 지름만큼 움직이는 원리
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, _nodeRadius, _unwalkableMask));
                _grid[x, y] = new Node(walkable, worldPoint); // 그리드 정보 담기
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // 0~1
        float percentX = (worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
        float percentY = (worldPosition.z + _gridWorldSize.y / 2) / _gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        return _grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));

        if( _grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node node in _grid)
            {
                Gizmos.color = (node._walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node._worldPosition, Vector3.one * (_nodeDiameter - 0.01f));
            }
        }
    }

}
