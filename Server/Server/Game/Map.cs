using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Server.Game
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

        public int FCost { get { return _gCost + _hCost; } }

    }

    public struct Vector3Int
    {
        public int x;
        public int y;
        public int z;

        public Vector3Int(int x, int y, int z) { this.x = x; this.y = y; this.z = z; }

        public static Vector3Int zero { get { return new Vector3Int(0, 0, 0); } }
        public static Vector3Int up { get {return new Vector3Int(0, 0, 1); } }
        public static Vector3Int down { get {return new Vector3Int(0, 0, -1); } }
        public static Vector3Int left{ get {return new Vector3Int(-1, 0, 0); } }
        public static Vector3Int right { get {return new Vector3Int(1, 0, 0); } }

        public static Vector3Int operator+(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x + b.x, a.y + b.y , a.z + b.z);
        }

        public static Vector3Int operator-(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3Int operator*(Vector3Int a, float b)
        {
            return new Vector3Int((int)(a.x * b),(int)(a.y * b), (int)(a.z * b));
        }

        public static Vector3Int operator*(Vector3Int a, int b)
        {
            return new Vector3Int((a.x * b), (a.y * b), (a.z * b));
        }

        public static Vector3Int operator/(Vector3Int a, float b)
        {
            return new Vector3Int((int)(a.x / b), (int)(a.y / b), (int)(a.z / b));
        }

        public static Vector3Int operator/(Vector3Int a, int b)
        {
            return new Vector3Int((a.x / b), (a.y / b), (a.z / b));
        }
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
        Player[,] _players;

        public bool CanGo(Vector3Int cellPos)
        {
            if (cellPos.x < 0 || cellPos.x >= _gridSizeX)
                return false;
            if (cellPos.z < 0 || cellPos.z >= _gridSizeY)
                return false;

           
            return !_collision[cellPos.x, cellPos.y];
        }

        public Player Find(Vector3Int cellPos)
        {
            if (cellPos.x < 0 || cellPos.x > _gridSizeX)
                return null;
            if (cellPos.z < 0 || cellPos.z > _gridSizeY)
                return null;

            return _players[cellPos.x, cellPos.y];
        }

        public bool ApplyMove(Player player, Vector3Int dest)
        {
            PositionInfo posInfo = player.Info.PosInfo;
            if (posInfo.PosX < 0 || posInfo.PosX > _gridSizeX)
                return false;
            if (posInfo.PosZ < 0 || posInfo.PosZ > _gridSizeY)
                return false;
            if (CanGo(dest) == false)
                return false;
            
            {
                if (_players[posInfo.PosX, posInfo.PosY] == player)
                    _players[posInfo.PosX, posInfo.PosY] = null;
            }
            {
                _players[dest.x, dest.y] = player;
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
            _players = new Player[_gridSizeX, _gridSizeY];

            // 임시 땜방
            _gridWorldSizeX = 100.0f;
            _gridWorldSizeY = 90.0f;
            _nodeRadius = 5.0f;
            _nodeDiameter = 10.0f;
            // 

            for (int y = 0; y < _gridSizeY; y++)
            {
                string line = reader.ReadLine();
                for (int x = 0; x< _gridSizeX; x++)
                {
                    _collision[x, y] = (line[x] == '1' ? true : false);
                }
            }
        }

        public void Load()
        {
            _nodeDiameter = _nodeRadius * 2;
            _gridSizeX = (int)Math.Round(_gridWorldSizeX / _nodeDiameter, 0); // X 칸수 
            _gridSizeY = (int)Math.Round(_gridWorldSizeY / _nodeDiameter, 0); // Y 칸수
            CreateGrid();
        }

        void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            Vector3Int worldBottomLeft = Vector3Int.zero - Vector3Int.right * _gridWorldSizeX / 2 - Vector3Int.up * _gridWorldSizeY / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    // 좌하단 끝점에서 중앙을 기준으로 지름만큼 움직이는 원리
                    Vector3Int worldPoint = worldBottomLeft + Vector3Int.right * (x * _nodeDiameter + _nodeRadius) + Vector3Int.up * (y * _nodeDiameter + _nodeRadius);

                    // TODO 임시로 walkable true;
                    //bool walkable = !(Physics.CheckSphere(worldPoint, _nodeRadius, _unwalkableMask));
                    bool walkable = true;
                    _grid[x, y] = new Node(walkable, worldPoint, x, y); // 그리드 정보 담기
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            // 상하좌우대각선 검색 3x3 8칸 검색
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // 자기 자신은 제외
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node._gridX + x;
                    int checkY = node._gridY + y;

                    if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                        neighbours.Add(_grid[checkX, checkY]);

                }
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

        #region Astar 길찾기
        void FindPath(Vector3Int startPos, Vector3Int targetPos)
        {
            Node startNode = NodeFromWorldPoint(startPos);
            Node targetNode = NodeFromWorldPoint(targetPos);

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

                List<Node> nodeNeigbours = GetNeighbours(currentNode);
                foreach (Node neighbour in nodeNeigbours)
                {
                    if (!neighbour._walkable || closedSet.Contains(neighbour))
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

            _path = path;
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int disX = Math.Abs(nodeA._gridX - nodeB._gridX);
            int disY = Math.Abs(nodeA._gridY - nodeB._gridY);

            if (disX > disY)
                return 14 * disY + 10 * (disX - disY);
            return 14 * disX + 10 * (disY - disX);
        }
        #endregion
    }
}
