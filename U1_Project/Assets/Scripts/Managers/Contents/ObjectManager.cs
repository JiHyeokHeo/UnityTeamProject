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
	
	public void Add(PlayerInfo info, bool myPlayer = false)
	{
		if (myPlayer)
		{
			GameObject go = Managers.Resource.Instantiate("Target");
			go.name = info.Name;
			_objects.Add(info.PlayerId, go);

            MyTest = go.GetComponent<MyMonsterController>();
			MyTest.Id = info.PlayerId;
			MyTest.PosInfo = info.PosInfo;
			MyTest.SyncPos();
        }
		else
		{
            GameObject go = Managers.Resource.Instantiate("Seeker");
            go.name = info.Name;
            _objects.Add(info.PlayerId, go);

            MonsterController pc = go.GetComponent<MonsterController>();
			pc.Id = info.PlayerId;
			pc.PosInfo = info.PosInfo;
            pc.SyncPos();
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
