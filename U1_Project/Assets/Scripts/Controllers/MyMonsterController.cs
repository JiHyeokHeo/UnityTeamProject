using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class MyMonsterController : MonsterController
    {
        protected override void Init()
        {
            base.Init();
            Managers.Input.KeyAction -= GetDirOrder;
            Managers.Input.KeyAction += GetDirOrder;
        }

        protected override void UpdateController()
        {
            switch (_state)
            {
                case State.Dead:
                    break;
                case State.Moving:
                    GetDirOrder();
                    break;
                case State.Idle:
                    GetDirOrder();
                    break;
            }

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

        void GetDirOrder()
        {
            // 싸울 타겟을 지정해야함
            if (Input.GetKey(KeyCode.W))
            {
                Dir = MoveDir.Up;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Dir = MoveDir.Left;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Dir = MoveDir.Down;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Dir = MoveDir.Right;
            }
            else
            {
                Dir = MoveDir.None;
            }
        }

        protected override void MoveToNextPos()
        {
            State prevState = State;
            Vector3Int prevCellPos = CellPos;

            base.MoveToNextPos();

            if (prevState != State || CellPos != prevCellPos)
            {
                C_Move movePacket = new C_Move();
                movePacket.PosInfo = PosInfo;
                Managers.Network.Send(movePacket);
            }
        }
    }
}
