using Assets.Scripts.Controllers;
using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
	public MonsterController MyTest { get; set; }
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
                GameObject go = Managers.Resource.Instantiate("Target");
                go.name = info.Name;
                _objects.Add(info.ObjectId, go);

                MyTest = go.GetComponent<MyMonsterController>();
                MyTest.Id = info.ObjectId;
                MyTest.PosInfo = info.PosInfo;
				MyTest.Stat = info.StatInfo;
                MyTest.SyncPos();
            }
            else
            {
                GameObject go = Managers.Resource.Instantiate("Seeker");
                go.name = info.Name;
                _objects.Add(info.ObjectId, go);

                MonsterController pc = go.GetComponent<MonsterController>();
                pc.Id = info.ObjectId;
                pc.PosInfo = info.PosInfo;
                pc.Stat = info.StatInfo;
                pc.SyncPos();
            }
        }
		else if (_objectType == GameObjectType.Monster)
		{

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

	public void RemoveMyPlayer()
	{
		if (MyTest == null)
			return;

		Remove(MyTest.Id);
		MyTest = null;
	}

	public GameObject FindById(int id)
	{
		GameObject go = null;
		_objects.TryGetValue(id, out go);
		return go;
	}

	public GameObject Find(Vector3Int cellPos)
	{
		foreach (GameObject obj in _objects.Values)
		{
			BaseController bc = obj.GetComponent<BaseController>();
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
	}
}
