using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pseudo code (�ǻ��ڵ�)
// �ϴ� �츮�� �� �޾ƾ��� ������ ���ȴٰ� ǥ���Ѵ�.
// �׸��� �츮�� �̹� �򰡵� ������ �����ٰ� ǥ���� ���̴�.
// �츮�� ���� ��带 �����ŷ� ǥ���Ѵ�.

// �ݺ��� ����
// �����ִ� ��� �� ���� ���� f_cost�� Node�� �ֽ� ���� ����
// �����ִ� ���(�ڷᱸ���� ��Ƶ�)�κ��� �ֽ� ��带 ����
// �̰��� closed�� ����ش�.
// �ֺ� ������ ��ȸ�� �Ұ��� �ϰų� �ֺ� ��尡 closed�� �Ǹ� ���� �ֺ� ����� �̵��Ѵ�.


// Has - A ����
public class TestPathFind : MonoBehaviour
{
    public Transform _seeker, _target;

    GridMap grid;

    private void Awake()
    {
        grid = GetComponent<GridMap>();
    }

    private void Update()
    {
        FindPath(_seeker.position, _target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        
        // �ߺ��� �����ִ� �÷���
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i]._hCost < currentNode._hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // �����ϸ� ����
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour._walkable || closedSet.Contains(neighbour))
                    continue;

                // ���� ��忡�� �ڱ� �ֺ� ������ �Ÿ�
                int newMovementCostToNeighbour = currentNode._gCost + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour._gCost || !openSet.Contains(neighbour))
                {
                    neighbour._gCost = newMovementCostToNeighbour;
                    neighbour._hCost = GetDistance(neighbour, targetNode);
                    neighbour._parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode._parent;
        }
        path.Reverse();

        grid._path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int disX = Mathf.Abs(nodeA._gridX - nodeB._gridX);
        int disY = Mathf.Abs(nodeA._gridY - nodeB._gridY);

        if (disX > disY)
            return 14 * disY + 10 * (disX - disY);
        return 14 * disX + 10 * (disY - disX);
    }
}
