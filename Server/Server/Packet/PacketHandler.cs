﻿using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using Server.Game.Room;
using Server.Game;
using System.Threading;

class PacketHandler
{
	public static void C_MoveHandler(PacketSession session, IMessage packet)
	{
		//C_Move movePacket = packet as C_Move;
		//ClientSession clientSession = session as ClientSession;

	//   Console.WriteLine($"C_Move ( {clientSession.SessionId}, {movePacket.PosInfo.PosX}, {movePacket.PosInfo.PosY} , {movePacket.PosInfo.PosZ}");

		//Player player = clientSession.MyPlayer;
		//if (player == null)
		//	return;

		//GameRoom room = player.Room;
		//if (room == null)
		//	return;

		//room.Push(room.HandleMove, player, movePacket);
    }

	public static void C_WorldMoveHandler(PacketSession session, IMessage packet)
	{
		C_WorldMove worldMovePacket = packet as C_WorldMove;
		ClientSession clientSession = session as ClientSession;

		Player player = clientSession.MyPlayer;
		if (player == null)
			return;

		GameRoom room = player.Room;
		if (room == null)
			return;
		
		room.Push(room.HandleMove, player, worldMovePacket);
    }

	public static void C_SkillHandler(PacketSession session, IMessage packet)
	{
		C_Skill skillPacket = packet as C_Skill;
		ClientSession clientSession = session as ClientSession;

		Console.WriteLine($"C_Skill {skillPacket.Info.SkillId}");


		Player player = clientSession.MyPlayer;
        if (player == null)
            return;

        GameRoom room = player.Room;
        if (room == null)
            return;

		room.Push(room.HandleSkill, player, skillPacket);
    }
}
