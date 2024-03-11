using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public int Id { get; set; }
    
    public float _speed = 50.0f;

    protected bool _updated = false;

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            CellPos = new Vector3Int(value.PosX, value.PosY, value.PosZ);
            State = value.State;
            Dir = value.MoveDir;
        }
    }

    public void SyncPos()
    {
        Node node = Managers.Map.NodeFromWorldPoint(CellPos);
        Vector3 destPos = node._worldPosition;
        transform.position = destPos;
    }

    public Vector3Int CellPos
    {
        get
        {
            return new Vector3Int(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
        }

        set
        {
            if (PosInfo.PosX == value.x && PosInfo.PosY == value.y && PosInfo.PosZ == value.z)
                return;

            PosInfo.PosX = value.x;
            PosInfo.PosY = value.y;
            PosInfo.PosZ = value.z;
            _updated = true;
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
            _updated = true;
        }
    }

    protected GameObject _lockTarget;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    protected Animator _animator;
    protected State _state = State.Idle;
    public virtual State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            UpdateAnimation();
        }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateController();
    }

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        PosInfo.State = State.Idle;
        Dir = MoveDir.None;

        // 애니메이션 업데이트 추가 예정
        // UpdateAnimation();
    }

    protected virtual void UpdateAnimation()
    {
        if (State == State.Idle)
        {
            switch (_lastDir)
            {
                case MoveDir.Up:
                    //_animator.Play();
                    //_animator.CrossFade("", 0.1f);
                    break;
                case MoveDir.Down:
                    break;
                case MoveDir.Left:
                    break;
                case MoveDir.Right:
                    break;

            }
        }
        else if(State == State.Moving)
        {
            // TODO
            // 움직이는거 추가
        }
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

    public virtual void UseSkill(int skillId) { }

    protected virtual void UpdateMoving()
    {
        //if (_isMoving == false)
        //    return;

        Node node = Managers.Map.NodeFromWorldPoint(CellPos);
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
            State = State.Moving;
        }

    }

    protected virtual void MoveToNextPos()
    {
            

    }


}
