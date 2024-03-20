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

        State = CreatureState.Idle;
        Dir = MoveDir.Down;
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }
    protected virtual void CheckUpdatedFlag()
    {

    }

    IEnumerator CoStartPunch()
    {
        Debug.Log("Monster Controller ÆÝÄ¡ ½ÃÀÛ");
        State = CreatureState.Skill;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Monster Controller ÆÝÄ¡ Á¾·á");
        State = CreatureState.Idle;
        _coSkill = null;
        CheckUpdatedFlag();
    }
    
}
