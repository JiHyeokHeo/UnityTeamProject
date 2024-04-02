using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MyPlayerController : PlayerController
{
    int _layerMask = 1 << (int)Define.Layer.Ground;
    bool _moveKeyPressed = false;
    Vector3 _destPos;

    protected override void Init()
    {
        base.Init();
        Managers.Input.MouseAction -= GetDirOrder;
        Managers.Input.MouseAction += GetDirOrder;
    }

    protected override void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Dead:
                break;
            case CreatureState.Moving:
                GetDirOrder();
                break;
            case CreatureState.Idle:
                GetDirOrder();
                break;
        }

        base.UpdateController();
    }

    protected override void UpdateMoving()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _layerMask);

        if (raycastHit)
        {
            _destPos = hit.point;
            State = CreatureState.Moving;
        }
        else
        {

        }

    }

    protected override void UpdateIdle()
    {
        // 이동 상태로 갈지 확인
        if (_moveKeyPressed)
        {
            State = CreatureState.Moving;
            return;
        }

        if (_coSkillColltime == null && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Skill! Space 클릭");

            C_Skill skill = new C_Skill() { Info = new SkillInfo() };
            skill.Info.SkillId = 1;
            Managers.Network.Send(skill);

            _coSkillColltime = StartCoroutine("ColInputCooltime", 0.2f);
        }
    }

    Coroutine _coSkillColltime;
    IEnumerator ColInputCooltime(float time)
    {
        yield return new WaitForSeconds(time);
        _coSkillColltime = null;
    }


    void GetDirOrder(Define.MouseEvent evt)
    {
        _moveKeyPressed = true;

        switch (evt)
        {

        }

        if (Input.GetMouseButton(0))
        {
        }
        else
        {
            _moveKeyPressed = false;
        }
            //// 싸울 타겟을 지정해야함
            //if (Input.GetKey(KeyCode.W))
            //{
            //    Dir = MoveDir.Up;
            //}
            //else if (Input.GetKey(KeyCode.A))
            //{
            //    Dir = MoveDir.Left;
            //}
            //else if (Input.GetKey(KeyCode.S))
            //{
            //    Dir = MoveDir.Down;
            //}
            //else if (Input.GetKey(KeyCode.D))
            //{
            //    Dir = MoveDir.Right;
            //}
            //else
            //{
            //    _moveKeyPressed = false;
            //}
        }

    protected override void MoveToNextPos()
    {
        if (_moveKeyPressed == false)
        {
            State = CreatureState.Idle;
            CheckUpdatedFlag();
            return;
        }

        Vector3Int destPos = CellPos;

        //switch (Dir)
        //{
        //    case MoveDir.Up:
        //        destPos += new Vector3Int(0, 0, 1);
        //        break;
        //    case MoveDir.Left:
        //        destPos += Vector3Int.left;
        //        break;
        //    case MoveDir.Right:
        //        destPos += Vector3Int.right;
        //        break;
        //    case MoveDir.Down:
        //        destPos += new Vector3Int(0, 0, -1);
        //        break;
        //}

        if (Managers.Object.FindCreature(destPos) == null)
        {
            CellPos = destPos;
        }

        CheckUpdatedFlag();

    }

    protected override void CheckUpdatedFlag()
    {
        if (_updated)
        {
            C_Move movePacket = new C_Move();
            movePacket.PosInfo = PosInfo;
            Managers.Network.Send(movePacket);
            //Debug.Log("Test");
            _updated = false;
        }
    }
}
