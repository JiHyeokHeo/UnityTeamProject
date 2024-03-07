using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : BaseController
{
    protected override void Init()
    {
        base.Init();
        GameObject obj = GameObject.Find("Astar");
        //GameObject obj2 = GameObject.Find("Seeker");
        _gridMap = obj.GetComponent<GridMap>();
        //_gridMap.player = obj2.transform;
        WorldObjectType = Define.WorldObject.Monster;
    }

    void Start()
    {
        Init();
        Node node = _gridMap.NodeFromWorldPoint(CellPos);
        transform.position = node._worldPosition;
    }

    protected override void UpdateController()
    {
        base.UpdateController();
    }

    protected override void UpdateIdle()
    {
        // 이동 상태로 갈지 확인
        if (Dir != MoveDir.None)
        {
            State = State.Moving;
            return;
        }

    }
}
