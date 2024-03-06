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

            MyTest = go.GetComponent<MonsterController>();
			MyTest.Id = info.PlayerId;
			MyTest.PosInfo = info.PosInfo;
        }
		else
		{
            GameObject go = Managers.Resource.Instantiate("Seeker");
            go.name = info.Name;
            _objects.Add(info.PlayerId, go);

            PlayerController pc = go.GetComponent<PlayerController>();
			pc.Id = info.PlayerId;
			pc.PosInfo = info.PosInfo;
        }

	}
	public void Add(int id, GameObject go)
	{
        _objects.Add(id, go);
	}

	public void Remove(int id)
	{
		_objects.Remove(id);
	}

	public void RemoveMyPlayer()
	{
		if (MyTest == null)
			return;

		Remove(MyTest.Id);
		MyTest = null;
	}

	public GameObject Find(Vector3Int cellPos)
	{
		foreach (GameObject obj in _objects.Values)
		{
			BaseController bc = obj.GetComponent<BaseController>();
			if (bc == null)
				continue;

			//if (bc.CellPos == cellPos)
			//	return obj;
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
		_objects.Clear();
	}
}
