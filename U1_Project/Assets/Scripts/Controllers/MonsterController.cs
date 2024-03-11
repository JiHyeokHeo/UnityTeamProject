using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : BaseController
{
    protected Coroutine _coSkill;

    protected override void Init()
    {
        base.Init();
        GameObject obj = GameObject.Find("Astar");
        //GameObject obj2 = GameObject.Find("Seeker");
        //_gridMap.player = obj2.transform;
        WorldObjectType = Define.WorldObject.Monster;
    }

    void Start()
    {
        Init();
        Node node = Managers.Map.NodeFromWorldPoint(CellPos);
        transform.position = node._worldPosition;
    }

    protected override void UpdateController()
    {
        base.UpdateController();
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
        Debug.Log("Monster Controller ��ġ ����");
        State = State.Skill;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Monster Controller ��ġ ����");
        State = State.Idle;
        _coSkill = null;
        CheckUpdatedFlag();
    }

    protected override void UpdateIdle()
    {
        // �̵� ���·� ���� Ȯ��
        if (Dir != MoveDir.None)
        {
            State = State.Moving;
            return;
        }

    }
}
