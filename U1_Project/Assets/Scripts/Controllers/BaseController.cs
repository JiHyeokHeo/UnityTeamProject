using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public int Id { get; set; }
    
    StatInfo _stat = new StatInfo();
    public virtual StatInfo Stat
    {
        get { return _stat; }
        set 
        {
            if (_stat.Equals(value))
                return;

            _stat.Hp = value.Hp;
            _stat.MaxHp = value.MaxHp;
            _stat.Speed = value.Speed;
        }
    }

    public virtual int Hp
    {
        get { return Stat.Hp; }
        set
        {
            Stat.Hp = value;
        }
    }
    public float Speed
    {
        get { return Stat.Speed;}
        set { Stat.Speed = value;}
    }

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
        Vector3 destPos = Managers.Map.CellPosToWorldPoint(CellPos);
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

    public MoveDir Dir
    {
        get { return PosInfo.MoveDir; }
        set
        {
            if (PosInfo.MoveDir == value)
                return;

            PosInfo.MoveDir = value;

            // TODO 
            // UpdateAnimation();
            _updated = true;
        }
    }

    protected GameObject _lockTarget;
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    protected Animator _animator;
    public virtual CreatureState State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;

            PosInfo.State = value;
            Animator anim = GetComponent<Animator>();
            UpdateAnimation();
            _updated = true;
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
        PosInfo.State = CreatureState.Idle;
        Dir = MoveDir.Down;
        // 애니메이션 업데이트 추가 예정
        UpdateAnimation();
    }

    protected virtual void UpdateAnimation()
    {
        if (State == CreatureState.Idle)
        {
            switch (Dir)
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
        else if(State == CreatureState.Moving)
        {
            // TODO
            // 움직이는거 추가
        }
    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Dead:
                UpdateDie();
                break;
            case CreatureState.Moving:
                UpdateMoving();
                break;
            case CreatureState.Idle:
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

        Vector3 destPos = Managers.Map.CellPosToWorldPoint(CellPos) + new Vector3(0.0f, 4.0f, -5.0f);
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < Speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();
        }
        else
        {
            transform.position += moveDir.normalized * Speed * Time.deltaTime;
            State = CreatureState.Moving;
        }

    }

    public MoveDir GetDirFromVec(Vector3Int dir)
    {
        if (dir.x > 0)
            return MoveDir.Right;
        else if (dir.x < 0)
            return MoveDir.Left;
        else if (dir.z > 0)
            return MoveDir.Up;
        else
            return MoveDir.Down;
    }


    protected virtual void MoveToNextPos()
    {
            

    }

}
