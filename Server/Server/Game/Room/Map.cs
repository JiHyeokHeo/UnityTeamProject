using Google.Protobuf.Protocol;
using Server.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Server.Game.Room
{
    public class Node
    {
        public bool _walkable;
        public Vector3Int _worldPosition;
        public int _gridX;
        public int _gridY;

        public int _gCost; // 시작지점
        public int _hCost; // 도착지점
        public Node _parent;

        public Node(bool walkable, Vector3Int worldPos, int gridX, int gridY)
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

    public struct Vector3Int
    {
        public int x;
        public int y;
        public int z;

        public Vector3Int(int x, int y, int z) { this.x = x; this.y = y; this.z = z; }

        public static Vector3Int zero { get { return new Vector3Int(0, 0, 0); } }
        public static Vector3Int up { get { return new Vector3Int(0, 0, 1); } }
        public static Vector3Int down { get { return new Vector3Int(0, 0, -1); } }
        public static Vector3Int left { get { return new Vector3Int(-1, 0, 0); } }
        public static Vector3Int right { get { return new Vector3Int(1, 0, 0); } }

        public static Vector3Int operator +(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3Int operator -(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3Int operator *(Vector3Int a, float b)
        {
            return new Vector3Int((int)(a.x * b), (int)(a.y * b), (int)(a.z * b));
        }

        public static Vector3Int operator *(Vector3Int a, int b)
        {
            return new Vector3Int(a.x * b, a.y * b, a.z * b);
        }

        public static Vector3Int operator /(Vector3Int a, float b)
        {
            return new Vector3Int((int)(a.x / b), (int)(a.y / b), (int)(a.z / b));
        }

        public static Vector3Int operator /(Vector3Int a, int b)
        {
            return new Vector3Int(a.x / b, a.y / b, a.z / b);
        }

        public float magnitude { get { return (float)Math.Sqrt(sqrMagnitude); } }  
        public int sqrMagnitude { get { return (x * x + z * z); } }
        public int cellDistFromZero { get { return Math.Abs(x) + Math.Abs(z); } }
    }


    public class Map
    {
        public float _gridWorldSizeX;
        public float _gridWorldSizeY;
        public float _nodeRadius;
        public Node[,] _grid; // 2차원 배열
        public List<Node> _path;
        // 지름
        public float _nodeDiameter;
        public int _gridSizeX, _gridSizeY;

        public bool[,] _collision;
        GameObject[,] _objects;

        public bool CanGo(Vector3Int cellPos, bool checkObjects = true)
        {
            if (cellPos.x < 0 || cellPos.x >= _gridSizeX)
                return false;
            if (cellPos.z < 0 || cellPos.z >= _gridSizeY)
                return false;

            return !_collision[cellPos.x, cellPos.z] || (!checkObjects || _objects[cellPos.x, cellPos.z] == null);
        }

        public GameObject Find(Vector3Int cellPos)
        {
            if (cellPos.x < 0 || cellPos.x > _gridSizeX)
                return null;
            if (cellPos.z < 0 || cellPos.z > _gridSizeY)
                return null;

            return _objects[cellPos.x, cellPos.y];
        }

        public bool ApplyLeave(GameObject gameObject)
        {
            PositionInfo posInfo = gameObject.PosInfo;
            if (posInfo.PosX < 0 || posInfo.PosX > _gridSizeX)
                return false;
            if (posInfo.PosZ < 0 || posInfo.PosZ > _gridSizeY)
                return false;

            {
                if (_objects[posInfo.PosX, posInfo.PosY] == gameObject)
                    _objects[posInfo.PosX, posInfo.PosY] = null;
            }

            return true;
        }

        public bool ApplyMove(GameObject gameObject, Vector3Int dest)
        {
            ApplyLeave(gameObject);

            PositionInfo posInfo = gameObject.PosInfo;
            if (CanGo(dest, true) == false)
                return false;

            {
                _objects[dest.x, dest.y] = gameObject;
            }

            // 실제 좌표이동
            posInfo.PosX = dest.x;
            posInfo.PosY = dest.y;
            posInfo.PosZ = dest.z;

            return true;
        }

        public void LoadMap(int mapId, string pathPrefix = "../../../../../Common/MapData")
        {
            // TEMP 이름
            string mapName = "Astar";

            string text = File.ReadAllText($"{pathPrefix}/{mapName}.txt");
            StringReader reader = new StringReader(text);

            _gridSizeX = int.Parse(reader.ReadLine());
            _gridSizeY = int.Parse(reader.ReadLine());
            _collision = new bool[_gridSizeX, _gridSizeY];
            _objects = new GameObject[_gridSizeX, _gridSizeY];
            // 임시 땜방
            _gridWorldSizeX = 100.0f;
            _gridWorldSizeY = 90.0f;
            _nodeRadius = 5.0f;
            _nodeDiameter = 10.0f;
            // 

            for (int y = 0; y < _gridSizeY; y++)
            {
                string line = reader.ReadLine();
                for (int x = 0; x < _gridSizeX; x++)
                {
                    _collision[x, y] = line[x] == '1' ? true : false;
                }
            }

            _grid = new Node[_gridSizeX, _gridSizeY];
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _grid[x, y] = new Node(x, y); // 그리드 정보 담기
                }
            }
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

        public Node NodeFromWorldPoint(Vector3Int worldPosition)
        {
            // 0~1
            float percentX = (worldPosition.x + _gridWorldSizeX / 2) / _gridWorldSizeX;
            float percentY = (worldPosition.z + _gridWorldSizeY / 2) / _gridWorldSizeY;
            percentX = Math.Clamp(percentX, 0, 1);
            percentY = Math.Clamp(percentY, 0, 1);

            int x = (int)Math.Round((_gridSizeX - 1) * percentX);
            int y = (int)Math.Round((_gridSizeY - 1) * percentY);

            return _grid[x, y];
        }

        public Node NodeFromCellPos(Vector3Int cellPos)
        {
            return _grid[cellPos.x, cellPos.z];
        }

        #region Astar 길찾기
        public List<Node> FindPath(Vector3Int startPos, Vector3Int targetPos, bool checkObjects = true)
        {
            Node startNode = NodeFromCellPos(startPos);
            Node targetNode = NodeFromCellPos(targetPos);

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
                    return RetracePath(startNode, targetNode);
                }

                List<Node> nodeNeigbours = GetNeighbours(currentNode);
                foreach (Node neighbour in nodeNeigbours)
                {
                    Vector3Int nextPos = new Vector3Int(neighbour._gridX, startPos.y, neighbour._gridY);

                    if (CanGo(nextPos, checkObjects) == false)
                        continue;

                    if (closedSet.Contains(neighbour))
                        continue;

                    // 현재 노드에서 자기 주변 노드로의 거리
                    int newMovementCostToNeighbour = currentNode._gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour._gCost || !openSet.Contains(neighbour))
                    {
                        neighbour._gCost = newMovementCostToNeighbour;
                        neighbour._hCost = GetDistance(neighbour, targetNode);
                        neighbour._parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            return RetracePath(startNode, targetNode);
        }

        List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode._parent;
            }
            path.Add(startNode);
            path.Reverse();

            _path = path;
            return path;
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int disX = Math.Abs(nodeA._gridX - nodeB._gridX);
            int disY = Math.Abs(nodeA._gridY - nodeB._gridY);

            if (disX > disY) 
                return 10 * disY + 10 * (disX - disY);
            return 10 * disX + 10 * (disY - disX);
        }
        #endregion
    }
}
