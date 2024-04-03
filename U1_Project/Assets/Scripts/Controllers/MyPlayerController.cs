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
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    protected override void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Dead:
                break;
            case CreatureState.Moving:
                break;
            case CreatureState.Idle:
                break;
        }

        base.UpdateController();
    }

    protected override void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.01f)
        {
            State = CreatureState.Idle;
            return;
        }
        else
        {
            // 플레이어 이동속도 하드코딩
            float moveDist = Mathf.Clamp(100.0f * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            WorldPos = transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            WorldRotation = transform.rotation;
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


    void OnMouseEvent(Define.MouseEvent evt)
    {
        _moveKeyPressed = true;

        switch (State)
        {
            case CreatureState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case CreatureState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _layerMask);

        switch (evt)
        {
            case Define.MouseEvent.Click:
                if (raycastHit)
                {
                    _destPos = hit.point;
                    State = CreatureState.Moving;
                }
                break;
        }
        CheckUpdatedFlag();
    }
    
    protected override void MoveToNextPos()
    {
        if (_moveKeyPressed == false)
        {
            State = CreatureState.Idle;
            CheckUpdatedFlag();
            return;
        }

        CheckUpdatedFlag();
    }

    protected override void CheckUpdatedFlag()
    {
        if (_updated)
        {
            C_WorldMove movePacket = new C_WorldMove();
            movePacket.WorldPosInfo = WorldPosInfo;
            Managers.Network.Send(movePacket);
            //Debug.Log("Test");
            _updated = false;
        }
    }
}
