using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MonsterController : CreatureController
{
    protected Coroutine _coSkill;

    protected override void Init()
    {
        base.Init();
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
