using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    protected Coroutine _coSkill;
    protected Vector3 _prevPos;
    Quaternion _prevRotation;
    protected override void Init()
    {
        base.Init();
    }

    void Start()
    {
        Init();
    }

    protected override void UpdateController()
    {
        base.UpdateController();
    }

    protected override void UpdateMoving()
    {
        Vector3 dir = WorldPos - transform.position;
        if (dir.magnitude < 0.01f)
        {
            State = CreatureState.Idle;
            return;
        }

        dir.y = 0;

        float latency = Managers.Network.RoundTripLatency;
        Vector3 predictedPosition = transform.position + dir.normalized * latency;

        transform.position = predictedPosition;
  
        _prevPos = WorldPos;
        _prevRotation = WorldRotation;
    }
    public override void UseSkill(int skillId)
    {
        if (skillId == 1)
        {
            _coSkill = StartCoroutine("CoStartPunch");
        }
    }

    protected virtual void CheckUpdatedFlag()
    {

    }

    IEnumerator CoStartPunch()
    {
        Debug.Log("Monster Controller 펀치 시작");
        State = CreatureState.Skill;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Monster Controller 펀치 종료");
        State = CreatureState.Idle;
        _coSkill = null;
        CheckUpdatedFlag();
    }

    protected override void UpdateIdle()
    {
        //// 이동 상태로 갈지 확인
        //if (Dir != MoveDir.None)
        //{
        //    State = State.Moving;
        //    return;
        //}

    }
}
