using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask _unwalkableMask;
    public Vector2 _gridWorldSize;
    public float _nodeRadius;
    Node[,] _grid; // 2���� �迭

    // ����
    float _nodeDiameter;
    int _gridSizeX, _gridSizeY;

    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter); // X ĭ�� 
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter); // Y ĭ��
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
                // ���ϴ� �������� �߾��� �������� ������ŭ �����̴� ����
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, _nodeRadius, _unwalkableMask));
                _grid[x, y] = new Node(walkable, worldPoint); // �׸��� ���� ���
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
