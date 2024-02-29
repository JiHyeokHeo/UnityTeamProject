using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : BaseController
{
    public GridMap _gridMap;
    public float _speed = 5.0f;

    Vector3Int _cellPos = Vector3Int.zero;
    MoveDir _dir = MoveDir.None;
    bool _isMoving = false;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        Managers.Input.KeyAction -= GetDirOrder;
        Managers.Input.KeyAction += GetDirOrder;
    }

    void Start()
    {
        Init();
        Node node = _gridMap.NodeFromWorldPoint(_cellPos);
        transform.position = node._worldPosition;
    }

    void Update()
    {

        UpdatePosition();
        UpdateMovingCheck();
    
    }

    void GetDirOrder()
    {
        // 싸울 타겟을 지정해야함
        if (Input.GetKey(KeyCode.W))
        {
            _dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _dir = MoveDir.Right;
        }
        else
        {
            _dir = MoveDir.None;
        }
    }

    void UpdatePosition()
    {
        if (_isMoving == false)
            return;

        Node node = _gridMap.NodeFromWorldPoint(_cellPos);
        Vector3 destPos = node._worldPosition;
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _dir = MoveDir.None;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            _isMoving = true;
        }

    }

    void UpdateMovingCheck()
    {
        if (_isMoving == false)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _cellPos += new Vector3Int(0, 0, 1);
                    _isMoving = true;
                    break;
                case MoveDir.Left:
                    _cellPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case MoveDir.Right:
                    _cellPos += Vector3Int.right;
                    _isMoving = true;
                    break;
                case MoveDir.Down:
                    _cellPos += new Vector3Int(0, 0, -1);
                    _isMoving = true;
                    break;
            }
        }
    }
}
