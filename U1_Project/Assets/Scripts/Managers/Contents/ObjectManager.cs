﻿using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
	public MyPlayerController MyPlayer { get; set; }
	//Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();
	Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();
	
	public static GameObjectType GetObjectTypeById(int id)
	{
		int type = (id >> 24) & 0x7F;
		return (GameObjectType)type;
	}

	public void Add(ObjectInfo info, bool myPlayer = false)
	{
		GameObjectType _objectType = GetObjectTypeById(info.ObjectId);

        if (_objectType == GameObjectType.Player)
		{
            if (myPlayer)
            {
                GameObject go = Managers.Resource.Instantiate("Creature/project_-jax");
                go.name = info.Name;
                _objects.Add(info.ObjectId, go);

                MyPlayer = go.GetComponent<MyPlayerController>();
                MyPlayer.Id = info.ObjectId;
                //MyPlayer.PosInfo = info.PosInfo;
				MyPlayer.WorldPosInfo = info.WorldPosInfo;
				MyPlayer.Stat = info.StatInfo;
                MyPlayer.SyncWorldPos();
            }
            else
            {
                GameObject go = Managers.Resource.Instantiate("Creature/project_-jax2");
                go.name = info.Name;
                _objects.Add(info.ObjectId, go);

                PlayerController pc = go.GetComponent<PlayerController>();
                pc.Id = info.ObjectId;
                //pc.PosInfo = info.PosInfo;
				pc.WorldPosInfo = info.WorldPosInfo;
                pc.Stat = info.StatInfo;
                pc.SyncWorldPos();
            }
        }
		else if (_objectType == GameObjectType.Monster)
		{
            GameObject go = Managers.Resource.Instantiate("Creature/Monster");
            go.name = info.Name;
            _objects.Add(info.ObjectId, go);

            MonsterController mc = go.GetComponent<MonsterController>();
            mc.Id = info.ObjectId;
            mc.PosInfo = info.PosInfo;
            mc.Stat = info.StatInfo;
	        mc.SyncPos();
        }
		else if (_objectType == GameObjectType.Projectile)
		{
			//GameObject go = Managers.Resource.Instantiate("Creature/Arrow");
			//go.name = "Arrow";
			//_objects.Add(info.ObjectId, go);	

			// SkillController 추후 추가
		}
	
	}

	public void Remove(int id)
	{
		GameObject go = FindById(id);
		if (go == null)
			return;

		_objects.Remove(id);
		Managers.Resource.Destroy(go);
	}

	public GameObject FindById(int id)
	{
		GameObject go = null;
		_objects.TryGetValue(id, out go);
		return go;
	}

	public GameObject FindCreature(Vector3Int cellPos)
	{
		foreach (GameObject obj in _objects.Values)
		{
            CreatureController bc = obj.GetComponent<CreatureController>();
			if (bc == null)
				continue;

			if (bc.CellPos == cellPos)
				return obj;
		}

		return null;
	}

	public GameObject Find(Func<GameObject, bool> condition)
	{
		foreach (GameObject obj in _objects.Values)
		{
			if (condition.Invoke(obj))
				return obj;
		}

		return null;
	}

	public void Clear()
	{
		foreach (GameObject obj in _objects.Values)
			Managers.Resource.Destroy(obj);

		_objects.Clear();
        MyPlayer = null;
    }
}
