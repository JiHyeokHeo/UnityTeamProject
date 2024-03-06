using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public int Id { get; set; }

    public GridMap _gridMap;
    public float _speed = 5.0f;

    public Vector3Int CellPos 
    {
        get 
        {
            return new Vector3Int(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
        } 

        set
        {
            PosInfo.PosX = value.x;
            PosInfo.PosY = value.y;
            PosInfo.PosZ = value.z;
        }
    }

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            _positionInfo = value;
        }
    }

    protected MoveDir _lastDir = MoveDir.Down;
    public MoveDir Dir
    {
        get { return PosInfo.MoveDir; }
        set
        {
            if (PosInfo.MoveDir == value)
                return;

            PosInfo.MoveDir = value;
            if (value != MoveDir.None)
                _lastDir = value;

            // TODO 
            // UpdateAnimation();
        }
    }

    protected GameObject _lockTarget;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    protected State _state = State.Idle;

    public virtual State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch(_state)
            {
                case State.Dead:
                    //anim.CrossFade("", 0.1f);
                    break;
                case State.Idle:
                    break;
                case State.Moving:
                    break;
                case State.Skill:
                    break;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateController();
    }

    protected virtual void UpdateController()
    {
        switch (_state)
        {
            case State.Dead:
                UpdateDie();
                break;
            case State.Moving:
                UpdateMoving();
                break;
            case State.Idle:
                UpdateIdle();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateDie() { }

    protected virtual void UpdateMoving()
    {
        //if (_isMoving == false)
        //    return;

        Node node = _gridMap.NodeFromWorldPoint(CellPos);
        Vector3 destPos = node._worldPosition;
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
        }

    }

    protected virtual void MoveToNextPos()
    {
        if (Dir == MoveDir.None)
        {
            State = State.Idle;
            return;
        }

        Vector3Int prevCellPos = CellPos;

        Vector3Int destPos = CellPos;

        switch (Dir)
        {
            case MoveDir.Up:
                destPos += new Vector3Int(0, 0, 1);
                break;
            case MoveDir.Left:
                destPos += Vector3Int.left;
                break;
            case MoveDir.Right:
                destPos += Vector3Int.right;
                break;
            case MoveDir.Down:
                destPos += new Vector3Int(0, 0, -1);
                break;
        }

        if (Managers.Object.Find(destPos) == null)
        {
            CellPos = destPos;
        }

    }

    protected virtual void Init()
    {

    }
}
