using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.Data;
using Server.Game.Room;
using System;
using System.Collections.Generic;

namespace Server.Game
{
    public class GameRoom : JobSerializer
    {
        public int RoomId { get; set; }

        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        Dictionary<int, Monster> _monsters = new Dictionary<int, Monster>();
        Dictionary<int, Projectile> _projectiles = new Dictionary<int, Projectile>();

        public Map Map { get; private set; } = new Map();

        public void Init(int mapId)
        {
            Map.LoadMap(mapId);

            //Monster monster = ObjectManager.Instance.Add<Monster>();
            //monster.CellPos = new Vector3Int(5, 0, 5);
            //EnterGame(monster);

            //TestTimer();
        }


        void TestTimer()
        {
            Console.WriteLine("Test Timer");
            PushAfter(100, TestTimer);
        }
        // MMO (50ms)
        public void Update()
        {
            foreach (Monster monster in _monsters.Values)
            {
                monster.Update();
            }

            foreach (Projectile projectile in _projectiles.Values)
            {
                projectile.Update();
            }

            Flush();
        }

        public void EnterGame(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            GameObjectType type = ObjectManager.GetObjectTypeById(gameObject.Id);


            if (type == GameObjectType.Player)
            {
                Player player = gameObject as Player;
                _players.Add(gameObject.Id, player);
                player.Room = this;

                Map.ApplyMove(player, new Vector3Int(player.CellPos.x, player.CellPos.y, player.CellPos.z));

                // 본인한테 정보 전송
                {
                    S_EnterGame enterPacket = new S_EnterGame();
                    enterPacket.Player = player.Info;
                    player.Session.Send(enterPacket);

                    S_Spawn spawnPacket = new S_Spawn();
                    foreach (Player p in _players.Values)
                    {
                        if (player != p)
                            spawnPacket.Objects.Add(p.Info);
                    }

                    foreach (Monster m in _monsters.Values)
                        spawnPacket.Objects.Add(m.Info);

                    foreach (Projectile p in _projectiles.Values)
                        spawnPacket.Objects.Add(p.Info);

                    player.Session.Send(spawnPacket);
                }
            }
            else if (type == GameObjectType.Monster)
            {
                Monster monster = gameObject as Monster;
                _monsters.Add(gameObject.Id, monster);
                monster.Room = this;

                Map.ApplyMove(monster, new Vector3Int(monster.CellPos.x, monster.CellPos.y, monster.CellPos.z));
            }
            else if (type == GameObjectType.Projectile)
            {
                Projectile projectile = gameObject as Projectile;
                _projectiles.Add(gameObject.Id, projectile);
                projectile.Room = this;

                Map.ApplyMove(projectile, new Vector3Int(projectile.CellPos.x, projectile.CellPos.y, projectile.CellPos.z));
            }

            // 타인한테 정보 전송
            {
                S_Spawn spawnPacket = new S_Spawn();
                spawnPacket.Objects.Add(gameObject.Info);
                foreach (Player p in _players.Values)
                {
                    if (p.Id != gameObject.Id)
                        p.Session.Send(spawnPacket);
                }
            }


        }

        public void LeaveGame(int objectId)
        {
            GameObjectType type = ObjectManager.GetObjectTypeById(objectId);


            if (type == GameObjectType.Player)
            {
                Player player = null;
                if (_players.Remove(objectId, out player) == false)
                    return;

                Map.ApplyLeave(player);
                player.Room = null;

                // 본인한테 정보 전송
                {
                    S_LeaveGame leavePacket = new S_LeaveGame();
                    player.Session.Send(leavePacket);
                }
            }
            else if (type == GameObjectType.Monster)
            {
                Monster monster = null;
                if (_monsters.Remove(objectId, out monster) == false)
                    return;

                Map.ApplyLeave(monster);
                monster.Room = null;
            }
            else if (type == GameObjectType.Projectile)
            {
                Projectile projectile = null;
                if (_projectiles.Remove(objectId, out projectile) == false)
                    return;

                projectile.Room = null;
            }

            // 타인에게 정보 전송
            {
                S_Despawn despawnPacket = new S_Despawn();
                despawnPacket.ObjectIds.Add(objectId);
                foreach (Player p in _players.Values)
                {
                    if (p.Id != objectId)
                        p.Session.Send(despawnPacket);
                }
            }

        }

        public void HandleMove(Player player, C_WorldMove movePacket)
        {
            if (player == null)
                return;


            // TODO : 검증
            WorldPosInfo movePosInfo = movePacket.WorldPosInfo;
            ObjectInfo info = player.Info;

            // 다른 좌표로 이동할 경우, 갈 수 있는지 체크
            if (movePosInfo.PosX != info.PosInfo.PosX || movePosInfo.PosZ != info.PosInfo.PosZ)
            {
                //if (Map.CanGo(new Vector3Int(movePosInfo.PosX, movePosInfo.PosY, movePosInfo.PosZ)) == false)
                //    return;
            }

            info.WorldPosInfo.State = movePosInfo.State;
            info.WorldPosInfo.PosX = movePosInfo.PosX;
            info.WorldPosInfo.PosY = movePosInfo.PosY;
            info.WorldPosInfo.PosZ = movePosInfo.PosZ;
            info.WorldPosInfo.RotX = movePosInfo.RotX;
            info.WorldPosInfo.RotY = movePosInfo.RotY;
            info.WorldPosInfo.RotZ = movePosInfo.RotZ;
            //Map.ApplyMove(player, new Vector3Int(movePosInfo.PosX, movePosInfo.PosY, movePosInfo.PosZ));

            // 다른 플레이어한테도 알려준다.
            S_WorldMove resMovePacket = new S_WorldMove();
            resMovePacket.ObjectId = player.Info.ObjectId;  
            resMovePacket.WorldPosInfo = movePacket.WorldPosInfo;

            Broadcast(resMovePacket);
        }

        public void HandleSkill(Player player, C_Skill skillPacket)
        {
            if (player == null)
                return;


            ObjectInfo info = player.Info;
            if (info.PosInfo.State != CreatureState.Idle)
                return;

            // TODO : 스킬 사용 가능 여부 체크
            info.PosInfo.State = CreatureState.Skill;
            S_Skill skill = new S_Skill() { Info = new SkillInfo() };
            skill.ObjectId = info.ObjectId;
            skill.Info.SkillId = skillPacket.Info.SkillId;
            Broadcast(skill);

            Data.Skill skillData = null;
            if (DataManager.SkillDict.TryGetValue(skillPacket.Info.SkillId, out skillData) == false)
                return;

            switch (skillData.skillType)
            {
                case SkillType.SkillAuto:
                    {
                        // TODO : 데미지 판정
                        Vector3Int skillPos = player.GetFrontCellPos(info.PosInfo.MoveDir);
                        GameObject target = Map.Find(skillPos);
                        if (target != null)
                        {
                            Console.WriteLine("Hit GameObject");
                        }
                    }
                    break;
                case SkillType.SkillProjectile:
                    {
                        // TODO : 다양한 스킬
                        Arrow arrow = ObjectManager.Instance.Add<Arrow>();
                        if (arrow == null)
                            return;

                        arrow.Owner = player;
                        arrow.Data = skillData;
                        arrow.PosInfo.State = CreatureState.Moving;
                        arrow.PosInfo.MoveDir = player.PosInfo.MoveDir;
                        arrow.PosInfo.PosX = player.PosInfo.PosX;
                        arrow.PosInfo.PosY = player.PosInfo.PosY;
                        arrow.Speed = skillData.projectile.speed;
                        Push(EnterGame, arrow);
                    }
                    break;
            }
        }

        public Player FindPlayer(Func<GameObject, bool> condition)
        {
            foreach (Player player in _players.Values)
            {
                if (condition.Invoke(player))
                    return player;
            }

            return null;
        }

        public void Broadcast(IMessage packet)
        {

            foreach (Player p in _players.Values)
            {
                p.Session.Send(packet);
            }

        }
    }
}
