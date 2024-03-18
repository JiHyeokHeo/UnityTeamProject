using Google.Protobuf.Protocol;
using Server.Data;
using Server.Game.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
    public class GameObject
    {
        public GameObjectType ObjectType { get; protected set; } = GameObjectType.None;
        public int Id
        {
            get { return Info.ObjectId; }
            set { Info.ObjectId = value; }
        }

        public GameRoom Room { get; set; }

        public ObjectInfo Info { get; set; } = new ObjectInfo();
        public PositionInfo PosInfo { get; private set; } = new PositionInfo();
        public StatInfo Stat { get; private set; } = new StatInfo();

        public float Speed
        {
            get { return Stat.Speed; }
            set { Stat.Speed = value; }
        }

        public GameObject()
        {
            Info.PosInfo = PosInfo;
            Info.StatInfo = Stat;
        }

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
                PosInfo.PosZ = value.z;
            }
        }

        public Vector3Int GetFrontCellPos()
        {
            return GetFrontCellPos(PosInfo.MoveDir);
        }

        public Vector3Int GetFrontCellPos(MoveDir dir)
        {
            Vector3Int cellPos = CellPos;

            switch (dir)
            {
                case MoveDir.Up:
                    cellPos += Vector3Int.up;
                    break;
                case MoveDir.Down:
                    cellPos += Vector3Int.down;
                    break;
                case MoveDir.Left:
                    cellPos += Vector3Int.left;
                    break;
                case MoveDir.Right:
                    cellPos += Vector3Int.right;
                    break;
            }

            return cellPos;
        }

        public virtual void OnDamaged(GameObject attacker,int damage)
        {
            Stat.Hp = Math.Max(Stat.Hp - damage, 0);

            S_ChangeHp changePacket = new S_ChangeHp();
            changePacket.ObjectId = Id;
            changePacket.Hp = Stat.Hp;
            Room.Broadcast(changePacket);

            if (Stat.Hp <= 0)
            {
                OnDead(attacker);
            }
        }
        public virtual void OnDead(GameObject attacker)
        {

        }
    }
}
