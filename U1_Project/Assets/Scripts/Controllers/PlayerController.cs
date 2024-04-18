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
        dir.y = 0;

        if (dir.sqrMagnitude < 0.01f)
        {
            State = CreatureState.Idle;
            return;
        }

        float packetLatency = Managers.Network.RoundTripLatency;
        Vector3 predictedPosition = transform.position + dir.normalized * packetLatency * 100.0f;

        float maxMoveDistance = Mathf.Clamp(100.0f * Time.deltaTime, 0, dir.magnitude);
        transform.position = Vector3.MoveTowards(transform.position, predictedPosition, maxMoveDistance);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
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
