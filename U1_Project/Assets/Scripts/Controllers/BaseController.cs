using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public int Id { get; set; }

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

    protected GameObject _lockTarget;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    protected Define.State _state = Define.State.Idle;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch(_state)
            {
                case Define.State.Die:
                    //anim.CrossFade("", 0.1f);
                    break;
                case Define.State.Idle:
                    break;
                case Define.State.Moving:
                    break;
                case Define.State.Skill:
                    break;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch (_state)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
        }
    }

    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }

    public abstract void Init();
}
