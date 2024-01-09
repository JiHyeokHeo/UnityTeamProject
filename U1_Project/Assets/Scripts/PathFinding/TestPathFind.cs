using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pseudo code (의사코드)
// 일단 우리는 평가 받아야할 노드들을 열렸다고 표현한다.
// 그리고 우리는 이미 평가된 노드들은 닫혔다고 표현할 것이다.
// 우리는 시작 노드를 열린거로 표기한다.

// 반복문 시작
// 열려있는 노드 중 가장 작은 f_cost의 Node를 최신 노드로 갱신
// 열려있는 노드(자료구조에 담아둔)로부터 최신 노드를 제거
// 이것을 closed에 담아준다.
// 주변 노드들이 순회가 불가능 하거나 주변 노드가 closed가 되면 다음 주변 노드들로 이동한다.


// Has - A 관계
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
        
        // 중복을 막아주는 컬렉션
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

            // 도착하면 리턴
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour._walkable || closedSet.Contains(neighbour))
                    continue;

                // 현재 노드에서 자기 주변 노드로의 거리
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
